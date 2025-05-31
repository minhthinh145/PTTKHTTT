import { useEffect, useState } from "react";
import { getAccountsByRole } from "../../apis/services/accountServices";
import type { TaiKhoanDTO } from "../../apis/types/taiKhoan";
import type { GiangVienProfile } from "../../apis/types/giangvien";

const TeacherAccountList = () => {
  const [accounts, setAccounts] = useState<TaiKhoanDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAccountsByRole("GiangVien").then((data) => {
      setAccounts(Array.isArray(data) ? data : []);
      setLoading(false);
      console.log("GiangVien accounts:", data);
    });
  }, []);

  if (loading) return <div>Đang tải danh sách giảng viên...</div>;

  return (
    <div>
      <h2 className="font-bold mb-4">Danh sách tài khoản giảng viên</h2>
      <div className="overflow-x-auto">
        <table className="min-w-full border border-gray-300 text-center">
          <thead className="bg-gray-100">
            <tr>
              <th className="border px-2 py-1">Mã GV</th>
              <th className="border px-2 py-1">Tên giảng viên</th>
              <th className="border px-2 py-1">Email</th>
              <th className="border px-2 py-1">Địa chỉ</th>
              <th className="border px-2 py-1">Lớp học</th>
            </tr>
          </thead>
          <tbody>
            {accounts.map((acc) => {
              const gv: GiangVienProfile | undefined = acc.giangVien;
              if (!gv) return null;
              return (
                <tr key={acc.id}>
                  <td className="border px-2 py-1">{gv.maGiangVien}</td>
                  <td className="border px-2 py-1">{gv.hoTen}</td>
                  <td className="border px-2 py-1">{gv.email}</td>
                  <td className="border px-2 py-1">{gv.diaChi}</td>
                  <td className="border px-2 py-1">{gv.lopHoc}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default TeacherAccountList;
