// import React from "react";
import { LeftSide } from "../components/Auth/LoginPage/LeftSide";
import { RightSide } from "../components/Auth/LoginPage/RightSide";
export const LoginPage = () => {
  return (
    <div className="bg-[#effafd]">
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
