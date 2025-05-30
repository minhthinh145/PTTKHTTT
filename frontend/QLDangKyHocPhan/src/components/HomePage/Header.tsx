import { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
const Header = () => {
  const { user, logout } = useAuth();
  const [open, setOpen] = useState(false);

  return (
    <header className="bg-[#084B83] text-white px-6 py-4 font-bold text-xl flex justify-between items-center relative">
      <span>TRƯỜNG ĐẠI HỌC ABC</span>
      <div
        className="rounded-full bg-white w-8 h-8 cursor-pointer flex items-center justify-center text-[#084B83]"
        onClick={() => setOpen(!open)}
      >
        {/* Có thể hiển thị avatar hoặc chữ viết tắt tên */}
        {user?.username
          ? user.username
              .split(" ")
              .map((n: string) => n[0])
              .join("")
              .toUpperCase()
          : "U"}
      </div>

      {open && (
        <div
          className="absolute right-6 top-full mt-2 bg-white text-black rounded shadow-lg w-24 py-2 text-center cursor-pointer"
          onClick={() => {
            logout();
            setOpen(false);
          }}
        >
          Đăng xuất
        </div>
      )}
    </header>
  );
};

export default Header;
