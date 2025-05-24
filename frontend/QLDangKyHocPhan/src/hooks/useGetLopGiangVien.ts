import { useState, useEffect } from "react";
import { getLopHocPhanByGiangVien } from "../apis/services/GiangVienService";
import type { LopHocPhanDTO } from "../apis/types/lopHocPhan";

interface UseLopHocPhanByGiangVienResult {
  lopHocPhan: LopHocPhanDTO[] | undefined;
  loading: boolean;
  error: string | null;
  reload: () => void;
}

export const useLopHocPhanByGiangVien = (): UseLopHocPhanByGiangVienResult => {
  const [lopHocPhan, setLopHocPhan] = useState<LopHocPhanDTO[] | undefined>(
    undefined
  );
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [reloadFlag, setReloadFlag] = useState<number>(0);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await getLopHocPhanByGiangVien();
        if (response.isSuccess && response.data) {
          setLopHocPhan(response.data);
        } else {
          setError(response.message || "Lỗi khi lấy dữ liệu");
          setLopHocPhan(undefined);
        }
      } catch (e) {
        setError("Lỗi hệ thống");
        setLopHocPhan(undefined);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [reloadFlag]);

  const reload = () => setReloadFlag((prev) => prev + 1);

  return { lopHocPhan, loading, error, reload };
};
