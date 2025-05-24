import type { CTDAOTAO } from "../../apis/types/ChuongTrinhDaoTao";
import RegisterTabContent from "./ChildPage/RegisterTabContent";
import EnrollTabContent from "./ChildPage/EnrollTabContent";
import SearchTabContent from "./ChildPage/SearchTabContent";
import HistoryTabContent from "./ChildPage/HistoryTabContent";
import type { LopHocPhanDTO } from "../../apis/types/lopHocPhan";
import type { User } from "../../apis/types/user"; // import interface User tương ứng với SinhVienDTO
import type { HocPhanDTO } from "../../apis/types/HocPhan";

interface Props {
  activeTab: string;
  ctdt: CTDAOTAO | null;
  lopHocPhan: LopHocPhanDTO[] | undefined;
  hocPhanChuaDangKy: HocPhanDTO[] | undefined;
  user: User | null;
  reloadProfile: () => Promise<void>;
}

const MainContent = ({
  activeTab,
  ctdt,
  lopHocPhan,
  hocPhanChuaDangKy,
  user,
  reloadProfile,
}: Props) => {
  const renderContent = () => {
    switch (activeTab) {
      case "register":
        return (
          <RegisterTabContent
            ctdt={ctdt}
            hocPhanChuaDangKy={hocPhanChuaDangKy}
            user={user}
          />
        );
      case "enroll":
        return <EnrollTabContent />;
      case "search":
        return <SearchTabContent lopHocPhan={lopHocPhan} />;
      case "history":
        return <HistoryTabContent />;
      default:
        return <h2 className="text-xl">Không tìm thấy nội dung</h2>;
    }
  };

  return <main className="p-6 bg-gray-100 flex-1">{renderContent()}</main>;
};

export default MainContent;
