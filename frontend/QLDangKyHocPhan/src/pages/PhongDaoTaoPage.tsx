import { useState } from "react";
import SidebarPhongDaoTao from "../components/HomePage/SidebarPhongDaoTao";
import { useGetPhongDaoTaoProfile } from "../hooks/PhongDaoTao/useGetPhongDaoTaoProfile";
import { useAuth } from "../contexts/AuthContext";
import Header from "../components/HomePage/Header";
import StudentAccountList from "../components/PhongDaoTao/StudentAccountList";
import TeacherAccountList from "../components/PhongDaoTao/TeacherAccountList";
import ThemTaiKhoan from "../components/PhongDaoTao/ThemTaiKhoan";
import DoiMatKhauSinhVien from "../components/PhongDaoTao/DoiMatKhauSinhVien";

const PhongDaoTaoPage = () => {
  const [activeTab, setActiveTab] = useState("studentAccounts");
  const { getValidToken } = useAuth();
  const { user, loading, error } = useGetPhongDaoTaoProfile(getValidToken);

  if (loading) return <div>Đang tải...</div>;
  if (error || !user) return <div>Lỗi: {error || "Không tìm thấy user"}</div>;

  return (
    <div className="flex h-screen">
      <SidebarPhongDaoTao
        activeTab={activeTab}
        setActiveTab={setActiveTab}
        user={user}
      />

      <div className="flex-1 flex flex-col">
        <Header />

        <main className="flex-1 p-8">
          {activeTab === "studentAccounts" && <StudentAccountList />}
          {activeTab === "teacherAccounts" && <TeacherAccountList />}
          {activeTab === "themTaiKhoan" && <ThemTaiKhoan />}
          {activeTab === "quenMatKhau" && <DoiMatKhauSinhVien />}
        </main>
      </div>
    </div>
  );
};

export default PhongDaoTaoPage;
