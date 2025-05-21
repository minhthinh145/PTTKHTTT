// import { useState } from "react";
import { LoginPage } from "./pages/LoginPage";
import "./App.css";
import "./index.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/login" element={<LoginPage />} />
          {/* Add other routes here */}
        </Routes>
      </Router>
    </>
  );
}

export default App;
