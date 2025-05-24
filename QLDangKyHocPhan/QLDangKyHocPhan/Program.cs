using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Implementation;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Implementation;
using QLDangKyHocPhan.Services.Interface;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Thêm logging
    builder.Services.AddLogging(logging =>
    {
        logging.AddConsole();
        logging.AddDebug();
        logging.SetMinimumLevel(LogLevel.Debug); // Tăng level để bắt lỗi chi tiết
    });

    // Thêm controllers
    builder.Services.AddControllers()
          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
          }); 

    // Cấu hình Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "QLDangKyHocPhan API",
            Version = "v1",
            Description = "API quản lý đăng ký học phần"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

    // Cấu hình AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));

    // Cấu hình DbContext (kiểm tra connection string)
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or empty.");
    }
    builder.Services.AddDbContext<QlDangKyHocPhanContext>(options =>
        options.UseSqlServer(connectionString));

    // Cấu hình Identity
    builder.Services.AddIdentity<Taikhoan, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 1;
        options.User.AllowedUserNameCharacters =
     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";
    })
    .AddEntityFrameworkStores<QlDangKyHocPhanContext>()
    .AddDefaultTokenProviders();

    // Đăng ký services
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUserImportService, UserImportService>();
    builder.Services.AddScoped<IChuongTrinhDaoTaoService, ChuongTrinhDaoTaoService>();
    builder.Services.AddScoped<ILopHocPhanService, LopHocPhanService>();
    builder.Services.AddScoped<ILichSuDangKyService, LichSuDangKyService>();
    builder.Services.AddScoped<IHocPhanService, HocPhanService>();
    builder.Services.AddScoped<ISinhVienService, SinhVienService>();
    builder.Services.AddScoped<IDangKyHocPhanService, DangKyHocPhanService>();
    builder.Services.AddScoped<IHocPhanDangKyService, HocPhanDangKyService>();
    builder.Services.AddScoped<IGiangVienService, GiangVienService>();



    builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    builder.Services.AddScoped<IChuongTrinhDaoTaoRepository, ChuongTrinhDaoTaoRepository>();
    builder.Services.AddScoped<ISinhVienRepository, SinhVienRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ILopHocPhanRepository, LopHocPhanRepository>();
    builder.Services.AddScoped<IDangKyHocPhanRepository, DangKyHocPhanRepository>();
    builder.Services.AddScoped<ILichSuDangKyRepository, LichSuDangKyRepository>();
    builder.Services.AddScoped<IHocPhanRepository, HocPhanRepository>();
    builder.Services.AddScoped<IHocPhanDangKyRepository, HocPhanDangKyRepository>();
    builder.Services.AddScoped<IGiangVienRepository, GiangVienRepository>();



    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // nếu muốn tăng max depth
    });

    // Cấu hình giới hạn file upload
    builder.Services.Configure<FormOptions>(options =>
    {
        options.MultipartBodyLengthLimit = 104857600; // 100MB
    });

    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.MaxRequestBodySize = 104857600; // 100MB
    });

    // Cấu hình Authentication JWT
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

    // Cấu hình CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Seed roles (bọc trong try-catch để bắt lỗi)
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await DbInitializer.SeedRolesAsync(roleManager);
        }
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi khi seed roles");
        throw; // Ném lại để debug
    }

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "QLDangKyHocPhan API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    // Middleware logging
    app.Use(async (context, next) =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogDebug($"Xử lý yêu cầu: {context.Request.Method} {context.Request.Path}");
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Lỗi trong pipeline");
            throw;
        }
        logger.LogDebug($"Hoàn thành yêu cầu: {context.Response.StatusCode}");
    });

    app.UseCors("AllowAllOrigins");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Lỗi khởi động ứng dụng: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    throw;
}