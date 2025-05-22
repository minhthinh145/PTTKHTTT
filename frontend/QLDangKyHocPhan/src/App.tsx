import { LoginPage } from "./pages/LoginPage";
import "./App.css";
import "./index.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import { AuthProvider } from "./contexts/AuthContext";

function App() {
  return (
    <Router>
      <AuthProvider>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/HomePage" element={<HomePage />} />
          {/* Add other routes here */}
        </Routes>
      </AuthProvider>
    </Router>
  );
}

export default App;
