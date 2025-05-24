import { useEffect, useState } from "react";
import type { User } from "../apis/types/user";
import type { HocPhanDTO } from "../apis/types/HocPhan";
import type { ApiResponse } from "../apis/types/auth";
import { callGetHocPhanChuaDangKy } from "../apis/services/hocPhanService";

export const useHocPhanChuaDangKy = (user: User | null) => {
  const [hocPhanList, setHocPhanList] = useState<HocPhanDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!user?.maSinhVien || !user?.maCT) return;

      setLoading(true);
      try {
        const res: ApiResponse<HocPhanDTO[]> = await callGetHocPhanChuaDangKy(
          user
        );

        if (res.isSuccess && res.data) {
          setHocPhanList(res.data);
          setError(null);
        } else {
          setError(res.message || "Không lấy được danh sách học phần.");
        }
      } catch (err: any) {
        setError(err.message || "Lỗi không xác định.");
      }
      setLoading(false);
    };

    fetchData();
  }, [user]);

  return { hocPhanList, loading, error };
};
