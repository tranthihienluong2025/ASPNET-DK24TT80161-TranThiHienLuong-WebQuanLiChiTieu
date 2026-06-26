using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Data.SeedData
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMuc>().HasData(GetDanhMucs());
            modelBuilder.Entity<NguoiDung>().HasData(GetNguoiDungs());
            modelBuilder.Entity<GioiHanChiTieu>().HasData(GetGioiHanChiTieus());
            modelBuilder.Entity<ChiTieu>().HasData(GetChiTieus());
            modelBuilder.Entity<ChiTieuTheoLich>().HasData(GetChiTieuTheoLiches());
            modelBuilder.Entity<ThongBao>().HasData(GetThongBaos());
            modelBuilder.Entity<LichSuNhanNhac>().HasData(GetLichSuNhanNhacs());
            modelBuilder.Entity<Otp>().HasData(GetOtps());
            modelBuilder.Entity<TuKhoaDanhMuc>().HasData(GetTuKhoaDanhMucs());
        }

        private static List<DanhMuc> GetDanhMucs()
        {
            return new List<DanhMuc>
            {
                new DanhMuc { Id = 1, TenDanhMuc = "Ăn uống", TuDongPhanLoai = true, MauSac = "#FF6384" },
                new DanhMuc { Id = 2, TenDanhMuc = "Di chuyển", TuDongPhanLoai = true, MauSac = "#36A2EB" },
                new DanhMuc { Id = 3, TenDanhMuc = "Mua sắm", TuDongPhanLoai = true, MauSac = "#FFCE56" },
                new DanhMuc { Id = 4, TenDanhMuc = "Giải trí", TuDongPhanLoai = true, MauSac = "#4BC0C0" },
                new DanhMuc { Id = 5, TenDanhMuc = "Hóa đơn", TuDongPhanLoai = true, MauSac = "#9966FF" },
                new DanhMuc { Id = 6, TenDanhMuc = "Sức khỏe", TuDongPhanLoai = true, MauSac = "#FF9F40" },
                new DanhMuc { Id = 7, TenDanhMuc = "Giáo dục", TuDongPhanLoai = true, MauSac = "#FFCD56" },
                new DanhMuc { Id = 8, TenDanhMuc = "Du lịch", TuDongPhanLoai = true, MauSac = "#C9CBCF" },
                new DanhMuc { Id = 9, TenDanhMuc = "Cà phê", TuDongPhanLoai = true, MauSac = "#8E5EA2" },
                new DanhMuc { Id = 10, TenDanhMuc = "Đầu tư", TuDongPhanLoai = true, MauSac = "#3D9970" }
            };
        }

        private static List<NguoiDung> GetNguoiDungs()
        {
            const string passwordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f";

            return new List<NguoiDung>
            {
                new NguoiDung
                {
                    Id = 1,
                    Email = "admin@qlct.com",
                    MatKhau = passwordHash,
                    HoTen = "admin",
                    NgayDangKy = new DateTime(2026, 1, 1, 8, 0, 0),
                    LoaiTaiKhoan = 1,
                    NhanEmailNhacNho = true,
                    TanSuatNhanNhac = "HangNgay",
                    GioNhanNhac = new TimeOnly(8, 0),
                    AvatarUrl = "https://ui-avatars.com/api/?name=admin&background=00112C&color=fff"
                },
                new NguoiDung
                {
                    Id = 2,
                    Email = "user1@qlct.com",
                    MatKhau = passwordHash,
                    HoTen = "user1",
                    NgayDangKy = new DateTime(2026, 2, 10, 9, 30, 0),
                    LoaiTaiKhoan = 0,
                    NhanEmailNhacNho = true,
                    TanSuatNhanNhac = "HangNgay",
                    GioNhanNhac = new TimeOnly(20, 0),
                    AvatarUrl = "https://ui-avatars.com/api/?name=user1&background=36A2EB&color=fff"
                },
                new NguoiDung
                {
                    Id = 3,
                    Email = "user2@qlct.com",
                    MatKhau = passwordHash,
                    HoTen = "user2",
                    NgayDangKy = new DateTime(2026, 2, 15, 10, 15, 0),
                    LoaiTaiKhoan = 0,
                    NhanEmailNhacNho = true,
                    TanSuatNhanNhac = "HangTuan",
                    GioNhanNhac = new TimeOnly(19, 30),
                    AvatarUrl = "https://ui-avatars.com/api/?name=user2&background=FF6384&color=fff"
                }
            };
        }

        private static List<GioiHanChiTieu> GetGioiHanChiTieus()
        {
            return new List<GioiHanChiTieu>
            {
                new GioiHanChiTieu { Id = 1, NguoiDungId = 1, Thang = 4, Nam = 2026, SoTienToiDa = 2500000 },
                new GioiHanChiTieu { Id = 2, NguoiDungId = 2, Thang = 4, Nam = 2026, SoTienToiDa = 6500000 },
                new GioiHanChiTieu { Id = 3, NguoiDungId = 3, Thang = 4, Nam = 2026, SoTienToiDa = 3500000 },

                new GioiHanChiTieu { Id = 4, NguoiDungId = 1, Thang = 5, Nam = 2026, SoTienToiDa = 2500000 },
                new GioiHanChiTieu { Id = 5, NguoiDungId = 2, Thang = 5, Nam = 2026, SoTienToiDa = 7500000 },
                new GioiHanChiTieu { Id = 6, NguoiDungId = 3, Thang = 5, Nam = 2026, SoTienToiDa = 4000000 },

                new GioiHanChiTieu { Id = 7, NguoiDungId = 1, Thang = 6, Nam = 2026, SoTienToiDa = 3000000 },
                new GioiHanChiTieu { Id = 8, NguoiDungId = 2, Thang = 6, Nam = 2026, SoTienToiDa = 8500000 },
                new GioiHanChiTieu { Id = 9, NguoiDungId = 3, Thang = 6, Nam = 2026, SoTienToiDa = 4500000 }
            };
        }

        private static List<ChiTieu> GetChiTieus()
        {
            var chiTieus = new List<ChiTieu>();
            int id = 1;

            var expenses = new[]
            {
                // ==================== APRIL 2026 ====================
                new { Date = new DateTime(2026, 4, 1, 7, 30, 0), UserId = 2, Amount = 35000m, Name = "Ăn sáng", CategoryId = 1, Note = "Bánh mì và cà phê" },
                new { Date = new DateTime(2026, 4, 2, 18, 20, 0), UserId = 3, Amount = 120000m, Name = "Đổ xăng", CategoryId = 2, Note = "Di chuyển đầu tháng" },
                new { Date = new DateTime(2026, 4, 4, 20, 10, 0), UserId = 2, Amount = 180000m, Name = "Xem phim cuối tuần", CategoryId = 4, Note = "Vé phim và nước" },
                new { Date = new DateTime(2026, 4, 5, 9, 0, 0), UserId = 2, Amount = 3200000m, Name = "Tiền thuê phòng", CategoryId = 5, Note = "Chi phí cố định tháng 4" },
                new { Date = new DateTime(2026, 4, 7, 12, 15, 0), UserId = 3, Amount = 75000m, Name = "Ăn trưa văn phòng", CategoryId = 1, Note = "Cơm trưa" },
                new { Date = new DateTime(2026, 4, 9, 21, 0, 0), UserId = 2, Amount = 95000m, Name = "Cà phê làm việc", CategoryId = 9, Note = "Làm việc buổi tối" },
                new { Date = new DateTime(2026, 4, 10, 8, 30, 0), UserId = 1, Amount = 250000m, Name = "Internet văn phòng", CategoryId = 5, Note = "Hóa đơn tháng 4" },
                new { Date = new DateTime(2026, 4, 12, 16, 45, 0), UserId = 3, Amount = 420000m, Name = "Mua áo sơ mi", CategoryId = 3, Note = "Đồ đi làm" },
                new { Date = new DateTime(2026, 4, 14, 19, 30, 0), UserId = 2, Amount = 210000m, Name = "Ăn tối với bạn", CategoryId = 1, Note = "Quán nướng" },
                new { Date = new DateTime(2026, 4, 16, 13, 0, 0), UserId = 3, Amount = 160000m, Name = "Thuốc cảm", CategoryId = 6, Note = "Nhà thuốc gần nhà" },
                new { Date = new DateTime(2026, 4, 18, 10, 15, 0), UserId = 2, Amount = 260000m, Name = "Sách kỹ năng", CategoryId = 7, Note = "Sách học tập" },
                new { Date = new DateTime(2026, 4, 20, 11, 0, 0), UserId = 3, Amount = 300000m, Name = "Tiền điện nước", CategoryId = 5, Note = "Hóa đơn sinh hoạt" },
                new { Date = new DateTime(2026, 4, 22, 18, 40, 0), UserId = 2, Amount = 90000m, Name = "Grab về nhà", CategoryId = 2, Note = "Trời mưa" },
                new { Date = new DateTime(2026, 4, 24, 20, 0, 0), UserId = 3, Amount = 150000m, Name = "Trà sữa", CategoryId = 9, Note = "Gặp bạn cuối tuần" },
                new { Date = new DateTime(2026, 4, 26, 9, 20, 0), UserId = 2, Amount = 500000m, Name = "Đầu tư quỹ", CategoryId = 10, Note = "Góp định kỳ" },
                new { Date = new DateTime(2026, 4, 28, 17, 10, 0), UserId = 1, Amount = 680000m, Name = "Mua thiết bị nhỏ", CategoryId = 3, Note = "Phụ kiện làm việc" },

                // ==================== MAY 2026 ====================
                new { Date = new DateTime(2026, 5, 1, 8, 0, 0), UserId = 2, Amount = 3600000m, Name = "Tiền thuê phòng", CategoryId = 5, Note = "Chi phí cố định tháng 5" },
                new { Date = new DateTime(2026, 5, 2, 7, 45, 0), UserId = 3, Amount = 45000m, Name = "Ăn sáng", CategoryId = 1, Note = "Phở sáng" },
                new { Date = new DateTime(2026, 5, 4, 18, 10, 0), UserId = 2, Amount = 130000m, Name = "Đổ xăng", CategoryId = 2, Note = "Đi làm hằng ngày" },
                new { Date = new DateTime(2026, 5, 6, 20, 15, 0), UserId = 3, Amount = 220000m, Name = "Mua mỹ phẩm", CategoryId = 3, Note = "Đồ dùng cá nhân" },
                new { Date = new DateTime(2026, 5, 8, 9, 0, 0), UserId = 1, Amount = 320000m, Name = "Hóa đơn phần mềm", CategoryId = 5, Note = "Gia hạn công cụ" },
                new { Date = new DateTime(2026, 5, 10, 15, 30, 0), UserId = 2, Amount = 850000m, Name = "Đặt vé Đà Lạt", CategoryId = 8, Note = "Du lịch ngắn ngày" },
                new { Date = new DateTime(2026, 5, 12, 12, 0, 0), UserId = 3, Amount = 70000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm văn phòng" },
                new { Date = new DateTime(2026, 5, 14, 19, 45, 0), UserId = 2, Amount = 110000m, Name = "Cà phê gặp bạn", CategoryId = 9, Note = "Cuối ngày" },
                new { Date = new DateTime(2026, 5, 16, 16, 0, 0), UserId = 3, Amount = 280000m, Name = "Khám sức khỏe", CategoryId = 6, Note = "Kiểm tra định kỳ" },
                new { Date = new DateTime(2026, 5, 18, 21, 10, 0), UserId = 2, Amount = 230000m, Name = "Ăn tối gia đình", CategoryId = 1, Note = "Cuối tuần" },
                new { Date = new DateTime(2026, 5, 20, 9, 30, 0), UserId = 3, Amount = 350000m, Name = "Tiền điện nước", CategoryId = 5, Note = "Hóa đơn tháng 5" },
                new { Date = new DateTime(2026, 5, 22, 13, 15, 0), UserId = 2, Amount = 300000m, Name = "Khóa học online", CategoryId = 7, Note = "Nâng cấp kỹ năng" },
                new { Date = new DateTime(2026, 5, 24, 20, 30, 0), UserId = 3, Amount = 190000m, Name = "Xem phim", CategoryId = 4, Note = "Cuối tuần" },
                new { Date = new DateTime(2026, 5, 26, 8, 45, 0), UserId = 2, Amount = 600000m, Name = "Đầu tư quỹ", CategoryId = 10, Note = "Góp định kỳ" },
                new { Date = new DateTime(2026, 5, 28, 17, 50, 0), UserId = 1, Amount = 450000m, Name = "Tiếp khách", CategoryId = 1, Note = "Ăn tối công việc" },
                new { Date = new DateTime(2026, 5, 30, 10, 0, 0), UserId = 3, Amount = 500000m, Name = "Mua balo", CategoryId = 3, Note = "Chuẩn bị đi làm" },

                // ==================== JUNE 2026 ====================
                new { Date = new DateTime(2026, 6, 1, 7, 25, 0), UserId = 2, Amount = 40000m, Name = "Ăn sáng đầu tháng", CategoryId = 1, Note = "Bún bò" },
                new { Date = new DateTime(2026, 6, 1, 8, 20, 0), UserId = 3, Amount = 125000m, Name = "Đổ xăng", CategoryId = 2, Note = "Chuẩn bị tuần mới" },
                new { Date = new DateTime(2026, 6, 1, 9, 0, 0), UserId = 2, Amount = 3600000m, Name = "Tiền thuê phòng", CategoryId = 5, Note = "Chi phí cố định tháng 6" },
                new { Date = new DateTime(2026, 6, 1, 12, 10, 0), UserId = 3, Amount = 75000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm văn phòng" },
                new { Date = new DateTime(2026, 6, 1, 15, 30, 0), UserId = 1, Amount = 250000m, Name = "Internet văn phòng", CategoryId = 5, Note = "Hóa đơn tháng 6" },
                new { Date = new DateTime(2026, 6, 1, 19, 40, 0), UserId = 2, Amount = 95000m, Name = "Cà phê tối", CategoryId = 9, Note = "Làm việc cá nhân" },
                new { Date = new DateTime(2026, 6, 2, 7, 40, 0), UserId = 3, Amount = 45000m, Name = "Ăn sáng", CategoryId = 1, Note = "Bánh cuốn" },
                new { Date = new DateTime(2026, 6, 2, 11, 50, 0), UserId = 2, Amount = 70000m, Name = "Ăn trưa văn phòng", CategoryId = 1, Note = "Cơm phần" },
                new { Date = new DateTime(2026, 6, 2, 14, 15, 0), UserId = 3, Amount = 120000m, Name = "Mua đồ cá nhân", CategoryId = 3, Note = "Dầu gội và sữa tắm" },
                new { Date = new DateTime(2026, 6, 2, 18, 25, 0), UserId = 2, Amount = 85000m, Name = "Grab về nhà", CategoryId = 2, Note = "Tan làm muộn" },
                new { Date = new DateTime(2026, 6, 2, 20, 10, 0), UserId = 3, Amount = 59000m, Name = "Gói nghe nhạc", CategoryId = 4, Note = "Gia hạn dịch vụ tháng 6" },
                new { Date = new DateTime(2026, 6, 3, 7, 30, 0), UserId = 2, Amount = 35000m, Name = "Ăn sáng", CategoryId = 1, Note = "Bánh mì đầu ngày" },
                new { Date = new DateTime(2026, 6, 3, 12, 10, 0), UserId = 3, Amount = 70000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm văn phòng" },
                new { Date = new DateTime(2026, 6, 4, 18, 15, 0), UserId = 2, Amount = 120000m, Name = "Đổ xăng", CategoryId = 2, Note = "Đi làm hằng ngày" },
                new { Date = new DateTime(2026, 6, 4, 20, 30, 0), UserId = 3, Amount = 50000m, Name = "Trà sữa", CategoryId = 9, Note = "Giải lao buổi tối" },
                new { Date = new DateTime(2026, 6, 5, 8, 0, 0), UserId = 2, Amount = 180000m, Name = "Gửi xe tháng 6", CategoryId = 2, Note = "Phí gửi xe chung cư" },
                new { Date = new DateTime(2026, 6, 5, 19, 30, 0), UserId = 2, Amount = 150000m, Name = "Ăn tối", CategoryId = 1, Note = "Quán ăn gần nhà" },
                new { Date = new DateTime(2026, 6, 6, 9, 30, 0), UserId = 3, Amount = 220000m, Name = "Đi chợ cuối tuần", CategoryId = 1, Note = "Thực phẩm trong tuần" },
                new { Date = new DateTime(2026, 6, 6, 20, 0, 0), UserId = 2, Amount = 180000m, Name = "Xem phim", CategoryId = 4, Note = "Cuối tuần" },
                new { Date = new DateTime(2026, 6, 7, 12, 0, 0), UserId = 2, Amount = 65000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm phần" },
                new { Date = new DateTime(2026, 6, 7, 16, 0, 0), UserId = 3, Amount = 60000m, Name = "Cà phê gặp bạn", CategoryId = 9, Note = "Cuối tuần" },
                new { Date = new DateTime(2026, 6, 8, 9, 0, 0), UserId = 1, Amount = 49000m, Name = "Gói lưu trữ đám mây", CategoryId = 5, Note = "Gia hạn dung lượng" },
                new { Date = new DateTime(2026, 6, 8, 18, 20, 0), UserId = 3, Amount = 110000m, Name = "Đổ xăng", CategoryId = 2, Note = "Đi làm" },
                new { Date = new DateTime(2026, 6, 9, 7, 40, 0), UserId = 2, Amount = 40000m, Name = "Ăn sáng", CategoryId = 1, Note = "Phở sáng" },
                new { Date = new DateTime(2026, 6, 9, 21, 0, 0), UserId = 3, Amount = 320000m, Name = "Mua đồ gia dụng", CategoryId = 3, Note = "Đồ dùng nhà bếp" },
                new { Date = new DateTime(2026, 6, 10, 10, 0, 0), UserId = 2, Amount = 100000m, Name = "Nạp thẻ điện thoại", CategoryId = 5, Note = "Gói cước hằng tháng" },
                new { Date = new DateTime(2026, 6, 10, 12, 30, 0), UserId = 2, Amount = 70000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm văn phòng" },
                new { Date = new DateTime(2026, 6, 11, 14, 0, 0), UserId = 1, Amount = 320000m, Name = "Hóa đơn phần mềm", CategoryId = 5, Note = "Gia hạn công cụ làm việc" },
                new { Date = new DateTime(2026, 6, 11, 19, 30, 0), UserId = 3, Amount = 240000m, Name = "Ăn tối gia đình", CategoryId = 1, Note = "Quây quần cuối tuần" },
                new { Date = new DateTime(2026, 6, 12, 9, 15, 0), UserId = 2, Amount = 95000m, Name = "Cà phê làm việc", CategoryId = 9, Note = "Buổi sáng" },
                new { Date = new DateTime(2026, 6, 12, 16, 30, 0), UserId = 3, Amount = 300000m, Name = "Khám sức khỏe định kỳ", CategoryId = 6, Note = "Lịch khám theo tháng" },
                new { Date = new DateTime(2026, 6, 13, 12, 0, 0), UserId = 3, Amount = 70000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm phần" },
                new { Date = new DateTime(2026, 6, 13, 18, 40, 0), UserId = 2, Amount = 85000m, Name = "Grab về nhà", CategoryId = 2, Note = "Tan làm muộn" },
                new { Date = new DateTime(2026, 6, 14, 15, 0, 0), UserId = 3, Amount = 95000m, Name = "Xem phim", CategoryId = 4, Note = "Giải trí cuối tuần" },
                new { Date = new DateTime(2026, 6, 14, 20, 0, 0), UserId = 2, Amount = 210000m, Name = "Ăn tối với bạn", CategoryId = 1, Note = "Cuối tuần" },
                new { Date = new DateTime(2026, 6, 15, 9, 0, 0), UserId = 3, Amount = 150000m, Name = "Mua sách", CategoryId = 7, Note = "Sách tham khảo" },
                new { Date = new DateTime(2026, 6, 15, 13, 15, 0), UserId = 2, Amount = 300000m, Name = "Khóa học online tháng 6", CategoryId = 7, Note = "Gia hạn khóa học kỹ năng" },
                new { Date = new DateTime(2026, 6, 16, 7, 30, 0), UserId = 2, Amount = 35000m, Name = "Ăn sáng", CategoryId = 1, Note = "Bánh mì" },
                new { Date = new DateTime(2026, 6, 16, 18, 30, 0), UserId = 3, Amount = 120000m, Name = "Đổ xăng", CategoryId = 2, Note = "Đi làm" },
                new { Date = new DateTime(2026, 6, 17, 12, 0, 0), UserId = 2, Amount = 70000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm văn phòng" },
                new { Date = new DateTime(2026, 6, 17, 20, 0, 0), UserId = 3, Amount = 55000m, Name = "Trà sữa", CategoryId = 9, Note = "Giải lao" },
                new { Date = new DateTime(2026, 6, 18, 10, 0, 0), UserId = 3, Amount = 450000m, Name = "Mua thực phẩm định kỳ", CategoryId = 1, Note = "Đồ ăn cho tuần giữa tháng" },
                new { Date = new DateTime(2026, 6, 18, 19, 30, 0), UserId = 2, Amount = 90000m, Name = "Cà phê tối", CategoryId = 9, Note = "Làm việc cá nhân" },
                new { Date = new DateTime(2026, 6, 19, 10, 0, 0), UserId = 1, Amount = 180000m, Name = "Mua văn phòng phẩm", CategoryId = 3, Note = "Đồ dùng làm việc" },
                new { Date = new DateTime(2026, 6, 19, 12, 30, 0), UserId = 2, Amount = 65000m, Name = "Ăn trưa", CategoryId = 1, Note = "Cơm phần" },
                new { Date = new DateTime(2026, 6, 20, 9, 30, 0), UserId = 3, Amount = 360000m, Name = "Tiền điện nước tháng 6", CategoryId = 5, Note = "Hóa đơn sinh hoạt" },
                new { Date = new DateTime(2026, 6, 20, 11, 0, 0), UserId = 2, Amount = 280000m, Name = "Đi siêu thị", CategoryId = 3, Note = "Mua sắm cuối tuần" },
                new { Date = new DateTime(2026, 6, 20, 19, 0, 0), UserId = 3, Amount = 130000m, Name = "Ăn tối", CategoryId = 1, Note = "Cuối tuần" }
            };

            foreach (var expense in expenses)
            {
                chiTieus.Add(new ChiTieu
                {
                    Id = id++,
                    TenChiTieu = expense.Name,
                    SoTien = expense.Amount,
                    NgayChi = expense.Date,
                    GhiChu = expense.Note,
                    NguoiDungId = expense.UserId,
                    DanhMucId = expense.CategoryId
                });
            }

            return chiTieus;
        }

        private static List<ChiTieuTheoLich> GetChiTieuTheoLiches()
        {
            return new List<ChiTieuTheoLich>
            {
                new ChiTieuTheoLich
                {
                    Id = 1,
                    TenChiTieu = "Tiền thuê phòng tháng 4",
                    SoTien = 3200000m,
                    GhiChu = "Đã thanh toán tiền thuê phòng tháng 4",
                    NgayThucHien = new DateOnly(2026, 4, 5),
                    LanThucHienCuoi = new DateOnly(2026, 4, 5),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 2,
                    TenChiTieu = "Internet văn phòng tháng 4",
                    SoTien = 250000m,
                    GhiChu = "Đã thanh toán Internet tháng 4",
                    NgayThucHien = new DateOnly(2026, 4, 10),
                    LanThucHienCuoi = new DateOnly(2026, 4, 10),
                    HoatDong = false,
                    NguoiDungId = 1,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 3,
                    TenChiTieu = "Tiền điện nước tháng 4",
                    SoTien = 300000m,
                    GhiChu = "Đã thanh toán hóa đơn sinh hoạt tháng 4",
                    NgayThucHien = new DateOnly(2026, 4, 20),
                    LanThucHienCuoi = new DateOnly(2026, 4, 20),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 4,
                    TenChiTieu = "Tiền thuê phòng tháng 5",
                    SoTien = 3600000m,
                    GhiChu = "Đã thanh toán tiền thuê phòng tháng 5",
                    NgayThucHien = new DateOnly(2026, 5, 1),
                    LanThucHienCuoi = new DateOnly(2026, 5, 1),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 5,
                    TenChiTieu = "Hóa đơn phần mềm tháng 5",
                    SoTien = 320000m,
                    GhiChu = "Đã gia hạn công cụ làm việc",
                    NgayThucHien = new DateOnly(2026, 5, 8),
                    LanThucHienCuoi = new DateOnly(2026, 5, 8),
                    HoatDong = false,
                    NguoiDungId = 1,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 6,
                    TenChiTieu = "Khám sức khỏe tháng 5",
                    SoTien = 280000m,
                    GhiChu = "Đã hoàn thành lịch khám định kỳ",
                    NgayThucHien = new DateOnly(2026, 5, 16),
                    LanThucHienCuoi = new DateOnly(2026, 5, 16),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 6
                },
                new ChiTieuTheoLich
                {
                    Id = 7,
                    TenChiTieu = "Tiền điện nước tháng 5",
                    SoTien = 350000m,
                    GhiChu = "Đã thanh toán hóa đơn sinh hoạt tháng 5",
                    NgayThucHien = new DateOnly(2026, 5, 20),
                    LanThucHienCuoi = new DateOnly(2026, 5, 20),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 8,
                    TenChiTieu = "Tiền thuê phòng tháng 6",
                    SoTien = 3600000m,
                    GhiChu = "Đã thanh toán vào ngày đầu tháng",
                    NgayThucHien = new DateOnly(2026, 6, 1),
                    LanThucHienCuoi = new DateOnly(2026, 6, 1),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 9,
                    TenChiTieu = "Gói nghe nhạc tháng 6",
                    SoTien = 59000m,
                    GhiChu = "Đã gia hạn dịch vụ giải trí",
                    NgayThucHien = new DateOnly(2026, 6, 2),
                    LanThucHienCuoi = new DateOnly(2026, 6, 2),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 4
                },
                new ChiTieuTheoLich
                {
                    Id = 10,
                    TenChiTieu = "Gửi xe tháng 6",
                    SoTien = 180000m,
                    GhiChu = "Đã thanh toán phí gửi xe chung cư",
                    NgayThucHien = new DateOnly(2026, 6, 5),
                    LanThucHienCuoi = new DateOnly(2026, 6, 5),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 2
                },
                new ChiTieuTheoLich
                {
                    Id = 11,
                    TenChiTieu = "Gói lưu trữ đám mây",
                    SoTien = 49000m,
                    GhiChu = "Đã gia hạn dung lượng lưu trữ",
                    NgayThucHien = new DateOnly(2026, 6, 8),
                    LanThucHienCuoi = new DateOnly(2026, 6, 8),
                    HoatDong = false,
                    NguoiDungId = 1,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 12,
                    TenChiTieu = "Nạp thẻ điện thoại",
                    SoTien = 100000m,
                    GhiChu = "Đã nạp gói cước liên lạc hằng tháng",
                    NgayThucHien = new DateOnly(2026, 6, 10),
                    LanThucHienCuoi = new DateOnly(2026, 6, 10),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 13,
                    TenChiTieu = "Khám sức khỏe định kỳ",
                    SoTien = 300000m,
                    GhiChu = "Đã hoàn thành lịch khám theo tháng",
                    NgayThucHien = new DateOnly(2026, 6, 12),
                    LanThucHienCuoi = new DateOnly(2026, 6, 12),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 6
                },
                new ChiTieuTheoLich
                {
                    Id = 14,
                    TenChiTieu = "Khóa học online tháng 6",
                    SoTien = 300000m,
                    GhiChu = "Đã gia hạn khóa học kỹ năng",
                    NgayThucHien = new DateOnly(2026, 6, 15),
                    LanThucHienCuoi = new DateOnly(2026, 6, 15),
                    HoatDong = false,
                    NguoiDungId = 2,
                    DanhMucId = 7
                },
                new ChiTieuTheoLich
                {
                    Id = 15,
                    TenChiTieu = "Mua thực phẩm định kỳ",
                    SoTien = 450000m,
                    GhiChu = "Đã mua đồ ăn cho tuần giữa tháng",
                    NgayThucHien = new DateOnly(2026, 6, 18),
                    LanThucHienCuoi = new DateOnly(2026, 6, 18),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 1
                },
                new ChiTieuTheoLich
                {
                    Id = 16,
                    TenChiTieu = "Tiền điện nước tháng 6",
                    SoTien = 360000m,
                    GhiChu = "Đã thanh toán hóa đơn sinh hoạt tháng 6",
                    NgayThucHien = new DateOnly(2026, 6, 20),
                    LanThucHienCuoi = new DateOnly(2026, 6, 20),
                    HoatDong = false,
                    NguoiDungId = 3,
                    DanhMucId = 5
                },
                new ChiTieuTheoLich
                {
                    Id = 17,
                    TenChiTieu = "Cà phê làm việc định kỳ",
                    SoTien = 120000m,
                    GhiChu = "Dự kiến gặp bạn làm việc cuối tuần",
                    NgayThucHien = new DateOnly(2026, 6, 23),
                    LanThucHienCuoi = new DateOnly(2026, 5, 14),
                    HoatDong = true,
                    NguoiDungId = 2,
                    DanhMucId = 9
                },
                new ChiTieuTheoLich
                {
                    Id = 18,
                    TenChiTieu = "Đầu tư quỹ tháng 6",
                    SoTien = 600000m,
                    GhiChu = "Góp quỹ định kỳ cuối tháng",
                    NgayThucHien = new DateOnly(2026, 6, 26),
                    LanThucHienCuoi = new DateOnly(2026, 5, 26),
                    HoatDong = true,
                    NguoiDungId = 2,
                    DanhMucId = 10
                }
            };
        }

        private static List<ThongBao> GetThongBaos()
        {
            return new List<ThongBao>
            {
                new ThongBao
                {
                    Id = 1,
                    NoiDung = "Bạn đã sử dụng khoảng 70% hạn mức tháng 4/2026.",
                    NgayGui = new DateTime(2026, 4, 20, 20, 0, 0),
                    DaDoc = true,
                    NguoiDungId = 2
                },
                new ThongBao
                {
                    Id = 2,
                    NoiDung = "Hóa đơn điện nước tháng 4 đã được ghi nhận.",
                    NgayGui = new DateTime(2026, 4, 20, 20, 10, 0),
                    DaDoc = true,
                    NguoiDungId = 3
                },
                new ThongBao
                {
                    Id = 3,
                    NoiDung = "Bạn có lịch đầu tư quỹ vào ngày 26/05/2026.",
                    NgayGui = new DateTime(2026, 5, 25, 8, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 2
                },
                new ThongBao
                {
                    Id = 4,
                    NoiDung = "Tổng chi tiêu tháng 5 vẫn nằm trong hạn mức.",
                    NgayGui = new DateTime(2026, 5, 30, 21, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 3
                },
                new ThongBao
                {
                    Id = 5,
                    NoiDung = "Đã ghi nhận chi tiêu đến ngày 02/06/2026. Hạn mức tháng 6 vẫn còn an toàn.",
                    NgayGui = new DateTime(2026, 6, 2, 21, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 2
                },
                new ThongBao
                {
                    Id = 6,
                    NoiDung = "Bạn có 2 khoản chi tiêu theo lịch sắp tới trong tháng 6/2026.",
                    NgayGui = new DateTime(2026, 6, 2, 21, 15, 0),
                    DaDoc = true,
                    NguoiDungId = 2
                },
                new ThongBao
                {
                    Id = 7,
                    NoiDung = "Khám sức khỏe định kỳ ngày 12/06/2026 đã được ghi nhận.",
                    NgayGui = new DateTime(2026, 6, 12, 17, 0, 0),
                    DaDoc = true,
                    NguoiDungId = 3
                },
                new ThongBao
                {
                    Id = 8,
                    NoiDung = "Bạn đã sử dụng khoảng 65% hạn mức tháng 6/2026.",
                    NgayGui = new DateTime(2026, 6, 15, 20, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 2
                },
                new ThongBao
                {
                    Id = 9,
                    NoiDung = "Hóa đơn điện nước tháng 6 đã được thanh toán ngày 20/06/2026.",
                    NgayGui = new DateTime(2026, 6, 20, 10, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 3
                },
                new ThongBao
                {
                    Id = 10,
                    NoiDung = "Sắp tới hạn đầu tư quỹ định kỳ ngày 26/06/2026.",
                    NgayGui = new DateTime(2026, 6, 20, 8, 0, 0),
                    DaDoc = false,
                    NguoiDungId = 2
                }
            };
        }

        private static List<LichSuNhanNhac> GetLichSuNhanNhacs()
        {
            return new List<LichSuNhanNhac>
            {
                new LichSuNhanNhac
                {
                    Id = 1,
                    TieuDe = "Nhắc nhập chi tiêu",
                    NoiDung = "Đừng quên ghi lại chi tiêu hôm nay.",
                    NgayGui = new DateTime(2026, 4, 15, 20, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "HangNgay",
                    NguoiDungId = 2
                },
                new LichSuNhanNhac
                {
                    Id = 2,
                    TieuDe = "Nhắc thanh toán hóa đơn",
                    NoiDung = "Bạn có hóa đơn điện nước cần thanh toán.",
                    NgayGui = new DateTime(2026, 4, 20, 8, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "HoaDon",
                    NguoiDungId = 3
                },
                new LichSuNhanNhac
                {
                    Id = 3,
                    TieuDe = "Tổng kết tháng 5",
                    NoiDung = "Kiểm tra lại chi tiêu tháng 5 trước khi sang tháng mới.",
                    NgayGui = new DateTime(2026, 5, 31, 20, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "HangThang",
                    NguoiDungId = 2
                },
                new LichSuNhanNhac
                {
                    Id = 4,
                    TieuDe = "Nhắc ghi chi tiêu đầu tháng",
                    NoiDung = "Ngày 02/06/2026 đã có thêm một số khoản chi tiêu cần kiểm tra.",
                    NgayGui = new DateTime(2026, 6, 2, 20, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "HangNgay",
                    NguoiDungId = 2
                },
                new LichSuNhanNhac
                {
                    Id = 5,
                    TieuDe = "Nhắc khám sức khỏe định kỳ",
                    NoiDung = "Bạn có lịch khám sức khỏe vào ngày 12/06/2026.",
                    NgayGui = new DateTime(2026, 6, 12, 8, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "ChiTieuTheoLich",
                    NguoiDungId = 3
                },
                new LichSuNhanNhac
                {
                    Id = 6,
                    TieuDe = "Nhắc gia hạn khóa học",
                    NoiDung = "Đến hạn gia hạn khóa học online ngày 15/06/2026.",
                    NgayGui = new DateTime(2026, 6, 15, 8, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "ChiTieuTheoLich",
                    NguoiDungId = 2
                },
                new LichSuNhanNhac
                {
                    Id = 7,
                    TieuDe = "Nhắc thanh toán hóa đơn",
                    NoiDung = "Hóa đơn điện nước tháng 6 đến hạn ngày 20/06/2026.",
                    NgayGui = new DateTime(2026, 6, 20, 8, 0, 0),
                    TrangThai = true,
                    LoaiNhanNhac = "HoaDon",
                    NguoiDungId = 3
                }
            };
        }

        private static List<Otp> GetOtps()
        {
            return new List<Otp>
            {
                new Otp
                {
                    Id = 1,
                    Email = "admin@qlct.com",
                    MaOtp = "102938",
                    ThoiGianTao = new DateTime(2026, 6, 1, 8, 0, 0),
                    TrangThai = false
                },
                new Otp
                {
                    Id = 2,
                    Email = "user1@qlct.com",
                    MaOtp = "564738",
                    ThoiGianTao = new DateTime(2026, 6, 1, 8, 15, 0),
                    TrangThai = true
                },
                new Otp
                {
                    Id = 3,
                    Email = "user2@qlct.com",
                    MaOtp = "918273",
                    ThoiGianTao = new DateTime(2026, 6, 2, 8, 30, 0),
                    TrangThai = true
                }
            };
        }

        private static List<TuKhoaDanhMuc> GetTuKhoaDanhMucs()
        {
            return new List<TuKhoaDanhMuc>
            {
                new TuKhoaDanhMuc { Id = 1, DanhMucId = 1, TuKhoa = "ăn" },
                new TuKhoaDanhMuc { Id = 2, DanhMucId = 1, TuKhoa = "cơm" },
                new TuKhoaDanhMuc { Id = 3, DanhMucId = 1, TuKhoa = "bún" },
                new TuKhoaDanhMuc { Id = 4, DanhMucId = 2, TuKhoa = "xăng" },
                new TuKhoaDanhMuc { Id = 5, DanhMucId = 2, TuKhoa = "grab" },
                new TuKhoaDanhMuc { Id = 6, DanhMucId = 2, TuKhoa = "xe" },
                new TuKhoaDanhMuc { Id = 7, DanhMucId = 3, TuKhoa = "mua" },
                new TuKhoaDanhMuc { Id = 8, DanhMucId = 3, TuKhoa = "áo" },
                new TuKhoaDanhMuc { Id = 9, DanhMucId = 3, TuKhoa = "balo" },
                new TuKhoaDanhMuc { Id = 10, DanhMucId = 4, TuKhoa = "phim" },
                new TuKhoaDanhMuc { Id = 11, DanhMucId = 4, TuKhoa = "giải trí" },
                new TuKhoaDanhMuc { Id = 12, DanhMucId = 5, TuKhoa = "hóa đơn" },
                new TuKhoaDanhMuc { Id = 13, DanhMucId = 5, TuKhoa = "tiền nhà" },
                new TuKhoaDanhMuc { Id = 14, DanhMucId = 5, TuKhoa = "internet" },
                new TuKhoaDanhMuc { Id = 15, DanhMucId = 6, TuKhoa = "thuốc" },
                new TuKhoaDanhMuc { Id = 16, DanhMucId = 6, TuKhoa = "khám" },
                new TuKhoaDanhMuc { Id = 17, DanhMucId = 7, TuKhoa = "học" },
                new TuKhoaDanhMuc { Id = 18, DanhMucId = 7, TuKhoa = "sách" },
                new TuKhoaDanhMuc { Id = 19, DanhMucId = 8, TuKhoa = "du lịch" },
                new TuKhoaDanhMuc { Id = 20, DanhMucId = 8, TuKhoa = "vé" },
                new TuKhoaDanhMuc { Id = 21, DanhMucId = 9, TuKhoa = "cà phê" },
                new TuKhoaDanhMuc { Id = 22, DanhMucId = 9, TuKhoa = "trà sữa" },
                new TuKhoaDanhMuc { Id = 23, DanhMucId = 10, TuKhoa = "đầu tư" },
                new TuKhoaDanhMuc { Id = 24, DanhMucId = 10, TuKhoa = "quỹ" }
            };
        }
    }
}
