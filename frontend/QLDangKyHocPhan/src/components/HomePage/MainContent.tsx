interface Props {
  activeTab: string;
}

const MainContent = ({ activeTab }: Props) => {
  const renderContent = () => {
    switch (activeTab) {
      case "register":
        return (
          <>
            <h2 className="text-2xl font-bold mb-4">ĐĂNG KÝ HỌC PHẦN</h2>
            <div className="bg-white rounded shadow p-6">
              <div className="flex items-center gap-4 mb-4">
                <select className="border p-2 rounded w-64 cursor-pointer">
                  <option>Chương trình đào tạo</option>
                </select>
                <button className="bg-blue-700 text-white hover:bg-blue-500 transition-colors duration-200 px-4 py-2 rounded cursor-pointer">
                  Làm mới
                </button>
              </div>
              <div className="text-red-600 font-semibold">
                CHƯA ĐẾN THỜI HẠN ĐĂNG KÝ MÔN HỌC. VUI LÒNG QUAY LẠI SAU
              </div>
            </div>
          </>
        );
      case "enroll":
        return (
          <>
            <h2 className="text-2xl font-bold mb-4">ĐĂNG KÝ GHI DANH</h2>
            <div className="bg-white rounded shadow p-6">
              <div className="flex items-center gap-4 mb-4">
                <select className="border p-2 rounded w-64 cursor-pointer">
                  <option>Chương trình đào tạo</option>
                </select>
                <button className="bg-blue-700 text-white hover:bg-blue-500 transition-colors duration-200 px-4 py-2 rounded cursor-pointer">
                  Làm mới
                </button>
              </div>
              <div className="text-red-600 font-semibold">
                CHƯA ĐẾN THỜI HẠN GHI DANH MÔN HỌC. VUI LÒNG QUAY LẠI SAU
              </div>
            </div>
          </>
        );
      case "search":
        return (
          <>
            <h2 className="text-2xl font-bold mb-4">TRA CỨU MÔN HỌC</h2>
            <div className="bg-white p-6 rounded shadow">
              <div className="flex flex-wrap gap-4 mb-4">
                <select className="border px-4 py-2 rounded w-60 cursor-pointer">
                  <option>Loại tra cứu</option>
                  <option>Mã học phần</option>
                  <option>Tên học phần</option>
                </select>
                <input
                  type="text"
                  placeholder="Nhập thông tin môn học..."
                  className="border px-4 py-2 rounded flex-1 min-w-[200px]"
                />
                <button className="cursor-pointer bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-500">
                  Tìm kiếm
                </button>
              </div>

              <div className="overflow-auto rounded border border-gray-300">
                <table className="min-w-full text-sm text-left">
                  <thead className="bg-[#053C65] text-white">
                    <tr>
                      <th className="px-4 py-2">STT</th>
                      <th className="px-4 py-2">Mã HP</th>
                      <th className="px-4 py-2">Mã LHP</th>
                      <th className="px-4 py-2">Tên HP</th>
                      <th className="px-4 py-2">STC</th>
                      <th className="px-4 py-2">Loại</th>
                      <th className="px-4 py-2">Thông tin</th>
                      <th className="px-4 py-2">Giảng viên</th>
                      <th className="px-4 py-2">Giới hạn</th>
                      <th className="px-4 py-2">Từ ngày</th>
                      <th className="px-4 py-2">Đến ngày</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="bg-gray-50 text-center">
                      <td className="px-4 py-2" colSpan={11}>
                        Chưa có dữ liệu
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </>
        );
      case "history":
        return (
          <>
            <h2 className="text-2xl font-bold mb-4">
              LỊCH SỬ ĐĂNG KÝ HỌC PHẦN
            </h2>
            <div className="bg-white p-6 rounded shadow">
              <div className="flex flex-wrap gap-4 mb-4">
                <select className="border px-4 py-2 rounded w-60 cursor-pointer">
                  <option>Năm học</option>
                </select>
                <select className="border px-4 py-2 rounded w-60 cursor-pointer">
                  <option>Học kỳ</option>
                </select>
                <button className="cursor-pointer bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-600">
                  Tìm kiếm
                </button>
              </div>

              <div className="overflow-auto rounded border border-gray-300">
                <table className="min-w-full text-sm text-left">
                  <thead className="bg-[#053C65] text-white">
                    <tr>
                      <th className="px-4 py-2">STT</th>
                      <th className="px-4 py-2">Mã HP</th>
                      <th className="px-4 py-2">Mã LHP</th>
                      <th className="px-4 py-2">Tên HP</th>
                      <th className="px-4 py-2">STC</th>
                      <th className="px-4 py-2">Loại</th>
                      <th className="px-4 py-2">Thông tin</th>
                      <th className="px-4 py-2">Giảng viên</th>
                      <th className="px-4 py-2">Giới hạn</th>
                      <th className="px-4 py-2">Từ ngày</th>
                      <th className="px-4 py-2">Đến ngày</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="bg-gray-50 text-center">
                      <td className="px-4 py-2" colSpan={11}>
                        Chưa có dữ liệu
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </>
        );
      default:
        return <h2 className="text-xl">Không tìm thấy nội dung</h2>;
    }
  };

  return <main className="p-6 bg-gray-100 flex-1">{renderContent()}</main>;
};

export default MainContent;
