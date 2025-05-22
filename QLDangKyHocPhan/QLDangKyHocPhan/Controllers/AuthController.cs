using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Services.Interface;
using System.Security.Claims;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AuthController(IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signIn)
        {

            var result = await _accountService.SignInAsync(signIn);
            if (result == null)
            {
                return Unauthorized(new { message = "Tên đăng nhập không hợp lệ hoặc sai mật khẩu." });
            }

            return Ok(result);
        }

            [HttpGet("profile")]
            [Authorize]
            public async Task<IActionResult> GetProfile()
            {
                try
                {
                    var userId = GetUserIdByToken();    
                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized(new { message = "Invalid token." });
                    }

                    var userDTO = await _accountService.FindUserById(userId);
                    if (userDTO == null)
                    {
                        return NotFound(new { message = "User not found." });
                    }
                    var response = await _accountService.GetProfileByUserAccount(userDTO);
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    return StatusCode(500, new { message = "An error occurred while retrieving the profile.", error = ex.Message });
                }
            }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest(new { message = "Refresh token is required." });
            }

            try
            {
                var accessToken = await _authService.RefreshAccessTokenAsync(request.RefreshToken);
                return Ok(new { AccessToken = accessToken });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserProfileDTO user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "Invalid token." });
            }
            var userUpdate = await _accountService.UpdateUserById(userId, user);
            if (userUpdate == null)
            {
                return BadRequest(new { message = "Cannot update user" });
            }
            return Ok(new { message = "Cập nhật thông tin thành công" }); 
        }

        [HttpPost("checkpassword")]
        public async Task<IActionResult> CheckPasswordUser([FromBody] string password)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "Invalid token." });
            }
            var result = await _accountService.CheckPasswordAsync(userId, password);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut([FromBody] RefreshTokenRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest(new { message = "Refresh token is required." });
            }

            await _authService.RevokeRefreshTokenAsync(request.RefreshToken);
            return Ok(new { message = "Signed out successfully." });
        }


        private  string GetUserIdByToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
            {
                throw new Exception("User ID not found in claims.");
            }
        }
    }
}
