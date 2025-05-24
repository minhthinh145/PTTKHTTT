import Sidebar from "../components/HomePage/Sidebar";
import Header from "../components/HomePage/Header";
import MainContent from "../components/HomePage/MainContent";
import GiangVienContent from "..//components/HomePage/ChildPage/GiangVienContext";
import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useGetProfile } from "../hooks/useGetProfile";
import { useGetCTDT } from "../hooks/useGetCTDT";
import { useLopHocPhan } from "../hooks/useGetLopHocPhan";
import { useHocPhanChuaDangKy } from "../hooks/useHocPhanChuaDangKy";

// Hook dành riêng cho giảng viên
import { useLopHocPhanByGiangVien } from "../hooks/useGetLopGiangVien";

function HomePage() {
  const { getValidToken } = useAuth();

  const {
    user,
    loading: userLoading,
    error: userError,
    reloadProfile,
  } = useGetProfile(getValidToken);

  const isGiangVien = user && "maGiangVien" in user;

  // Gọi tất cả hooks, không điều kiện
  const getCTDTResult = useGetCTDT(user);
  const getLopHocPhanResult = useLopHocPhan();
  const getHocPhanChuaDangKyResult = useHocPhanChuaDangKy(user);
  const getLopHocPhanGVResult = useLopHocPhanByGiangVien();

  // Destructure kết quả
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
  const {
    lopHocPhan: lopHocPhanGV,
    loading: lopLoadingGV,
    error: lopErrorGV,
  } = getLopHocPhanGVResult;
  console.log("LopHocPhanGV", lopHocPhanGV);
  const [activeTab, setActiveTab] = useState("register");

  // Xác định loading, error theo role
  const isLoading =
    userLoading || (isGiangVien ? lopLoadingGV : ctdtLoading || lopLoading);
  const isError =
    userError || (isGiangVien ? lopErrorGV : ctdtError || lopError);

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
          <>
            {isGiangVien ? (
              <GiangVienContent
                lopHocPhan={lopHocPhan || []}
                loading={lopLoadingGV}
                error={lopErrorGV}
              />
            ) : (
              <MainContent
                activeTab={activeTab}
                ctdt={ctdt}
                lopHocPhan={lopHocPhan}
                user={user}
                reloadProfile={reloadProfile}
                hocPhanChuaDangKy={hocPhanList}
              />
            )}
          </>
        )}
      </div>
    </div>
  );
}
export default HomePage;
