using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Data.Configurations
{
    public class ChiTieuConfiguration : IEntityTypeConfiguration<ChiTieu>
    {
        public void Configure(EntityTypeBuilder<ChiTieu> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__ChiTieu__3214EC0748AA8F80");
            builder.ToTable("ChiTieu", tb => tb.HasTrigger("trg_CanhBaoVuotHanMuc"));

            builder.Property(e => e.TenChiTieu)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.SoTien)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.NgayChi)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("datetime2");

            builder.Property(e => e.GhiChu)
                .HasMaxLength(500);

            builder.HasOne(d => d.DanhMuc)
                .WithMany(p => p.ChiTieus)
                .HasForeignKey(d => d.DanhMucId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__ChiTieu__DanhMuc__440B1D61");

            builder.HasOne(d => d.NguoiDung)
                .WithMany(p => p.ChiTieus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ChiTieu__NguoiDu__4316F928");

            // Indexes
            builder.HasIndex(e => e.NgayChi).HasDatabaseName("IX_ChiTieu_NgayChi");
            builder.HasIndex(e => new { e.NguoiDungId, e.NgayChi }).HasDatabaseName("IX_ChiTieu_NguoiDung_NgayChi");
        }
    }
}