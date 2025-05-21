// import React from "react";
import Sidebar from "../components/HomePage/SideBar";
import Header from "../components/HomePage/Header";
import MainContent from "../components/HomePage/MainContent";
import { useState } from "react";

function HomePage() {
  const [activeTab, setActiveTab] = useState("register"); // default tab
  return (
    <div className="flex h-screen">
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} />
      <div className="flex-1 flex flex-col">
        <Header />
        <MainContent activeTab={activeTab} />
      </div>
    </div>
  );
}

export default HomePage;
