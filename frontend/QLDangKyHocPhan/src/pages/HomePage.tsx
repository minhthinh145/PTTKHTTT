import Sidebar from "../components/HomePage/Sidebar";
import Header from "../components/HomePage/Header";
import MainContent from "../components/HomePage/MainContent";
import { useState, useEffect } from "react";
import { useContext } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useGetProfile } from "../hooks/useGetProfile";

function HomePage() {
  const { getValidToken } = useAuth();

  const { user, loading, error, reloadProfile } =  useGetProfile(getValidToken);

  const [activeTab, setActiveTab] = useState("register");

  useEffect(() => {
    console.log("User from useGetProfile:", user);
  }, [user]);

  return (
    <div className="flex h-screen ">
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} />
      <div className="flex-1 flex flex-col">
        <Header />
        <MainContent activeTab={activeTab} />
      </div>
    </div>
  );
}

export default HomePage;
