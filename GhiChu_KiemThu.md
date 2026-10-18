# Ghi chú kiểm thử – Lab 04

| STT | Tình huống | Thao tác | Kết quả mong đợi |
|---|---|---|---|
| 1 | Thêm sản phẩm hợp lệ | Chọn 1, mã SP01, tên Chuot, giá 150000, SL 10 | Hiện `[EVENT] Da them san pham` |
| 2 | Nhập sai số | Chọn 1, đơn giá nhập `abc` | Báo "Gia tri khong hop le", cho nhập lại, không dừng |
| 3 | Giá/số lượng âm | Chọn 1, đơn giá `-5` | `Loi du lieu: Don gia khong duoc am.` |
| 4 | Mã trùng | Thêm lại mã SP01 | `Loi trung ma: Ma san pham 'SP01' da ton tai.` (DuplicateProductException) |
| 5 | Xóa mã không tồn tại | Chọn 6, mã `XYZ` | `Loi khong ton tai: ...` (ProductNotFoundException) |
| 6 | Xóa mã có tồn tại | Chọn 6, mã SP01 | Hiện `[EVENT] Da xoa san pham` |
| 7 | Danh sách rỗng | Chọn 2 khi chưa có SP | "Danh sach san pham dang rong." |
| 8 | Tìm theo tên | Chọn 4, từ khóa `chu` | Liệt kê SP có tên chứa "chu" |
| 9 | Lọc khoảng giá | Chọn 5, 100000 – 200000 | Liệt kê SP trong khoảng (dùng Func) |
| 10 | Tổng giá trị kho | Chọn 7 | Tổng = Σ đơn giá × số lượng |
