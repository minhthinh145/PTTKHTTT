import { LoginPage } from "./pages/LoginPage";
import "./App.css";
import "./index.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/SinhVienPage";
import { AuthProvider } from "./contexts/AuthContext";
import GiangVienPage from "./pages/GiangVienPage";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { GiangVienDanhSachSinhVien } from "./components/GiangVienPage/GiangVienDanhSachSinhVien";

function App() {
  return (
    <Router>
      <AuthProvider>
        <ToastContainer />
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/HomePage" element={<HomePage />} />
          <Route path="/SinhVien/Home" element={<HomePage />} />
          <Route path="/GiangVien/Home" element={<GiangVienPage />} />
          <Route
            path="/giangvien/lophocphan/:maLopHocPhan"
            element={<GiangVienDanhSachSinhVien />}
          />
          {/* Add other routes here */}
        </Routes>
      </AuthProvider>
    </Router>
  );
}

export default App;
