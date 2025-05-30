import { useState } from "react";
import { getSinhVienByMaLopHocPhan } from "../../apis/services/GiangVienService";
import type { SinhVienProfile } from "../../apis/types/user";

export function useSinhVienByLopHocPhan() {
  const [data, setData] = useState<SinhVienProfile[] | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchSinhVien = async (maLopHocPhan: string) => {
    setLoading(true);
    setError(null);
    try {
      const res = await getSinhVienByMaLopHocPhan(maLopHocPhan);
      if (res.isSuccess && res.data) {
        setData(res.data);
      } else {
        setError(res.message || "Lỗi không xác định");
        setData(null);
      }
    } catch (err: any) {
      setError(err.message || "Lỗi không xác định");
      setData(null);
    } finally {
      setLoading(false);
    }
  };

  return { data, loading, error, fetchSinhVien };
}
