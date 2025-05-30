import { useEffect, useState } from "react";
import type { User } from "../../apis/types/user"; // cập nhật path nếu cần
import type { CTDAOTAO } from "../../apis/types/ChuongTrinhDaoTao";
import type { ApiResponse } from "../../apis/types/auth";
import { callGetCTDT } from "../../apis/services/ChuongTrinhDaoTaoService";

export const useGetCTDT = (user: User | null) => {
  const [ctdt, setCTDT] = useState<CTDAOTAO | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!user?.maCT) return;

      setLoading(true);
      const res: ApiResponse<CTDAOTAO> = await callGetCTDT(user);

      if (res.isSuccess && res.data) {
        setCTDT(res.data);
        setError(null);
      } else {
        setError(res.message || "Lỗi không xác định khi lấy CTĐT");
      }

      setLoading(false);
    };

    fetchData();
  }, [user]);

  return { ctdt, loading, error };
};
