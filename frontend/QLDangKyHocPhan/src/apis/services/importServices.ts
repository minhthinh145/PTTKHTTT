import axiosInstance from "../axiosConfig";

export const importUsers = async (file: File) => {
  const formData = new FormData();
  formData.append("file", file);
  const response = await axiosInstance.post("/api/import/import-users", formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });
  return response.data;
};