import React, { createContext, useState, useEffect, useContext } from "react";
import type { ReactNode } from "react";
import { callRefreshToken } from "../apis/services/refreshTokenService";
import { signOut } from "../apis/services/authServices";
import { jwtDecode } from "jwt-decode";
import { getProfile } from "../apis/services/userServices";
import { useNavigate } from "react-router-dom";

// -------------------- Type definitions --------------------

interface User {
  username?: string;
  [key: string]: any;
}

interface AuthContextType {
  user: User | null;
  accessToken: string | null;
  refreshToken: string | null;
  login: (
    username: string,
    accessToken: string,
    refreshToken: string
  ) => Promise<void>;
  logout: () => Promise<void>;
  getValidToken: () => Promise<string | null>;
  setUser: React.Dispatch<React.SetStateAction<User | null>>;
}

interface AuthProviderProps {
  children: ReactNode;
}

// -------------------- Context creation --------------------

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined
);

// -------------------- Provider implementation --------------------

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  // Khởi tạo state lấy token từ localStorage (fallback)
  const [user, setUser] = useState<User | null>(() => {
    const storedUser = localStorage.getItem("user");
    return storedUser ? JSON.parse(storedUser) : null;
  });

  const [accessToken, setAccessToken] = useState<string | null>(() =>
    localStorage.getItem("accessToken")
  );
  const [refreshToken, setRefreshToken] = useState<string | null>(() =>
    localStorage.getItem("refreshToken")
  );

  const navigate = useNavigate();

  useEffect(() => {
    // Nếu có accessToken nhưng chưa có user, tải profile
    if (accessToken && !user) {
      loadUserProfile(accessToken);
    }
  }, [accessToken]);

  // loadUserProfile giữ nguyên
  const loadUserProfile = async (token: string) => {
    try {
      const response = await getProfile(token);

      if (response.data) {
        setUser(response.data);
        localStorage.setItem("user", JSON.stringify(response.data));
      } else {
        throw new Error(response.message || "No profile data");
      }
    } catch (err) {
      console.error("Error loading user profile:", err);
      await logout();
    }
  };
  // Hàm getValidToken chỉnh theo ý bạn, đọc fallback từ localStorage nếu state null
  const getValidToken = async (): Promise<string | null> => {
    let token = accessToken || localStorage.getItem("accessToken");
    let rToken = refreshToken || localStorage.getItem("refreshToken");

    if (!token || !rToken) {
      console.warn("No accessToken or refreshToken available");
      return null;
    }

    try {
      const decoded: { exp: number } = jwtDecode(token);
      const currentTime = Date.now() / 1000;

      if (decoded.exp > currentTime) {
        return token;
      }

      const refreshResponse = await callRefreshToken({ refreshToken: rToken });

      if (refreshResponse.data?.accessToken) {
        const newAccessToken = refreshResponse.data.accessToken;
        setAccessToken(newAccessToken);
        localStorage.setItem("accessToken", newAccessToken);
        await loadUserProfile(newAccessToken);
        return newAccessToken;
      } else {
        throw new Error(refreshResponse.message || "Failed to refresh token");
      }
    } catch (err) {
      await logout();
      return null;
    }
  };

  // login, logout giữ nguyên
  const login = async (
    username: string,
    newAccessToken: string,
    newRefreshToken: string
  ) => {
    try {
      setAccessToken(newAccessToken);
      setRefreshToken(newRefreshToken);
      setUser({ username });

      localStorage.setItem("accessToken", newAccessToken);
      localStorage.setItem("refreshToken", newRefreshToken);
      localStorage.setItem("user", JSON.stringify({ username }));

      await loadUserProfile(newAccessToken);
    } catch (err) {
      await logout();
    }
  };

  const logout = async () => {
    if (refreshToken) {
      try {
        await signOut(refreshToken);
      } catch (err) {}
    }

    setUser(null);
    setAccessToken(null);
    setRefreshToken(null);

    localStorage.removeItem("user");
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");

    navigate("/");
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        accessToken,
        refreshToken,
        login,
        logout,
        getValidToken,
        setUser,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

// -------------------- Custom hook for easier usage --------------------

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
