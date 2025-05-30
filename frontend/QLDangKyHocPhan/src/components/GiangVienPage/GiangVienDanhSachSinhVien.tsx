import Sidebar from "../HomePage/SidebarGV";
import Header from "../HomePage/Header";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
import { useGetGiangVienProfile } from "../../hooks/GiangVien/useGetGiangVienProfile";
import { useSinhVienByLopHocPhan } from "../../hooks/GiangVien/useSinhVienByLopHocPhan";
import { useNavigate } from "react-router-dom";
export const GiangVienDanhSachSinhVien = () => {
  const { getValidToken } = useAuth();
  const { maLopHocPhan } = useParams();
  const [activeTab, setActiveTab] = useState("register");

  const {
    user,
    loading: userLoading,
    error: userError,
  } = useGetGiangVienProfile(getValidToken);

  const { data, loading, error, fetchSinhVien } = useSinhVienByLopHocPhan();
  const navigate = useNavigate();

  useEffect(() => {
    if (maLopHocPhan) fetchSinhVien(maLopHocPhan);
  }, [maLopHocPhan]);

  if (userLoading) {
    return (
      <div className="p-6 text-center text-gray-500">Đang tải dữ liệu...</div>
    );
  }
  if (userError) {
    return <div className="p-6 text-center text-red-600">{userError}</div>;
  }
  if (!user) {
    return (
      <div className="p-6 text-center text-gray-500">
        Không có dữ liệu để hiển thị.
      </div>
    );
  }

  return (
    <div className="flex h-screen">
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} user={user} />
      <div className="flex-1 flex flex-col">
        <Header />
        <div className="p-8 flex-1 overflow-auto bg-[#f7fafc]">
          <button
            onClick={() => navigate(-1)}
            className="mb-4 px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
          >
            ← Quay lại
          </button>
          <h2 className="text-xl font-bold mb-4">
            Danh sách sinh viên đăng ký lớp học phần {maLopHocPhan}
          </h2>
          {loading && <p>Đang tải...</p>}
          {error && <p className="text-red-500">{error}</p>}
          {Array.isArray(data) && (
            <table className="w-full border text-sm bg-white rounded shadow text-center t">
              <thead>
                <tr>
                  <th className="border px-2 py-1">STT</th>
                  <th className="border px-2 py-1">Mã SV</th>
                  <th className="border px-2 py-1">Họ tên</th>
                  <th className="border px-2 py-1">Ngày sinh</th>
                  <th className="border px-2 py-1">Email</th>
                </tr>
              </thead>
              <tbody>
                {data.length === 0 ? (
                  <tr>
                    <td
                      colSpan={6}
                      className="border px-2 py-4 text-center text-gray-500"
                    >
                      Không có sinh viên nào đăng ký lớp này.
                    </td>
                  </tr>
                ) : (
                  data.map((sv, idx) => (
                    <tr key={sv.maSinhVien}>
                      <td className="border px-2 py-1">{idx + 1}</td>
                      <td className="border px-2 py-1">{sv.maSinhVien}</td>
                      <td className="border px-2 py-1">{sv.hoTen}</td>
                      <td className="border px-2 py-1">
                        {sv.ngaySinh
                          ? new Date(sv.ngaySinh).toLocaleDateString("vi-VN")
                          : ""}
                      </td>
                      <td className="border px-2 py-1">{sv.email}</td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          )}
        </div>
      </div>
    </div>
  );
};
