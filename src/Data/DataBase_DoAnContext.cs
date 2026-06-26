using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuanLyChiTieu.Data.SeedData;
using QuanLyChiTieu.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyChiTieu.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        #region Constructor

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #endregion

        #region DbSets

        public virtual DbSet<ChiTieu> ChiTieus { get; set; }
        public virtual DbSet<ChiTieuTheoLich> ChiTieuTheoLiches { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<GioiHanChiTieu> GioiHanChiTieus { get; set; }
        public virtual DbSet<LichSuNhanNhac> LichSuNhanNhacs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Otp> Otps { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<TuKhoaDanhMuc> TuKhoaDanhMucs { get; set; }
        public virtual DbSet<VwThongKeChiTieu> VwThongKeChiTieus { get; set; }

        #endregion

        #region Configuration Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = GetConnectionString();

                optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyChiTieuConfiguration(modelBuilder);
            ApplyChiTieuTheoLichConfiguration(modelBuilder);
            ApplyDanhMucConfiguration(modelBuilder);
            ApplyGioiHanChiTieuConfiguration(modelBuilder);
            ApplyLichSuNhanNhacConfiguration(modelBuilder);
            ApplyNguoiDungConfiguration(modelBuilder);
            ApplyOtpConfiguration(modelBuilder);
            ApplyThongBaoConfiguration(modelBuilder);
            ApplyTuKhoaDanhMucConfiguration(modelBuilder);
            ApplyVwThongKeChiTieuConfiguration(modelBuilder);

            ApplySeedData(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        private void ApplyChiTieuConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTieu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ChiTieu__3214EC0748AA8F80");
                entity.ToTable("ChiTieu", tb => tb.HasTrigger("trg_CanhBaoVuotHanMuc"));

                entity.Property(e => e.TenChiTieu).IsRequired().HasMaxLength(255);
                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.NgayChi).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.HasOne(d => d.DanhMuc)
                    .WithMany(p => p.ChiTieus)
                    .HasForeignKey(d => d.DanhMucId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__ChiTieu__DanhMuc__440B1D61");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.ChiTieus)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTieu__NguoiDu__4316F928");

                entity.HasIndex(e => e.NgayChi)
                    .HasDatabaseName("IX_ChiTieu_NgayChi");

                entity.HasIndex(e => e.NguoiDungId)
                    .HasDatabaseName("IX_ChiTieu_NguoiDungId");

                entity.HasIndex(e => new { e.NguoiDungId, e.NgayChi })
                    .HasDatabaseName("IX_ChiTieu_NguoiDung_NgayChi");
            });
        }

        private void ApplyChiTieuTheoLichConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTieuTheoLich>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ChiTieuT__3214EC07DDC555D6");
                entity.ToTable("ChiTieuTheoLich");

                entity.Property(e => e.TenChiTieu).IsRequired().HasMaxLength(255);
                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)").IsRequired();

                entity.Property(e => e.HoatDong)
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.Property(e => e.GhiChu).HasMaxLength(500);

                entity.HasOne(d => d.DanhMuc)
                    .WithMany(p => p.ChiTieuTheoLiches)
                    .HasForeignKey(d => d.DanhMucId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__ChiTieuTh__DanhM__71D1E811");

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.ChiTieuTheoLiches)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTieuTh__Nguoi__70DDC3D8");

                entity.HasIndex(e => e.NguoiDungId)
                    .HasDatabaseName("IX_ChiTieuTheoLich_NguoiDungId");

                entity.HasIndex(e => e.NgayThucHien)
                    .HasDatabaseName("IX_ChiTieuTheoLich_NgayThucHien");
            });
        }

        private void ApplyDanhMucConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__DanhMuc__3214EC07C2C35FB3");
                entity.ToTable("DanhMuc");

                entity.Property(e => e.TenDanhMuc).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MauSac).HasMaxLength(20);
                entity.Property(e => e.TuDongPhanLoai).HasDefaultValue(true);

                entity.HasIndex(e => e.TenDanhMuc)
                    .HasDatabaseName("IX_DanhMuc_TenDanhMuc");
            });
        }

        private void ApplyGioiHanChiTieuConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GioiHanChiTieu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__GioiHanC__3214EC078D4B0F17");
                entity.ToTable("GioiHanChiTieu");

                entity.Property(e => e.SoTienToiDa)
                    .HasColumnType("decimal(18, 2)")
                    .IsRequired();

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.GioiHanChiTieus)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__GioiHanCh__Nguoi__46E78A0C");

                entity.HasIndex(e => new { e.NguoiDungId, e.Thang, e.Nam })
                    .IsUnique()
                    .HasDatabaseName("IX_GioiHanChiTieu_NguoiDung_Thang_Nam");

                entity.HasIndex(e => new { e.Thang, e.Nam })
                    .HasDatabaseName("IX_GioiHanChiTieu_Thang_Nam");

                entity.HasIndex(e => e.NguoiDungId)
                    .HasDatabaseName("IX_GioiHanChiTieu_NguoiDungId");
            });
        }

        private void ApplyLichSuNhanNhacConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LichSuNhanNhac>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LichSuNh__3214EC07A3A7D1EB");
                entity.ToTable("LichSuNhanNhac");

                entity.Property(e => e.TieuDe).IsRequired().HasMaxLength(255);
                entity.Property(e => e.NoiDung).IsRequired();
                entity.Property(e => e.LoaiNhanNhac).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NgayGui).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.TrangThai).HasDefaultValue(true);

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.LichSuNhanNhacs)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__LichSuNha__Nguoi__571DF1D5");

                entity.HasIndex(e => e.NgayGui)
                    .HasDatabaseName("IX_LichSuNhanNhac_NgayGui");

                entity.HasIndex(e => e.NguoiDungId)
                    .HasDatabaseName("IX_LichSuNhanNhac_NguoiDungId");
            });
        }

        private void ApplyNguoiDungConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC07CF5C7E6E");
                entity.ToTable("NguoiDung");

                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(255);
                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.AvatarUrl).HasMaxLength(512);
                entity.Property(e => e.TanSuatNhanNhac).HasMaxLength(10);
                entity.Property(e => e.NgayDangKy).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.LoaiTaiKhoan).HasDefaultValue(0);
                entity.Property(e => e.NhanEmailNhacNho).HasDefaultValue(true);
                entity.Property(e => e.TanSuatNhanNhac).HasDefaultValue("HangTuan");
                entity.Property(e => e.GioNhanNhac).HasDefaultValue(new TimeOnly(18, 0, 0));

                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("UQ__NguoiDun__A9D10534A0F8E828");
            });
        }

        private void ApplyOtpConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Otp>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OTP__3214EC0766DF5851");
                entity.ToTable("OTP");

                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.MaOtp).HasColumnName("MaOTP").IsRequired().HasMaxLength(10);
                entity.Property(e => e.ThoiGianTao).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.TrangThai).HasDefaultValue(false);

                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("IX_OTP_Email");

                entity.HasIndex(e => e.MaOtp)
                    .HasDatabaseName("IX_OTP_MaOTP");
            });
        }

        private void ApplyThongBaoConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ThongBao__3214EC07F56E0324");
                entity.ToTable("ThongBao");

                entity.Property(e => e.NoiDung).IsRequired();
                entity.Property(e => e.NgayGui).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.DaDoc).HasDefaultValue(false);

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ThongBao__NguoiD__4BAC3F29");

                entity.HasIndex(e => e.NguoiDungId)
                    .HasDatabaseName("IX_ThongBao_NguoiDungId");

                entity.HasIndex(e => e.NgayGui)
                    .HasDatabaseName("IX_ThongBao_NgayGui");
            });
        }

        private void ApplyTuKhoaDanhMucConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TuKhoaDanhMuc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TuKhoaDa__3214EC077923D733");
                entity.ToTable("TuKhoaDanhMuc");

                entity.Property(e => e.TuKhoa).IsRequired().HasMaxLength(100);

                entity.HasOne(d => d.DanhMuc)
                    .WithMany(p => p.TuKhoaDanhMucs)
                    .HasForeignKey(d => d.DanhMucId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TuKhoaDan__DanhM__52593CB8");

                entity.HasIndex(e => e.TuKhoa)
                    .HasDatabaseName("IX_TuKhoaDanhMuc_TuKhoa");

                entity.HasIndex(e => e.DanhMucId)
                    .HasDatabaseName("IX_TuKhoaDanhMuc_DanhMucId");
            });
        }

        private void ApplyVwThongKeChiTieuConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwThongKeChiTieu>(entity =>
            {
                entity.ToView("vw_ThongKeChiTieu");
                entity.HasNoKey();
            });
        }

        private void ApplySeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        private string GetConnectionString()
        {
            var connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (!string.IsNullOrWhiteSpace(connString))
            {
                return connString;
            }

            return "Server=DESKTOP-2D7U6LA\\SQLEXPRESS;Database=DataBase_DoAnvenha;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #endregion

        #region Save Changes Overrides

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatus();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatus();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatus()
        {
            // Add soft delete logic here if needed.
        }

        #endregion

        #region Transaction Methods

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        #endregion
    }
}