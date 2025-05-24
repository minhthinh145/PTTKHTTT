export type Role = "SinhVien" | "GiangVien";

export interface SinhVienProfile {
  role: "SinhVien";
  maSinhVien: string;
  hoTen: string;
  ngaySinh: string;
  gioiTinh: string;
  email: string;
  maKhoa: string;
  tenKhoa: string;
  maCT: string;
  tenCTDT: string;
}

export interface GiangVienProfile {
  role: "GiangVien";
  maGiangVien: string;
  hoTen: string;
  diaChi: string;
  lopHoc: string;
  email: string;
}

export type User = SinhVienProfile | GiangVienProfile;

export interface ApiResponse<T> {
  data?: T;
  status: number;
  message?: string;
  isSuccess?: boolean;
}
