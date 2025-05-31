import type { SinhVienProfile } from "../../apis/types/user";

interface EnrollTabContentProps {
  user: SinhVienProfile | null; // thay any bằng kiểu user thực tế nếu có
}

const EnrollTabContent: React.FC<EnrollTabContentProps> = ({ user }) => {
  return (
    <>
      <h2 className="text-2xl font-bold mb-4">ĐĂNG KÝ GHI DANH</h2>
      <div className="bg-white rounded shadow p-6">
        <div className="flex items-center gap-4 mb-4">
          <select className="border p-2 rounded w-64 cursor-pointer">
            <option>{user?.tenCTDT}</option>
          </select>
          <button className="bg-blue-700 text-white hover:bg-blue-500 transition-colors duration-200 px-4 py-2 rounded cursor-pointer">
            Làm mới
          </button>
        </div>
        <div className="text-red-600 font-semibold">
          CHƯA ĐẾN THỜI HẠN GHI DANH MÔN HỌC. VUI LÒNG QUAY LẠI SAU
        </div>
        {/* Ví dụ sử dụng user */}
        {/* <div className="mt-4 text-gray-600">Xin chào, {user?.hoTen}</div> */}
      </div>
    </>
  );
};

export default EnrollTabContent;
