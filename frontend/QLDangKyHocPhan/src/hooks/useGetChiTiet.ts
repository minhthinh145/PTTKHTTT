import { useEffect, useState } from "react";
import type { User } from "../apis/types/user"; // cập nhật path nếu cần
import type { ChiTietCTDTDTO } from "../apis/types/ChuongTrinhDaoTao";
import type { ApiResponse } from "../apis/types/auth";
import { callGetChiTietCTDT } from "../apis/services/ChuongTrinhDaoTaoService";

export const useGetChiTietCTDT = (user: User | null) => {
  const [chiTietCTDT, setChiTietCTDT] = useState<ChiTietCTDTDTO[] | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!user?.maCT) return;

      setLoading(true);
      const res: ApiResponse<ChiTietCTDTDTO[]> = await callGetChiTietCTDT(user);

      if (res.isSuccess && res.data) {
        setChiTietCTDT(res.data);
        setError(null);
      } else {
        setError(res.message || "Lỗi không xác định khi lấy Chi tiết CTĐT");
      }

      setLoading(false);
    };

    fetchData();
  }, [user]);

  return { chiTietCTDT, loading, error };
};
