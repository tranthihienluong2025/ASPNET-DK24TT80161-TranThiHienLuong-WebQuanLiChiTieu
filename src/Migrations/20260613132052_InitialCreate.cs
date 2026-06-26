using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyChiTieu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TuDongPhanLoai = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    MauSac = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhMuc__3214EC07C2C35FB3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayDangKy = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    LoaiTaiKhoan = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    NhanEmailNhacNho = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    TanSuatNhanNhac = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: "HangTuan"),
                    GioNhanNhac = table.Column<TimeOnly>(type: "time", nullable: true, defaultValue: new TimeOnly(18, 0, 0)),
                    AvatarUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__3214EC07CF5C7E6E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaOTP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OTP__3214EC0766DF5851", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TuKhoaDanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanhMucId = table.Column<int>(type: "int", nullable: false),
                    TuKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TuKhoaDa__3214EC077923D733", x => x.Id);
                    table.ForeignKey(
                        name: "FK__TuKhoaDan__DanhM__52593CB8",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTieu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChiTieu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayChi = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    DanhMucId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTieu__3214EC0748AA8F80", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ChiTieu__DanhMuc__440B1D61",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTieu__NguoiDu__4316F928",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTieuTheoLich",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChiTieu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanThucHienCuoi = table.Column<DateOnly>(type: "date", nullable: true),
                    HoatDong = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    DanhMucId = table.Column<int>(type: "int", nullable: false),
                    NgayThucHien = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTieuT__3214EC07DDC555D6", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ChiTieuTh__DanhM__71D1E811",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTieuTh__Nguoi__70DDC3D8",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioiHanChiTieu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoTienToiDa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GioiHanC__3214EC078D4B0F17", x => x.Id);
                    table.ForeignKey(
                        name: "FK__GioiHanCh__Nguoi__46E78A0C",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuNhanNhac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    LoaiNhanNhac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichSuNh__3214EC07A3A7D1EB", x => x.Id);
                    table.ForeignKey(
                        name: "FK__LichSuNha__Nguoi__571DF1D5",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    DaDoc = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__3214EC07F56E0324", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ThongBao__NguoiD__4BAC3F29",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DanhMuc",
                columns: new[] { "Id", "MauSac", "TenDanhMuc", "TuDongPhanLoai" },
                values: new object[,]
                {
                    { 1, "#FF6384", "Ăn uống", true },
                    { 2, "#36A2EB", "Di chuyển", true },
                    { 3, "#FFCE56", "Mua sắm", true },
                    { 4, "#4BC0C0", "Giải trí", true },
                    { 5, "#9966FF", "Hóa đơn", true },
                    { 6, "#FF9F40", "Sức khỏe", true },
                    { 7, "#FFCD56", "Giáo dục", true },
                    { 8, "#C9CBCF", "Du lịch", true },
                    { 9, "#8E5EA2", "Cà phê", true },
                    { 10, "#3D9970", "Đầu tư", true }
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "Id", "AvatarUrl", "Email", "GioNhanNhac", "HoTen", "LoaiTaiKhoan", "MatKhau", "NgayDangKy", "NhanEmailNhacNho", "TanSuatNhanNhac" },
                values: new object[,]
                {
                    { 1, "https://ui-avatars.com/api/?name=admin&background=00112C&color=fff", "admin@qlct.com", new TimeOnly(8, 0, 0), "admin", 1, "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", new DateTime(2026, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), true, "HangNgay" },
                    { 2, "https://ui-avatars.com/api/?name=user1&background=36A2EB&color=fff", "user1@qlct.com", new TimeOnly(20, 0, 0), "user1", 0, "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", new DateTime(2026, 2, 10, 9, 30, 0, 0, DateTimeKind.Unspecified), true, "HangNgay" },
                    { 3, "https://ui-avatars.com/api/?name=user2&background=FF6384&color=fff", "user2@qlct.com", new TimeOnly(19, 30, 0), "user2", 0, "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", new DateTime(2026, 2, 15, 10, 15, 0, 0, DateTimeKind.Unspecified), true, "HangTuan" }
                });

            migrationBuilder.InsertData(
                table: "OTP",
                columns: new[] { "Id", "Email", "MaOTP", "ThoiGianTao", "TrangThai" },
                values: new object[,]
                {
                    { 1, "admin@qlct.com", "102938", new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2, "user1@qlct.com", "564738", new DateTime(2026, 6, 1, 8, 15, 0, 0, DateTimeKind.Unspecified), true },
                    { 3, "user2@qlct.com", "918273", new DateTime(2026, 6, 2, 8, 30, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "ChiTieu",
                columns: new[] { "Id", "DanhMucId", "GhiChu", "NgayChi", "NguoiDungId", "SoTien", "TenChiTieu" },
                values: new object[,]
                {
                    { 1, 1, "Bánh mì và cà phê", new DateTime(2026, 4, 1, 7, 30, 0, 0, DateTimeKind.Unspecified), 2, 35000m, "Ăn sáng" },
                    { 2, 2, "Di chuyển đầu tháng", new DateTime(2026, 4, 2, 18, 20, 0, 0, DateTimeKind.Unspecified), 3, 120000m, "Đổ xăng" },
                    { 3, 4, "Vé phim và nước", new DateTime(2026, 4, 4, 20, 10, 0, 0, DateTimeKind.Unspecified), 2, 180000m, "Xem phim cuối tuần" },
                    { 4, 5, "Chi phí cố định tháng 4", new DateTime(2026, 4, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 3200000m, "Tiền thuê phòng" },
                    { 5, 1, "Cơm trưa", new DateTime(2026, 4, 7, 12, 15, 0, 0, DateTimeKind.Unspecified), 3, 75000m, "Ăn trưa văn phòng" },
                    { 6, 9, "Làm việc buổi tối", new DateTime(2026, 4, 9, 21, 0, 0, 0, DateTimeKind.Unspecified), 2, 95000m, "Cà phê làm việc" },
                    { 7, 5, "Hóa đơn tháng 4", new DateTime(2026, 4, 10, 8, 30, 0, 0, DateTimeKind.Unspecified), 1, 250000m, "Internet văn phòng" },
                    { 8, 3, "Đồ đi làm", new DateTime(2026, 4, 12, 16, 45, 0, 0, DateTimeKind.Unspecified), 3, 420000m, "Mua áo sơ mi" },
                    { 9, 1, "Quán nướng", new DateTime(2026, 4, 14, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 210000m, "Ăn tối với bạn" },
                    { 10, 6, "Nhà thuốc gần nhà", new DateTime(2026, 4, 16, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, 160000m, "Thuốc cảm" },
                    { 11, 7, "Sách học tập", new DateTime(2026, 4, 18, 10, 15, 0, 0, DateTimeKind.Unspecified), 2, 260000m, "Sách kỹ năng" },
                    { 12, 5, "Hóa đơn sinh hoạt", new DateTime(2026, 4, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 3, 300000m, "Tiền điện nước" },
                    { 13, 2, "Trời mưa", new DateTime(2026, 4, 22, 18, 40, 0, 0, DateTimeKind.Unspecified), 2, 90000m, "Grab về nhà" },
                    { 14, 9, "Gặp bạn cuối tuần", new DateTime(2026, 4, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, 150000m, "Trà sữa" },
                    { 15, 10, "Góp định kỳ", new DateTime(2026, 4, 26, 9, 20, 0, 0, DateTimeKind.Unspecified), 2, 500000m, "Đầu tư quỹ" },
                    { 16, 3, "Phụ kiện làm việc", new DateTime(2026, 4, 28, 17, 10, 0, 0, DateTimeKind.Unspecified), 1, 680000m, "Mua thiết bị nhỏ" },
                    { 17, 5, "Chi phí cố định tháng 5", new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, 3600000m, "Tiền thuê phòng" },
                    { 18, 1, "Phở sáng", new DateTime(2026, 5, 2, 7, 45, 0, 0, DateTimeKind.Unspecified), 3, 45000m, "Ăn sáng" },
                    { 19, 2, "Đi làm hằng ngày", new DateTime(2026, 5, 4, 18, 10, 0, 0, DateTimeKind.Unspecified), 2, 130000m, "Đổ xăng" },
                    { 20, 3, "Đồ dùng cá nhân", new DateTime(2026, 5, 6, 20, 15, 0, 0, DateTimeKind.Unspecified), 3, 220000m, "Mua mỹ phẩm" },
                    { 21, 5, "Gia hạn công cụ", new DateTime(2026, 5, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 320000m, "Hóa đơn phần mềm" },
                    { 22, 8, "Du lịch ngắn ngày", new DateTime(2026, 5, 10, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, 850000m, "Đặt vé Đà Lạt" },
                    { 23, 1, "Cơm văn phòng", new DateTime(2026, 5, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, 70000m, "Ăn trưa" },
                    { 24, 9, "Cuối ngày", new DateTime(2026, 5, 14, 19, 45, 0, 0, DateTimeKind.Unspecified), 2, 110000m, "Cà phê gặp bạn" },
                    { 25, 6, "Kiểm tra định kỳ", new DateTime(2026, 5, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, 280000m, "Khám sức khỏe" },
                    { 26, 1, "Cuối tuần", new DateTime(2026, 5, 18, 21, 10, 0, 0, DateTimeKind.Unspecified), 2, 230000m, "Ăn tối gia đình" },
                    { 27, 5, "Hóa đơn tháng 5", new DateTime(2026, 5, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, 350000m, "Tiền điện nước" },
                    { 28, 7, "Nâng cấp kỹ năng", new DateTime(2026, 5, 22, 13, 15, 0, 0, DateTimeKind.Unspecified), 2, 300000m, "Khóa học online" },
                    { 29, 4, "Cuối tuần", new DateTime(2026, 5, 24, 20, 30, 0, 0, DateTimeKind.Unspecified), 3, 190000m, "Xem phim" },
                    { 30, 10, "Góp định kỳ", new DateTime(2026, 5, 26, 8, 45, 0, 0, DateTimeKind.Unspecified), 2, 600000m, "Đầu tư quỹ" },
                    { 31, 1, "Ăn tối công việc", new DateTime(2026, 5, 28, 17, 50, 0, 0, DateTimeKind.Unspecified), 1, 450000m, "Tiếp khách" },
                    { 32, 3, "Chuẩn bị đi làm", new DateTime(2026, 5, 30, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 500000m, "Mua balo" },
                    { 33, 1, "Bún bò", new DateTime(2026, 6, 1, 7, 25, 0, 0, DateTimeKind.Unspecified), 2, 40000m, "Ăn sáng đầu tháng" },
                    { 34, 2, "Chuẩn bị tuần mới", new DateTime(2026, 6, 1, 8, 20, 0, 0, DateTimeKind.Unspecified), 3, 125000m, "Đổ xăng" },
                    { 35, 5, "Chi phí cố định tháng 6", new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 3600000m, "Tiền thuê phòng" },
                    { 36, 1, "Cơm văn phòng", new DateTime(2026, 6, 1, 12, 10, 0, 0, DateTimeKind.Unspecified), 3, 75000m, "Ăn trưa" },
                    { 37, 5, "Hóa đơn tháng 6", new DateTime(2026, 6, 1, 15, 30, 0, 0, DateTimeKind.Unspecified), 1, 250000m, "Internet văn phòng" },
                    { 38, 9, "Làm việc cá nhân", new DateTime(2026, 6, 1, 19, 40, 0, 0, DateTimeKind.Unspecified), 2, 95000m, "Cà phê tối" },
                    { 39, 1, "Bánh cuốn", new DateTime(2026, 6, 2, 7, 40, 0, 0, DateTimeKind.Unspecified), 3, 45000m, "Ăn sáng" },
                    { 40, 1, "Cơm phần", new DateTime(2026, 6, 2, 11, 50, 0, 0, DateTimeKind.Unspecified), 2, 70000m, "Ăn trưa văn phòng" },
                    { 41, 3, "Dầu gội và sữa tắm", new DateTime(2026, 6, 2, 14, 15, 0, 0, DateTimeKind.Unspecified), 3, 120000m, "Mua đồ cá nhân" },
                    { 42, 2, "Tan làm muộn", new DateTime(2026, 6, 2, 18, 25, 0, 0, DateTimeKind.Unspecified), 2, 85000m, "Grab về nhà" },
                    { 43, 4, "Gia hạn dịch vụ tháng 6", new DateTime(2026, 6, 2, 20, 10, 0, 0, DateTimeKind.Unspecified), 3, 59000m, "Gói nghe nhạc" },
                    { 44, 1, "Bánh mì đầu ngày", new DateTime(2026, 6, 3, 7, 30, 0, 0, DateTimeKind.Unspecified), 2, 35000m, "Ăn sáng" },
                    { 45, 1, "Cơm văn phòng", new DateTime(2026, 6, 3, 12, 10, 0, 0, DateTimeKind.Unspecified), 3, 70000m, "Ăn trưa" },
                    { 46, 2, "Đi làm hằng ngày", new DateTime(2026, 6, 4, 18, 15, 0, 0, DateTimeKind.Unspecified), 2, 120000m, "Đổ xăng" },
                    { 47, 9, "Giải lao buổi tối", new DateTime(2026, 6, 4, 20, 30, 0, 0, DateTimeKind.Unspecified), 3, 50000m, "Trà sữa" },
                    { 48, 2, "Phí gửi xe chung cư", new DateTime(2026, 6, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, 180000m, "Gửi xe tháng 6" },
                    { 49, 1, "Quán ăn gần nhà", new DateTime(2026, 6, 5, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 150000m, "Ăn tối" },
                    { 50, 1, "Thực phẩm trong tuần", new DateTime(2026, 6, 6, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, 220000m, "Đi chợ cuối tuần" },
                    { 51, 4, "Cuối tuần", new DateTime(2026, 6, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 180000m, "Xem phim" },
                    { 52, 1, "Cơm phần", new DateTime(2026, 6, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 65000m, "Ăn trưa" },
                    { 53, 9, "Cuối tuần", new DateTime(2026, 6, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, 60000m, "Cà phê gặp bạn" },
                    { 54, 5, "Gia hạn dung lượng", new DateTime(2026, 6, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 49000m, "Gói lưu trữ đám mây" },
                    { 55, 2, "Đi làm", new DateTime(2026, 6, 8, 18, 20, 0, 0, DateTimeKind.Unspecified), 3, 110000m, "Đổ xăng" },
                    { 56, 1, "Phở sáng", new DateTime(2026, 6, 9, 7, 40, 0, 0, DateTimeKind.Unspecified), 2, 40000m, "Ăn sáng" },
                    { 57, 3, "Đồ dùng nhà bếp", new DateTime(2026, 6, 9, 21, 0, 0, 0, DateTimeKind.Unspecified), 3, 320000m, "Mua đồ gia dụng" },
                    { 58, 5, "Gói cước hằng tháng", new DateTime(2026, 6, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 100000m, "Nạp thẻ điện thoại" },
                    { 59, 1, "Cơm văn phòng", new DateTime(2026, 6, 10, 12, 30, 0, 0, DateTimeKind.Unspecified), 2, 70000m, "Ăn trưa" },
                    { 60, 5, "Gia hạn công cụ làm việc", new DateTime(2026, 6, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 320000m, "Hóa đơn phần mềm" },
                    { 61, 1, "Quây quần cuối tuần", new DateTime(2026, 6, 11, 19, 30, 0, 0, DateTimeKind.Unspecified), 3, 240000m, "Ăn tối gia đình" },
                    { 62, 9, "Buổi sáng", new DateTime(2026, 6, 12, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 95000m, "Cà phê làm việc" },
                    { 63, 6, "Lịch khám theo tháng", new DateTime(2026, 6, 12, 16, 30, 0, 0, DateTimeKind.Unspecified), 3, 300000m, "Khám sức khỏe định kỳ" },
                    { 64, 1, "Cơm phần", new DateTime(2026, 6, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, 70000m, "Ăn trưa" },
                    { 65, 2, "Tan làm muộn", new DateTime(2026, 6, 13, 18, 40, 0, 0, DateTimeKind.Unspecified), 2, 85000m, "Grab về nhà" },
                    { 66, 4, "Giải trí cuối tuần", new DateTime(2026, 6, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), 3, 95000m, "Xem phim" },
                    { 67, 1, "Cuối tuần", new DateTime(2026, 6, 14, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 210000m, "Ăn tối với bạn" },
                    { 68, 7, "Sách tham khảo", new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, 150000m, "Mua sách" },
                    { 69, 7, "Gia hạn khóa học kỹ năng", new DateTime(2026, 6, 15, 13, 15, 0, 0, DateTimeKind.Unspecified), 2, 300000m, "Khóa học online tháng 6" },
                    { 70, 1, "Bánh mì", new DateTime(2026, 6, 16, 7, 30, 0, 0, DateTimeKind.Unspecified), 2, 35000m, "Ăn sáng" },
                    { 71, 2, "Đi làm", new DateTime(2026, 6, 16, 18, 30, 0, 0, DateTimeKind.Unspecified), 3, 120000m, "Đổ xăng" },
                    { 72, 1, "Cơm văn phòng", new DateTime(2026, 6, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 70000m, "Ăn trưa" },
                    { 73, 9, "Giải lao", new DateTime(2026, 6, 17, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, 55000m, "Trà sữa" },
                    { 74, 1, "Đồ ăn cho tuần giữa tháng", new DateTime(2026, 6, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 450000m, "Mua thực phẩm định kỳ" },
                    { 75, 9, "Làm việc cá nhân", new DateTime(2026, 6, 18, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 90000m, "Cà phê tối" },
                    { 76, 3, "Đồ dùng làm việc", new DateTime(2026, 6, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 180000m, "Mua văn phòng phẩm" },
                    { 77, 1, "Cơm phần", new DateTime(2026, 6, 19, 12, 30, 0, 0, DateTimeKind.Unspecified), 2, 65000m, "Ăn trưa" },
                    { 78, 5, "Hóa đơn sinh hoạt", new DateTime(2026, 6, 20, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, 360000m, "Tiền điện nước tháng 6" },
                    { 79, 3, "Mua sắm cuối tuần", new DateTime(2026, 6, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, 280000m, "Đi siêu thị" },
                    { 80, 1, "Cuối tuần", new DateTime(2026, 6, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), 3, 130000m, "Ăn tối" }
                });

            migrationBuilder.InsertData(
                table: "ChiTieuTheoLich",
                columns: new[] { "Id", "DanhMucId", "GhiChu", "LanThucHienCuoi", "NgayThucHien", "NguoiDungId", "SoTien", "TenChiTieu" },
                values: new object[,]
                {
                    { 1, 5, "Đã thanh toán tiền thuê phòng tháng 4", new DateOnly(2026, 4, 5), new DateOnly(2026, 4, 5), 2, 3200000m, "Tiền thuê phòng tháng 4" },
                    { 2, 5, "Đã thanh toán Internet tháng 4", new DateOnly(2026, 4, 10), new DateOnly(2026, 4, 10), 1, 250000m, "Internet văn phòng tháng 4" },
                    { 3, 5, "Đã thanh toán hóa đơn sinh hoạt tháng 4", new DateOnly(2026, 4, 20), new DateOnly(2026, 4, 20), 3, 300000m, "Tiền điện nước tháng 4" },
                    { 4, 5, "Đã thanh toán tiền thuê phòng tháng 5", new DateOnly(2026, 5, 1), new DateOnly(2026, 5, 1), 2, 3600000m, "Tiền thuê phòng tháng 5" },
                    { 5, 5, "Đã gia hạn công cụ làm việc", new DateOnly(2026, 5, 8), new DateOnly(2026, 5, 8), 1, 320000m, "Hóa đơn phần mềm tháng 5" },
                    { 6, 6, "Đã hoàn thành lịch khám định kỳ", new DateOnly(2026, 5, 16), new DateOnly(2026, 5, 16), 3, 280000m, "Khám sức khỏe tháng 5" },
                    { 7, 5, "Đã thanh toán hóa đơn sinh hoạt tháng 5", new DateOnly(2026, 5, 20), new DateOnly(2026, 5, 20), 3, 350000m, "Tiền điện nước tháng 5" },
                    { 8, 5, "Đã thanh toán vào ngày đầu tháng", new DateOnly(2026, 6, 1), new DateOnly(2026, 6, 1), 2, 3600000m, "Tiền thuê phòng tháng 6" },
                    { 9, 4, "Đã gia hạn dịch vụ giải trí", new DateOnly(2026, 6, 2), new DateOnly(2026, 6, 2), 3, 59000m, "Gói nghe nhạc tháng 6" },
                    { 10, 2, "Đã thanh toán phí gửi xe chung cư", new DateOnly(2026, 6, 5), new DateOnly(2026, 6, 5), 2, 180000m, "Gửi xe tháng 6" },
                    { 11, 5, "Đã gia hạn dung lượng lưu trữ", new DateOnly(2026, 6, 8), new DateOnly(2026, 6, 8), 1, 49000m, "Gói lưu trữ đám mây" },
                    { 12, 5, "Đã nạp gói cước liên lạc hằng tháng", new DateOnly(2026, 6, 10), new DateOnly(2026, 6, 10), 2, 100000m, "Nạp thẻ điện thoại" },
                    { 13, 6, "Đã hoàn thành lịch khám theo tháng", new DateOnly(2026, 6, 12), new DateOnly(2026, 6, 12), 3, 300000m, "Khám sức khỏe định kỳ" },
                    { 14, 7, "Đã gia hạn khóa học kỹ năng", new DateOnly(2026, 6, 15), new DateOnly(2026, 6, 15), 2, 300000m, "Khóa học online tháng 6" },
                    { 15, 1, "Đã mua đồ ăn cho tuần giữa tháng", new DateOnly(2026, 6, 18), new DateOnly(2026, 6, 18), 3, 450000m, "Mua thực phẩm định kỳ" },
                    { 16, 5, "Đã thanh toán hóa đơn sinh hoạt tháng 6", new DateOnly(2026, 6, 20), new DateOnly(2026, 6, 20), 3, 360000m, "Tiền điện nước tháng 6" }
                });

            migrationBuilder.InsertData(
                table: "ChiTieuTheoLich",
                columns: new[] { "Id", "DanhMucId", "GhiChu", "HoatDong", "LanThucHienCuoi", "NgayThucHien", "NguoiDungId", "SoTien", "TenChiTieu" },
                values: new object[,]
                {
                    { 17, 9, "Dự kiến gặp bạn làm việc cuối tuần", true, new DateOnly(2026, 5, 14), new DateOnly(2026, 6, 23), 2, 120000m, "Cà phê làm việc định kỳ" },
                    { 18, 10, "Góp quỹ định kỳ cuối tháng", true, new DateOnly(2026, 5, 26), new DateOnly(2026, 6, 26), 2, 600000m, "Đầu tư quỹ tháng 6" }
                });

            migrationBuilder.InsertData(
                table: "GioiHanChiTieu",
                columns: new[] { "Id", "Nam", "NguoiDungId", "SoTienToiDa", "Thang" },
                values: new object[,]
                {
                    { 1, 2026, 1, 2500000m, 4 },
                    { 2, 2026, 2, 6500000m, 4 },
                    { 3, 2026, 3, 3500000m, 4 },
                    { 4, 2026, 1, 2500000m, 5 },
                    { 5, 2026, 2, 7500000m, 5 },
                    { 6, 2026, 3, 4000000m, 5 },
                    { 7, 2026, 1, 3000000m, 6 },
                    { 8, 2026, 2, 8500000m, 6 },
                    { 9, 2026, 3, 4500000m, 6 }
                });

            migrationBuilder.InsertData(
                table: "LichSuNhanNhac",
                columns: new[] { "Id", "LoaiNhanNhac", "NgayGui", "NguoiDungId", "NoiDung", "TieuDe", "TrangThai" },
                values: new object[,]
                {
                    { 1, "HangNgay", new DateTime(2026, 4, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Đừng quên ghi lại chi tiêu hôm nay.", "Nhắc nhập chi tiêu", true },
                    { 2, "HoaDon", new DateTime(2026, 4, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 3, "Bạn có hóa đơn điện nước cần thanh toán.", "Nhắc thanh toán hóa đơn", true },
                    { 3, "HangThang", new DateTime(2026, 5, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Kiểm tra lại chi tiêu tháng 5 trước khi sang tháng mới.", "Tổng kết tháng 5", true },
                    { 4, "HangNgay", new DateTime(2026, 6, 2, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ngày 02/06/2026 đã có thêm một số khoản chi tiêu cần kiểm tra.", "Nhắc ghi chi tiêu đầu tháng", true },
                    { 5, "ChiTieuTheoLich", new DateTime(2026, 6, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 3, "Bạn có lịch khám sức khỏe vào ngày 12/06/2026.", "Nhắc khám sức khỏe định kỳ", true },
                    { 6, "ChiTieuTheoLich", new DateTime(2026, 6, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "Đến hạn gia hạn khóa học online ngày 15/06/2026.", "Nhắc gia hạn khóa học", true },
                    { 7, "HoaDon", new DateTime(2026, 6, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 3, "Hóa đơn điện nước tháng 6 đến hạn ngày 20/06/2026.", "Nhắc thanh toán hóa đơn", true }
                });

            migrationBuilder.InsertData(
                table: "ThongBao",
                columns: new[] { "Id", "DaDoc", "NgayGui", "NguoiDungId", "NoiDung" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 4, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bạn đã sử dụng khoảng 70% hạn mức tháng 4/2026." },
                    { 2, true, new DateTime(2026, 4, 20, 20, 10, 0, 0, DateTimeKind.Unspecified), 3, "Hóa đơn điện nước tháng 4 đã được ghi nhận." },
                    { 3, false, new DateTime(2026, 5, 25, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bạn có lịch đầu tư quỹ vào ngày 26/05/2026." },
                    { 4, false, new DateTime(2026, 5, 30, 21, 0, 0, 0, DateTimeKind.Unspecified), 3, "Tổng chi tiêu tháng 5 vẫn nằm trong hạn mức." },
                    { 5, false, new DateTime(2026, 6, 2, 21, 0, 0, 0, DateTimeKind.Unspecified), 2, "Đã ghi nhận chi tiêu đến ngày 02/06/2026. Hạn mức tháng 6 vẫn còn an toàn." },
                    { 6, true, new DateTime(2026, 6, 2, 21, 15, 0, 0, DateTimeKind.Unspecified), 2, "Bạn có 2 khoản chi tiêu theo lịch sắp tới trong tháng 6/2026." },
                    { 7, true, new DateTime(2026, 6, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, "Khám sức khỏe định kỳ ngày 12/06/2026 đã được ghi nhận." },
                    { 8, false, new DateTime(2026, 6, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bạn đã sử dụng khoảng 65% hạn mức tháng 6/2026." },
                    { 9, false, new DateTime(2026, 6, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, "Hóa đơn điện nước tháng 6 đã được thanh toán ngày 20/06/2026." },
                    { 10, false, new DateTime(2026, 6, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, "Sắp tới hạn đầu tư quỹ định kỳ ngày 26/06/2026." }
                });

            migrationBuilder.InsertData(
                table: "TuKhoaDanhMuc",
                columns: new[] { "Id", "DanhMucId", "TuKhoa" },
                values: new object[,]
                {
                    { 1, 1, "ăn" },
                    { 2, 1, "cơm" },
                    { 3, 1, "bún" },
                    { 4, 2, "xăng" },
                    { 5, 2, "grab" },
                    { 6, 2, "xe" },
                    { 7, 3, "mua" },
                    { 8, 3, "áo" },
                    { 9, 3, "balo" },
                    { 10, 4, "phim" },
                    { 11, 4, "giải trí" },
                    { 12, 5, "hóa đơn" },
                    { 13, 5, "tiền nhà" },
                    { 14, 5, "internet" },
                    { 15, 6, "thuốc" },
                    { 16, 6, "khám" },
                    { 17, 7, "học" },
                    { 18, 7, "sách" },
                    { 19, 8, "du lịch" },
                    { 20, 8, "vé" },
                    { 21, 9, "cà phê" },
                    { 22, 9, "trà sữa" },
                    { 23, 10, "đầu tư" },
                    { 24, 10, "quỹ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieu_DanhMucId",
                table: "ChiTieu",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieu_NgayChi",
                table: "ChiTieu",
                column: "NgayChi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieu_NguoiDung_NgayChi",
                table: "ChiTieu",
                columns: new[] { "NguoiDungId", "NgayChi" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieu_NguoiDungId",
                table: "ChiTieu",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieuTheoLich_DanhMucId",
                table: "ChiTieuTheoLich",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieuTheoLich_NgayThucHien",
                table: "ChiTieuTheoLich",
                column: "NgayThucHien");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTieuTheoLich_NguoiDungId",
                table: "ChiTieuTheoLich",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_TenDanhMuc",
                table: "DanhMuc",
                column: "TenDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_GioiHanChiTieu_NguoiDung_Thang_Nam",
                table: "GioiHanChiTieu",
                columns: new[] { "NguoiDungId", "Thang", "Nam" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GioiHanChiTieu_NguoiDungId",
                table: "GioiHanChiTieu",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_GioiHanChiTieu_Thang_Nam",
                table: "GioiHanChiTieu",
                columns: new[] { "Thang", "Nam" });

            migrationBuilder.CreateIndex(
                name: "IX_LichSuNhanNhac_NgayGui",
                table: "LichSuNhanNhac",
                column: "NgayGui");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuNhanNhac_NguoiDungId",
                table: "LichSuNhanNhac",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "UQ__NguoiDun__A9D10534A0F8E828",
                table: "NguoiDung",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OTP_Email",
                table: "OTP",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MaOTP",
                table: "OTP",
                column: "MaOTP");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_NgayGui",
                table: "ThongBao",
                column: "NgayGui");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_NguoiDungId",
                table: "ThongBao",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_TuKhoaDanhMuc_DanhMucId",
                table: "TuKhoaDanhMuc",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_TuKhoaDanhMuc_TuKhoa",
                table: "TuKhoaDanhMuc",
                column: "TuKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTieu");

            migrationBuilder.DropTable(
                name: "ChiTieuTheoLich");

            migrationBuilder.DropTable(
                name: "GioiHanChiTieu");

            migrationBuilder.DropTable(
                name: "LichSuNhanNhac");

            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "TuKhoaDanhMuc");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
