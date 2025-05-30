import { useState, useEffect, useCallback } from "react";
import type { GiangVienProfile } from "../../apis/types/giangvien";
import { getGiangVienProfile } from "../../apis/services/userServices";
import type { ApiResponse } from "../../apis/types/user";

interface UseGetGiangVienProfileResult {
  user: GiangVienProfile | null;
  loading: boolean;
  error: string | null;
  reloadProfile: () => Promise<void>;
}

export function useGetGiangVienProfile(
  getValidToken: () => Promise<string | null>
): UseGetGiangVienProfileResult {
  const [user, setUser] = useState<GiangVienProfile | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const loadUserProfile = useCallback(async () => {
    const storedToken = localStorage.getItem("accessToken");
    if (!storedToken) {
      console.warn("No token in localStorage");
      setError("No token available");
      setLoading(false);
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const token = await getValidToken();

      if (!token) {
        setError("No valid token");
        setUser(null);
        setLoading(false);
        return;
      }

      const response: ApiResponse<GiangVienProfile> = await getGiangVienProfile(
        token
      );
      if (response.data) {
        setUser(response.data);
        localStorage.setItem("giangVien", JSON.stringify(response.data));
      } else {
        setError("Tài khoản không phải giảng viên");
        setUser(null);
      }
    } catch (err: any) {
      console.error("useGetGiangVienProfile error:", err);
      setError(err?.message ?? "Unexpected error");
      setUser(null);
    } finally {
      setLoading(false);
    }
  }, [getValidToken]);

  useEffect(() => {
    const storedToken = localStorage.getItem("accessToken");
    if (storedToken) {
      loadUserProfile();
    } else {
      setLoading(false);
      setError("No token found in localStorage");
    }
  }, [loadUserProfile]);

  return { user, loading, error, reloadProfile: loadUserProfile };
}
