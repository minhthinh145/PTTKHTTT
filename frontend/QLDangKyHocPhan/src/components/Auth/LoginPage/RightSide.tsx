import { useState } from "react";
import { useSignIn } from "../../../hooks/auth/useSignIn";
import { LoginButton } from "./LoginButton";
import { PasswordField } from "./PasswordField";
import { UsernameField } from "./UserNameField";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../contexts/AuthContext";
import { toast } from "react-toastify";

export const RightSide = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { signIn, loading, error } = useSignIn();
  const navigate = useNavigate();
  const { login } = useAuth();

  const handleLogin = async () => {
    const result = await signIn({ tenDangNhap: username, password });
    console.log("SignIn result:", result);

    if (result.status === 200 && result.data) {
      const { accessToken, refreshToken, loaiTaiKhoan } = result.data;
      await login(username, accessToken, refreshToken);
      toast.success("Đăng nhập thành công!");
      //chờ khoảng 1s

      // ✅ Điều hướng theo loại tài khoản
      switch (loaiTaiKhoan) {
        case "SinhVien":
          navigate("/SinhVien/Home");
          break;
        case "GiangVien":
          navigate("/GiangVien/Home");
          break;
        case "Admin":
          navigate("/Admin/Dashboard");
          break;
        default:
          navigate("/HomePage"); // fallback
      }
    } else {
      console.error("Login failed:", result.message);
    }
  };

  return (
    <div className="flex-1 flex flex-col bg-[#effafd] px-10 py-6 my-auto">
      {/* Header trên cùng */}
      <div className="mb-6 text-center">
        <h2 className="text-xl ">TRƯỜNG ĐẠI HỌC ABCD</h2>
        <p className="text-2xl font-bold">CỔNG ĐĂNG KÝ HỌC PHẦN</p>
      </div>

      {/* Phần form, bọc trong div bg trắng, bo góc, padding */}
      <div className="bg-white rounded-2xl p-8 max-w-md w-full mx-auto shadow-md border border-gray-50">
        <div className="mb-6 text-center">
          <h1 className="text-3xl font-bold text-red-500">ĐĂNG NHẬP</h1>
          <p className="text-sm">Cổng đăng ký học phần</p>
        </div>

        <UsernameField
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <PasswordField
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <LoginButton onClick={handleLogin} disabled={loading} />

        {error && (
          <p className="text-red-500 text-sm mt-4 text-center">{error}</p>
        )}
      </div>
      <p className="max-w-md mx-auto mt-8 text-center text-gray-500 text-sm">
        Website được phát triển bởi nhóm 4 - Môn Phân tích và thiết kế hệ thống
        thông tin
      </p>
    </div>
  );
};
