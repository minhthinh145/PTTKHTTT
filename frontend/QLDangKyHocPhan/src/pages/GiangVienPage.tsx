import Sidebar from "../components/HomePage/SidebarGV";
import Header from "../components/HomePage/Header";
import GiangVienContent from "../components/GiangVienPage/GiangVienContent";
import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useGetGiangVienProfile } from "../hooks/GiangVien/useGetGiangVienProfile";
import { useLopHocPhanByGiangVien } from "../hooks/GiangVien/useGetLopGiangVien";

function GiangVienPage() {
  const { getValidToken } = useAuth();
  const [activeTab, setActiveTab] = useState("register");

  const {
    user,
    loading: userLoading,
    error: userError,
  } = useGetGiangVienProfile(getValidToken);

  const {
    lopHocPhan: lopHocPhanGV,
    loading: lopLoadingGV,
    error: lopErrorGV,
  } = useLopHocPhanByGiangVien();

  const isLoading = userLoading || lopLoadingGV;
  const isError = userError || lopErrorGV;
  if (userLoading) {
    return (
      <div className="p-6 text-center text-gray-500">Đang tải dữ liệu...</div>
    );
  }
  if (userError) {
    return <div className="p-6 text-center text-red-600">{userError}</div>;
  }
  if (lopLoadingGV) {
    return (
      <div className="p-6 text-center text-gray-500">
        Đang tải dữ liệu lớp học phần...
      </div>
    );
  }
  if (lopErrorGV) {
    return <div className="p-6 text-center text-red-600">{lopErrorGV}</div>;
  }
  if (!user || !lopHocPhanGV) {
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

        {isLoading && (
          <div className="p-6 text-center text-gray-500">
            Đang tải dữ liệu...
          </div>
        )}
        {isError && (
          <div className="p-6 text-center text-red-600">{isError}</div>
        )}

        {!isLoading && !isError && (
          <GiangVienContent
            lopHocPhan={lopHocPhanGV || []}
            loading={lopLoadingGV}
            error={lopErrorGV}
          />
        )}
      </div>
    </div>
  );
}

export default GiangVienPage;
