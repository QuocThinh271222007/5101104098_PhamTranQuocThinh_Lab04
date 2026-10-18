using System.Globalization;
using System.Text;

namespace Lab04_QuanLySanPham;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var service = new ProductService();

        // Đăng ký lắng nghe event: được gọi khi thêm/xóa thành công
        service.ProductAdded += p => Console.WriteLine($"[EVENT] Da them san pham: {p.MaSP} - {p.TenSP}");
        service.ProductRemoved += p => Console.WriteLine($"[EVENT] Da xoa san pham: {p.MaSP} - {p.TenSP}");

        string luaChon;
        do
        {
            HienThiMenu();
            luaChon = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            try
            {
                switch (luaChon)
                {
                    case "1": ThemSanPham(service); break;
                    case "2": XuatDanhSach(service); break;
                    case "3": TimTheoMa(service); break;
                    case "4": TimTheoTen(service); break;
                    case "5": LocTheoKhoangGia(service); break;
                    case "6": XoaSanPham(service); break;
                    case "7": TinhTongGiaTriKho(service); break;
                    case "0": Console.WriteLine("Tam biet!"); break;
                    default: Console.WriteLine("Lua chon khong hop le!"); break;
                }
            }
            // Bắt từng loại exception riêng để thông báo rõ ràng
            catch (DuplicateProductException ex)
            {
                Console.WriteLine("Loi trung ma: " + ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine("Loi khong ton tai: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Loi du lieu: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khong xac dinh: " + ex.Message);
            }

            if (luaChon != "0")
            {
                Console.WriteLine();
                Console.Write("Nhan Enter de quay lai menu...");
                Console.ReadLine();
            }
        } while (luaChon != "0");
    }

    // ===================== Menu =====================

    static void HienThiMenu()
    {
        Console.Clear();
        Console.WriteLine("===== PRODUCT MANAGER =====");
        Console.WriteLine("1. Them san pham");
        Console.WriteLine("2. Xuat danh sach");
        Console.WriteLine("3. Tim theo ma");
        Console.WriteLine("4. Tim theo ten");
        Console.WriteLine("5. Loc theo khoang gia");
        Console.WriteLine("6. Xoa san pham");
        Console.WriteLine("7. Tinh tong gia tri kho");
        Console.WriteLine("0. Thoat");
        Console.Write("Chon: ");
    }

    // ===================== Chức năng =====================

    static void ThemSanPham(ProductService service)
    {
        string ma = NhapChuoi("Nhap ma san pham: ");
        string ten = NhapChuoi("Nhap ten san pham: ");
        decimal gia = NhapSoThuc("Nhap don gia: ");
        int soLuong = NhapSoNguyen("Nhap so luong: ");

        // Product ném ArgumentException nếu giá/số lượng âm;
        // AddProduct ném DuplicateProductException nếu mã trùng.
        var product = new Product(ma, ten, gia, soLuong);
        service.AddProduct(product);
    }

    static void XuatDanhSach(ProductService service)
    {
        var ds = service.GetAll();
        if (ds.Count == 0)
        {
            Console.WriteLine("Danh sach san pham dang rong.");
            return;
        }
        InDanhSach(ds);
    }

    static void TimTheoMa(ProductService service)
    {
        string ma = NhapChuoi("Nhap ma san pham can tim: ");
        Product? p = service.FindById(ma);

        if (p == null)
            Console.WriteLine($"Khong tim thay san pham co ma '{ma}'.");
        else
            InDanhSach(new[] { p });
    }

    static void TimTheoTen(ProductService service)
    {
        string tuKhoa = NhapChuoi("Nhap tu khoa ten san pham: ");
        InKetQuaTimKiem(service.SearchByName(tuKhoa));
    }

    static void LocTheoKhoangGia(ProductService service)
    {
        decimal min = NhapSoThuc("Nhap gia nho nhat: ");
        decimal max = NhapSoThuc("Nhap gia lon nhat: ");

        if (min > max)
        {
            Console.WriteLine("Gia nho nhat khong duoc lon hon gia lon nhat.");
            return;
        }

        // Truyền Func<Product,bool> trực tiếp cho Filter
        Func<Product, bool> trongKhoang = p => p.Price >= min && p.Price <= max;
        InKetQuaTimKiem(service.Filter(trongKhoang));
    }

    static void XoaSanPham(ProductService service)
    {
        string ma = NhapChuoi("Nhap ma san pham can xoa: ");
        service.RemoveProduct(ma); // ném ProductNotFoundException nếu không có
    }

    static void TinhTongGiaTriKho(ProductService service)
    {
        Console.WriteLine($"Tong gia tri kho = {service.TinhTongGiaTriKho():N0}");
    }

    // ===================== In ấn =====================

    static void InDanhSach(IEnumerable<Product> ds)
    {
        Console.WriteLine($"{"Ma",-8} | {"Ten",-25} | {"Don gia",14} | {"SL",6}");
        Console.WriteLine(new string('-', 62));
        foreach (Product p in ds)
        {
            Console.WriteLine(p);
        }
    }

    static void InKetQuaTimKiem(IEnumerable<Product> ketQua)
    {
        var ds = ketQua.ToList();
        if (ds.Count == 0)
            Console.WriteLine("Khong co san pham nao phu hop.");
        else
            InDanhSach(ds);
    }

    // ===================== Nhập liệu an toàn =====================

    /// <summary>Nhập chuỗi không rỗng.</summary>
    static string NhapChuoi(string message)
    {
        while (true)
        {
            Console.Write(message);
            string s = (Console.ReadLine() ?? "").Trim();
            if (s != "") return s;
            Console.WriteLine("Khong duoc de trong!");
        }
    }

    /// <summary>Nhập số nguyên, lặp lại đến khi hợp lệ.</summary>
    static int NhapSoNguyen(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value)) return value;
            Console.WriteLine("Gia tri khong hop le, vui long nhap so nguyen!");
        }
    }

    /// <summary>Nhập số thực (chấp nhận cả dấu , và .), lặp lại đến khi hợp lệ.</summary>
    static decimal NhapSoThuc(string message)
    {
        while (true)
        {
            Console.Write(message);
            string s = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value))
                return value;
            Console.WriteLine("Gia tri khong hop le, vui long nhap mot so!");
        }
    }
}
