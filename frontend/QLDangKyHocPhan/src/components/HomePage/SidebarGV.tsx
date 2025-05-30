import type { GiangVienProfile } from "../../apis/types/giangvien";

interface Props {
  activeTab: string;
  setActiveTab: (tab: string) => void;
  user: GiangVienProfile;
}

const SidebarGV = ({ activeTab, setActiveTab, user }: Props) => {
  const giangVienNavItems = [
    { key: "classList", label: "📋 Danh sách lớp giảng dạy" },
  ];

  return (
    <aside className="w-64 bg-[#053C65] text-white p-4 flex flex-col gap-4 cursor-default">
      <div className="bg-gray-700 p-4 rounded">
        <div className="font-bold">{user.hoTen}</div>
        <div className="text-sm text-gray-300">{user.maGiangVien}</div>
        <div className="font-semibold mt-2">Giảng viên</div>
      </div>

      <nav className="flex flex-col gap-2">
        {giangVienNavItems.map((item) => (
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

export default SidebarGV;
