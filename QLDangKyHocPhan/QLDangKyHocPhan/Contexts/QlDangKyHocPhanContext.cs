using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Models;
using YourProjectNamespace.Models;
namespace QLDangKyHocPhan.Contexts;

public partial class QlDangKyHocPhanContext : IdentityDbContext<Taikhoan>
{
    public QlDangKyHocPhanContext()
    {
    }

    public QlDangKyHocPhanContext(DbContextOptions<QlDangKyHocPhanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Giangvien> Giangviens { get; set; }

    public virtual DbSet<Hocphan> Hocphans { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lophocphan> Lophocphans { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<DangKy> DangKys { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN");
        });

        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_VAI_TRO");
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_VAI_TRO_MAP");
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_CLAIM");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_LOGIN");
        });

        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_VAI_TRO_CLAIM");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable(name: "TAIKHOAN_TOKEN");
        });
        modelBuilder.Entity<Giangvien>(entity =>
        {
            entity.HasKey(e => e.MaGiangVien);

            entity.Property(e => e.MaGiangVien).HasMaxLength(10);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.LopHoc).HasMaxLength(50);
            entity.Property(e => e.TaiKhoanDangKy).HasMaxLength(450);

            entity.HasOne(d => d.TaiKhoanDangKyNavigation).WithMany(p => p.Giangviens)
                .HasForeignKey(d => d.TaiKhoanDangKy);
        });

        modelBuilder.Entity<DangKy>()
    .HasOne(d => d.SinhVien)
    .WithMany() // hoặc .WithMany(sv => sv.DangKys) nếu bạn khai báo ngược
    .HasForeignKey(d => d.MaSinhVien);

        modelBuilder.Entity<DangKy>()
            .HasOne(d => d.LopHocPhan)
            .WithMany()
            .HasForeignKey(d => d.MaLopHP);


        modelBuilder.Entity<Hocphan>(entity =>
        {
            entity.HasKey(e => e.MaHocPhan).HasName("PK__HOCPHAN__9A13F25E14D4648A");

            entity.ToTable("HOCPHAN");

            entity.Property(e => e.MaHocPhan).HasMaxLength(10);
            entity.Property(e => e.DkthucHanh)
                .HasMaxLength(50)
                .HasColumnName("DKThucHanh");
            entity.Property(e => e.SoTc).HasColumnName("SoTC");
            entity.Property(e => e.TenHocPhan).HasMaxLength(100);
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__KHOA__65390405BDF59709");

            entity.ToTable("KHOA");

            entity.Property(e => e.MaKhoa).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.TenKhoa).HasMaxLength(100);
        });

        modelBuilder.Entity<Lophocphan>(entity =>
        {
            entity.HasKey(e => e.MaLopHocPhan).HasName("PK__LOPHOCPH__82581CD9FD3C38B2");

            entity.ToTable("LOPHOCPHAN");

            entity.Property(e => e.MaLopHocPhan).HasMaxLength(10);
            entity.Property(e => e.MaGiangVien).HasMaxLength(10);
            entity.Property(e => e.MaHocPhan).HasMaxLength(10);
            entity.Property(e => e.NgayHoc).HasMaxLength(50);
            entity.Property(e => e.PhongHoc).HasMaxLength(50);

            entity.HasOne(d => d.MaGiangVienNavigation).WithMany(p => p.Lophocphans)
                .HasForeignKey(d => d.MaGiangVien)
                .HasConstraintName("FK__LOPHOCPHA__MaGia__5BE2A6F2");


        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien);

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaKhoa).HasMaxLength(10);
            entity.Property(e => e.TaiKhoanId).HasMaxLength(450);

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.MaKhoa);

            entity.HasOne(d => d.TaiKhoan).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.TaiKhoanId);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(e => e.Token).IsUnique();
            entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
