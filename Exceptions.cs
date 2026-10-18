namespace Lab04_QuanLySanPham;

/// <summary>Phát sinh khi thêm sản phẩm có mã bị trùng.</summary>
public class DuplicateProductException : Exception
{
    public string MaSP { get; }

    public DuplicateProductException(string maSP)
        : base($"Ma san pham '{maSP}' da ton tai.")
    {
        MaSP = maSP;
    }
}

/// <summary>Phát sinh khi xóa hoặc sửa sản phẩm không tồn tại.</summary>
public class ProductNotFoundException : Exception
{
    public string MaSP { get; }

    public ProductNotFoundException(string maSP)
        : base($"Khong tim thay san pham co ma '{maSP}'.")
    {
        MaSP = maSP;
    }
}
