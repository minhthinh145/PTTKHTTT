export type Role = "SinhVien" | "GiangVien";

export interface SinhVienProfile {
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

export interface ApiResponse<T> {
  data?: T;
  status: number;
  message?: string;
  isSuccess?: boolean;
}
