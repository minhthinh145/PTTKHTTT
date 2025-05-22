import React, { useState } from "react";
import { HiEye, HiEyeSlash } from "react-icons/hi2";

interface Props {
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export const PasswordField = ({ value, onChange }: Props) => {
  const [show, setShow] = useState(false);
  const toggleShow = () => setShow((prev) => !prev);

  return (
    <div className="relative">
      <input
        id="password"
        type={show ? "text" : "password"}
        value={value}
        onChange={onChange}
        placeholder=" "
        className="peer block w-full rounded-lg border-2 border-blue-800 px-4 pt-6 pb-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-600"
      />
      <label
        htmlFor="password"
        className="absolute left-3 top-1 text-xs text-blue-800 transition-all peer-placeholder-shown:top-4 peer-placeholder-shown:text-sm peer-placeholder-shown:text-gray-500 peer-focus:top-1 peer-focus:text-xs peer-focus:text-blue-800"
      >
        Mật khẩu
      </label>
      <span
        className="absolute inset-y-0 right-3 flex items-center text-gray-400 cursor-pointer"
        onClick={toggleShow}
      >
        {show ? (
          <HiEyeSlash className="text-xl" />
        ) : (
          <HiEye className="text-xl" />
        )}
      </span>
    </div>
  );
};
