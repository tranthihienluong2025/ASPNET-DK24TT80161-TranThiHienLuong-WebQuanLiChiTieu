# ĐỒ ÁN MÔN HỌC: CHUYÊN ĐỀ ASP.NET
## ĐỀ TÀI: XÂY DỰNG WEBSITE QUẢN LÝ CHI TIÊU CÁ NHÂN

### THÔNG TIN SINH VIÊN & GIẢNG VIÊN HƯỚNG DẪN
* **Sinh viên thực hiện**: Trần Thị Hiền Lương
* **MSSV**: 170124022
* **Mã lớp**: DK24TTG1
* **Email**: tranthihienluong2025@gmail.com
* **SĐT**: [Cung cấp số điện thoại của bạn]
* **Giảng viên hướng dẫn**: ThS. Đoàn Phước Miền

---

### 1. Giới thiệu đề tài
**Website Quản lý chi tiêu cá nhân** là một nền tảng trực tuyến giúp người dùng ghi chép, theo dõi và kiểm soát tài chính cá nhân một cách khoa học và trực quan. Hệ thống cho phép quản lý chi tiêu, thu nhập, lập hạn mức chi tiêu, thiết lập mục tiêu tiết kiệm, tự động ghi nhận các khoản chi định kỳ và nhận báo cáo thống kê phân tích tài chính. Bên cạnh đó, hệ thống cung cấp giao diện quản trị Admin giúp quản lý danh sách người dùng, cấp tài khoản và theo dõi trạng thái hoạt động toàn hệ thống.

**Các công nghệ sử dụng**:
* **Backend**: ASP.NET Core MVC (.NET 8 SDK)
* **Database**: SQL Server (Express / LocalDB / Standard)
* **ORM**: Entity Framework Core
* **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5, Chart.js (vẽ biểu đồ trực quan)
* **Realtime**: SignalR (gửi thông báo thời gian thực)
* **Email Service**: MailKit & MimeKit (gửi email nhắc nhở và xác thực OTP)

---

### 2. Các chức năng cốt lõi của hệ thống
#### Đối với Người dùng (User)
* **Đăng ký, Đăng nhập & Đăng xuất**: Hỗ trợ cơ chế bảo mật xác thực Cookie và bảo vệ tài khoản.
* **Xác thực mã OTP qua Email**: Áp dụng trong quy trình đăng ký tài khoản, khôi phục mật khẩu (quên mật khẩu) và đổi mật khẩu an toàn.
* **Quản lý Hồ sơ cá nhân**: Cập nhật thông tin tài khoản (họ tên, email, ảnh đại diện) hoặc xóa tài khoản.
* **Quản lý Chi tiêu & Thu nhập**: Thêm mới, chỉnh sửa, xóa và hiển thị danh sách các khoản thu/chi. Hỗ trợ tìm kiếm nhanh theo từ khóa, lọc theo danh mục hoặc khoảng thời gian.
* **Tự động phân loại chi tiêu**: Tự động phân loại giao dịch dựa trên từ khóa nhập vào (ví dụ: gõ "cơm trưa" tự động nhận diện danh mục "Ăn uống").
* **Quản lý Danh mục**: Thêm mới, chỉnh sửa, xóa các danh mục chi tiêu/thu nhập cá nhân kèm màu sắc hiển thị trực quan.
* **Quản lý Hạn mức chi tiêu**: Đặt hạn mức chi tiêu tối đa theo tháng/năm. Hệ thống sẽ tự động so sánh, tính toán tỉ lệ và phát cảnh báo (qua giao diện & email) khi người dùng chi tiêu vượt ngưỡng.
* **Mục tiêu tiết kiệm**: Tạo mục tiêu tích lũy, theo dõi tiến độ tự động dựa trên số dư và đánh dấu hoàn thành khi đạt mục tiêu.
* **Chi tiêu định kỳ**: Thiết lập lịch chi tiêu cố định (tiền điện, tiền nước, thuê nhà...). Hệ thống tự động khởi tạo giao dịch chi tiêu khi đến kỳ hạn mà không cần nhập thủ công.
* **Cấu hình nhắc nhở & thông báo**: Người dùng có thể bật/tắt nhận email nhắc nhở ghi chép chi tiêu, tự chọn tần suất (hằng ngày, hằng tuần, hằng tháng) và khung giờ nhận mail.
* **Thống kê & Biểu đồ**: Thống kê số liệu chi tiêu theo danh mục, theo thời gian bằng biểu đồ trực quan, giúp người dùng dễ dàng tối ưu hóa tài chính.
* **Thông báo thời gian thực (SignalR)**: Nhận thông báo tức thời khi hệ thống tự động ghi nhận chi tiêu định kỳ hoặc phát cảnh báo vượt hạn mức ngân sách.

#### Đối với Quản trị viên (Admin)
* **Quản lý Người dùng**: Xem danh sách toàn bộ người dùng, hiển thị thông tin chi tiết (avatar, email, vai trò, số lượng giao dịch và tổng chi tiêu).
* **Quản trị Tài khoản**:
  * Tìm kiếm nhanh người dùng theo họ tên hoặc email.
  * Thêm mới tài khoản người dùng trực tiếp từ giao diện Admin.
  * Chỉnh sửa thông tin, đặt lại mật khẩu và chuyển đổi vai trò (Admin / User) linh hoạt.
  * Khóa/mở khóa tài khoản để kiểm soát quyền truy cập hệ thống.
* **Thống kê toàn hệ thống**: Theo dõi số liệu tổng quan về hoạt động và lượng người dùng của website.

---

### 3. Cấu trúc thư mục dự án
Hệ thống được tổ chức khoa học để phục vụ việc chấm điểm và kiểm tra tiến độ:
* [setup/](./setup/) : Chứa file cơ sở dữ liệu [db.bacpac](./setup/db.bacpac) phục vụ việc khôi phục database.
* [progress-report/](./progress-report/) : Thư mục lưu trữ các file báo cáo tiến độ thực hiện đồ án hàng tuần (`BaoCaoTuan1.md`, `BaoCaoTuan2.md`, `BaoCaoTuan3.md`, `BaoCaoTuan4.md`).
* [thesis/](./thesis/) : Chứa file báo cáo đồ án chính thức [ASPNET-DK24TT80161-TranThiHienLuong-WebQuanLiChiTieu.docx](./thesis/ASPNET-DK24TT80161-TranThiHienLuong-WebQuanLiChiTieu.docx).
* [src/](./src/) : Thư mục chứa toàn bộ mã nguồn ứng dụng ASP.NET Core MVC.

---

### 4. Hướng dẫn cài đặt và chạy chương trình
Để hỗ trợ Giảng viên cài đặt và chấm điểm nhanh chóng, vui lòng thực hiện theo các bước hướng dẫn chi tiết dưới đây:

[XEM FILE BÁO CÁO CHI TIẾT TẠI ĐÂY](./thesis/ASPNET-DK24TT80161-TranThiHienLuong-WebQuanLiChiTieu.docx)

#### Tóm tắt các bước thực hiện nhanh:
1. **Tải mã nguồn**: Clone hoặc tải file nén Zip của Repository này về máy tính.
2. **Khôi phục Database**:
   * Mở phần mềm **SQL Server Management Studio (SSMS)**.
   * Nhấp chuột phải vào thư mục **Databases** -> Chọn **Import Data-tier Application...**
   * Tìm đường dẫn và chọn file khôi phục tại: `setup/db.bacpac`.
   * Tiến hành Import để khởi tạo cơ sở dữ liệu `DataBase_DoAnvenha` cùng toàn bộ dữ liệu mẫu đã được tích hợp sẵn.
3. **Mở dự án**: Khởi động Visual Studio 2022 (hoặc phiên bản mới hơn) và mở file Solution tại đường dẫn [QuanLyChiTieu.sln](./src/QuanLyChiTieu.sln).
4. **Kiểm tra kết nối**: Đảm bảo chuỗi kết nối (Connection String) trong file [appsettings.json](./src/appsettings.json) khớp với cấu hình SQL Server trên máy của Thầy/Cô:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=DESKTOP-2D7U6LA\\SQLEXPRESS;Database=DataBase_DoAnvenha;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
5. **Khởi chạy**: Nhấn phím **F5** hoặc nhấp vào nút **Play** trên thanh công cụ của Visual Studio để biên dịch và chạy ứng dụng trên trình duyệt web.

---

### 5. Tài khoản thử nghiệm (Demo Accounts)
Sau khi khôi phục database thành công, Thầy/Cô có thể sử dụng các tài khoản mẫu dưới đây để kiểm tra toàn bộ phân quyền của hệ thống:

| Vai trò (Role) | Tên đăng nhập (Email / Username) | Mật khẩu (Password) |
|---|---|---|
| **Quản trị viên (Admin)** | `admin@qlct.com` | `12345678` |
| **Người dùng mẫu 1** | `user1@qlct.com` | `12345678` |
| **Người dùng mẫu 2** | `user2@qlct.com` | `12345678` |

> [!TIP]
> Ngoài các tài khoản mẫu trên, Thầy/Cô cũng có thể sử dụng chức năng đăng ký tài khoản trực tiếp trên giao diện và thực hiện kích hoạt thông qua mã OTP để kiểm tra toàn bộ luồng hoạt động.

---

### 6. Quản lý tiến độ đồ án
* **Báo cáo tuần**: Được cập nhật liên tục tại thư mục [progress-report/](./progress-report/) theo đúng quy định của học phần.
* **Lịch sử commit**: Được duy trì đều đặn tối thiểu 1 tuần/lần để minh chứng cho tiến độ thực hiện thực tế của sinh viên.