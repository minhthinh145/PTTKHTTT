export interface User {
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
  message?: string;
  status: number;
}
