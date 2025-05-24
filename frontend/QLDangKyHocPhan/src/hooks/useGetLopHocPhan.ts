import { useEffect, useState } from "react";
import { callGetAllLopHocPhan } from "../apis/services/lopHocPhan";
import type { LopHocPhanDTO } from "../apis/types/lopHocPhan";

export const useLopHocPhan = () => {
  const [lopHocPhan, setLopHocPhan] = useState<LopHocPhanDTO[] | undefined>(
    undefined
  );
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchLopHocPhan = async () => {
      try {
        setLoading(true);
        const res = await callGetAllLopHocPhan();
        if (res.isSuccess && res.data) {
          setLopHocPhan(res.data);
        } else {
          setError(res.message || "Không thể tải lớp học phần");
        }
      } catch (err: any) {
        setError(err.message || "Đã xảy ra lỗi");
      } finally {
        setLoading(false);
      }
    };

    fetchLopHocPhan();
  }, []);

  return {
    lopHocPhan,
    loading,
    error,
  };
};
