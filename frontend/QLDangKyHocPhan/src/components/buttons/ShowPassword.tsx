import { HiEye } from "react-icons/hi2";
import type { FC } from "react";

interface ShowPasswordProps {
  id: string;
}

export const ShowPassword: FC<ShowPasswordProps> = ({ id }) => {
  const handleToggle = () => {
    const passwordInput = document.getElementById(
      id
    ) as HTMLInputElement | null;
    if (passwordInput) {
      passwordInput.type =
        passwordInput.type === "password" ? "text" : "password";
    }
  };

  return (
    <span
      className="absolute inset-y-0 right-3 flex items-center text-gray-400 cursor-pointer"
      onClick={handleToggle}
    >
      <HiEye className="text-xl" />
    </span>
  );
};
