import Sidebar from "../components/HomePage/SidebarSV";
import Header from "../components/HomePage/Header";
import MainContent from "../components/SinhVienPage/SinhVienContent";
import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useGetProfile } from "../hooks/SinhVien/useGetProfile";
import { useGetCTDT } from "../hooks/SinhVien/useGetCTDT";
import { useLopHocPhan } from "../hooks/SinhVien/HocPhan/useGetLopHocPhan";
import { useHocPhanChuaDangKy } from "../hooks//SinhVien/HocPhan/useHocPhanChuaDangKy";

function HomePage() {
  const { getValidToken } = useAuth();
  const [activeTab, setActiveTab] = useState("register");

  const {
    user,
    loading: userLoading,
    error: userError,
    reloadProfile,
  } = useGetProfile(getValidToken);

  const getCTDTResult = useGetCTDT(user);
  const getLopHocPhanResult = useLopHocPhan();
  const getHocPhanChuaDangKyResult = useHocPhanChuaDangKy(user);

  const { ctdt, loading: ctdtLoading, error: ctdtError } = getCTDTResult;
  const {
    lopHocPhan,
    loading: lopLoading,
    error: lopError,
  } = getLopHocPhanResult;
  const {
    hocPhanList,
    loading: hpcdLoading,
    error: hpcdError,
  } = getHocPhanChuaDangKyResult;

  const isLoading = userLoading || ctdtLoading || lopLoading;
  const isError = userError || ctdtError || lopError;

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
          <MainContent
            activeTab={activeTab}
            ctdt={ctdt}
            lopHocPhan={lopHocPhan}
            user={user}
            reloadProfile={reloadProfile}
            hocPhanChuaDangKy={hocPhanList}
          />
        )}
      </div>
    </div>
  );
}

export default HomePage;
