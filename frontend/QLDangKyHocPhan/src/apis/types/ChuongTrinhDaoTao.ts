export interface CTDAOTAO {
  maCT: string;
  tenCTDT: string;
  namHoc: string;
}
export interface ChiTietCTDTDTO {
  MaCT_CTDT: string;
  MaCT: string;
  MaHocPhan: string;
  TenHocPhan: string;
  SoTinChi?: number | null;
}

export interface ServiceResult<T> {
  message?: string;
  isSuccess: boolean;
  data: T;
}
