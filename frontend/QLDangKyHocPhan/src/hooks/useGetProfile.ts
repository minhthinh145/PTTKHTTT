import { useState, useEffect, useCallback } from "react";
import { getProfile } from "../apis/services/userServices";
import type { User, ApiResponse } from "../apis/types/user";

interface UseGetProfileResult {
  user: User | null;
  loading: boolean;
  error: string | null;
  reloadProfile: () => Promise<void>;
}

export function useGetProfile(
  getValidToken: () => Promise<string | null>
): UseGetProfileResult {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const loadUserProfile = useCallback(async () => {
    const storedToken = localStorage.getItem("accessToken");
    if (!storedToken) {
      console.warn("useGetProfile: No token in localStorage");
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

      const response: ApiResponse<User> = await getProfile(token);
      if (response.data) {
        setUser(response.data);
        localStorage.setItem("user", JSON.stringify(response.data));
      } else {
        setError(response.message ?? "Failed to load profile");
        setUser(null);
      }
    } catch (err: any) {
      console.error("useGetProfile error:", err);
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
