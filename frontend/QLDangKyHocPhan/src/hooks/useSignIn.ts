import { useState, useCallback } from "react";
import { signIn as signInApi } from "../apis";
import type {
  SignInDTO,
  TokenResponseDTO,
  ApiResponse,
} from "../apis/types/auth";

interface UseSignInResult {
  signIn: (signInData: SignInDTO) => Promise<ApiResponse<TokenResponseDTO>>;
  error: string | null;
  loading: boolean;
}

export const useSignIn = (): UseSignInResult => {
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const signIn = useCallback(async (signInData: SignInDTO) => {
    setLoading(true);
    setError(null);
    try {
      const response = await signInApi(signInData);
      return response;
    } catch (err: any) {
      const message = err.message || "Sign in failed";
      setError(message);
      return {
        status: err.status || 500,
        message,
      } as ApiResponse<TokenResponseDTO>;
    } finally {
      setLoading(false);
    }
  }, []);

  return { signIn, error, loading };
};
