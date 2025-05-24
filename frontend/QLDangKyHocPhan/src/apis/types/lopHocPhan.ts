export interface LopHocPhanDTO {
  maLopHocPhan: string;
  phongHoc?: string | null;
  ngayBatDau?: string | null; // ISO string
  ngayKetThuc?: string | null;
  soLuong?: number | null;
  maHocPhan?: string | null;
  soLuongDangKy?: number | null;
  // Thông tin học phần mở rộng
  tenHocPhan?: string | null;
  soTinChi?: number | null;
  loaiHocPhan?: string | null;

  maGiangVien?: string | null;
  tenGiangVien?: string | null;
}
export interface DangKyDTO {
  maHocPhan: string;
  maLopHP: string;
  tenHocPhan: string;
  ngayThucHien: string; // ISO string
  loaiDangKy: string; // "Đăng ký" | "Hủy đăng ký"
}

export interface ApiResponse<T> {
  data?: T;
  message?: string;
  status: number;
  isSuccess?: boolean;
}
