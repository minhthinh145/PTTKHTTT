import { useEffect, useState } from "react";
import { getAccountsByRole } from "../../apis/services/accountServices";
import type { TaiKhoanDTO } from "../../apis/types/taiKhoan";
import type { SinhVienProfile } from "../../apis/types/user";

const StudentAccountList = () => {
  const [accounts, setAccounts] = useState<TaiKhoanDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAccountsByRole("SinhVien").then((data) => {
      setAccounts(Array.isArray(data) ? data : []);
      setLoading(false);
    });
  }, []);

  if (loading) return <div>Đang tải danh sách sinh viên...</div>;

  return (
    <div className="h-[calc(100vh-100px)] flex flex-col">
      {" "}
      {/* Chiều cao tuỳ chỉnh, trừ header nếu có */}
      <h2 className="font-bold mb-4">Danh sách tài khoản sinh viên</h2>
      <div className="flex-1 overflow-auto border border-gray-300 rounded">
        <table className="min-w-full text-sm text-center">
          <thead className="bg-gray-100 sticky top-0 z-10">
            <tr>
              <th className="border px-4 py-2">MSSV</th>
              <th className="border px-4 py-2">Tên sinh viên</th>
              <th className="border px-4 py-2">Email</th>
              <th className="border px-4 py-2">Ngày sinh</th>
              <th className="border px-4 py-2">Giới tính</th>
              <th className="border px-4 py-2">Mã CT</th>
              <th className="border px-4 py-2">Tên CTĐT</th>
              <th className="border px-4 py-2">Tên khoa</th>
            </tr>
          </thead>
          <tbody>
            {accounts.map((acc) => {
              const sv: SinhVienProfile | undefined = acc.sinhVien;
              if (!sv) return null;
              return (
                <tr key={acc.id}>
                  <td className="border px-4 py-2">{sv.maSinhVien}</td>
                  <td className="border px-4 py-2">{sv.hoTen}</td>
                  <td className="border px-4 py-2">{sv.email}</td>
                  <td className="border px-4 py-2">
                    {sv.ngaySinh
                      ? new Date(sv.ngaySinh).toLocaleDateString()
                      : ""}
                  </td>
                  <td className="border px-4 py-2">{sv.gioiTinh}</td>
                  <td className="border px-4 py-2">{sv.maCT}</td>
                  <td className="border px-4 py-2">{sv.tenCTDT}</td>
                  <td className="border px-4 py-2">{sv.tenKhoa}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default StudentAccountList;
