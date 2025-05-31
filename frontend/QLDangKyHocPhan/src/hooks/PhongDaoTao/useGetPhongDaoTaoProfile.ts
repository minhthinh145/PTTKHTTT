import { useState, useEffect, useCallback } from "react";
import type { PhongDaoTaoProfile } from "../../apis/types/phongdaotao";
import { getPhongDaoTaoProfile } from "../../apis/services/userServices";
import type { ApiResponse } from "../../apis/types/user";

interface UseGetPhongDaoTaoProfileResult {
  user: PhongDaoTaoProfile | null;
  loading: boolean;
  error: string | null;
  reloadProfile: () => Promise<void>;
}

export function useGetPhongDaoTaoProfile(
  getValidToken: () => Promise<string | null>
): UseGetPhongDaoTaoProfileResult {
  const [user, setUser] = useState<PhongDaoTaoProfile | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const loadUserProfile = useCallback(async () => {
    const storedToken = localStorage.getItem("accessToken");
    if (!storedToken) {
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

      const response: ApiResponse<PhongDaoTaoProfile> = await getPhongDaoTaoProfile(token);
      if (response.data) {
        setUser(response.data);
        localStorage.setItem("phongDaoTao", JSON.stringify(response.data));
      } else {
        setError("Tài khoản không phải phòng đào tạo");
        setUser(null);
      }
    } catch (err: any) {
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