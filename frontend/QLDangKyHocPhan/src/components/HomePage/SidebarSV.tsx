import type { SinhVienProfile } from "../../apis/types/user";

interface Props {
  activeTab: string;
  setActiveTab: (tab: string) => void;
  user: SinhVienProfile | null;
}

const SidebarSV = ({ activeTab, setActiveTab, user }: Props) => {
  const sinhVienNavItems = [
    { key: "register", label: "📘 Đăng ký học phần" },
    { key: "enroll", label: "📝 Đăng ký ghi danh" },
    { key: "search", label: "🔍 Tra cứu danh sách lớp học phần" },
    { key: "history", label: "📅 Lịch sử đăng ký học phần" },
  ];

  return (
    <aside className="w-64 bg-[#053C65] text-white p-4 flex flex-col gap-4 cursor-default">
      <div className="bg-gray-700 p-4 rounded">
        <div className="font-bold">{user?.hoTen ?? "Chưa đăng nhập"}</div>
        <div className="text-sm text-gray-300">{user?.maSinhVien}</div>
        <div className="font-semibold mt-2">Sinh viên</div>
      </div>

      <nav className="flex flex-col gap-2">
        {sinhVienNavItems.map((item) => (
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

export default SidebarSV;
