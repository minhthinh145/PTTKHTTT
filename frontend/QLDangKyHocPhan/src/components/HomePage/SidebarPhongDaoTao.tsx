import type { PhongDaoTaoProfile } from "../../apis/types/phongdaotao";

interface Props {
  activeTab: string;
  setActiveTab: (tab: string) => void;
  user: PhongDaoTaoProfile;
}

const SidebarPhongDaoTao = ({ activeTab, setActiveTab, user }: Props) => {
  const navItems = [
    { key: "studentAccounts", label: "👨‍🎓 Xem danh sách tài khoản sinh viên" },
    { key: "teacherAccounts", label: "👨‍🏫 Xem danh sách tài khoản giảng viên" },
    { key: "themTaiKhoan", label: "➕ Thêm tài khoản cho sinh viên" },
    { key: "quenMatKhau", label: "🔑 Thay đổi mật khẩu cho sinh viên" },
  ];

  return (
    <aside className="w-64 bg-[#053C65] text-white p-4 flex flex-col gap-4 cursor-default">
      <div className="bg-gray-700 p-4 rounded">
        <div className="font-bold">{user.userName}</div>
        <div className="font-semibold mt-2">Phòng Đào Tạo</div>
      </div>
      <nav className="flex flex-col gap-2">
        {navItems.map((item) => (
          <button
            key={item.key}
            className={`text-left px-4 py-2 rounded cursor-pointer ${
              activeTab === item.key
                ? "bg-orange-400 text-black font-semibold"
                : "hover:bg-orange-200 hover:text-black font-semibold"
            }`}
            onClick={() => setActiveTab(item.key)}
          >
            {item.label}
          </button>
        ))}
      </nav>
    </aside>
  );
};

export default SidebarPhongDaoTao;
