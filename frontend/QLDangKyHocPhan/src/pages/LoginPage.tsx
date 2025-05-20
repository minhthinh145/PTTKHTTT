import React from "react";
import { LeftSide } from "../components/LoginPage/LeftSide";
import { RightSide } from "../components/LoginPage/RightSide";
export const LoginPage = () => {
  return (
    <div className="bg-[#e8f4f8]">
      <div className="flex flex-col md:flex-row h-screen">
        <div className="hidden md:flex flex-1">
          <LeftSide />
        </div>
        <div className="flex-1 flex items-start justify-center p-4">
          <RightSide />
        </div>{" "}
      </div>
    </div>
  );
};
