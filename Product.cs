namespace Lab04_QuanLySanPham;

/// <summary>Sản phẩm. Price và Quantity không được âm.</summary>
public class Product : IEntity
{
    private decimal _price;
    private int _quantity;

    public string MaSP { get; }
    public string TenSP { get; set; }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Don gia khong duoc am.");
            _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("So luong khong duoc am.");
            _quantity = value;
        }
    }

    /// <summary>Khóa của đối tượng (IEntity) chính là mã sản phẩm.</summary>
    public string Id => MaSP;

    public Product(string maSP, string tenSP, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(maSP))
            throw new ArgumentException("Ma san pham khong duoc rong.");
        if (string.IsNullOrWhiteSpace(tenSP))
            throw new ArgumentException("Ten san pham khong duoc rong.");

        MaSP = maSP.Trim();
        TenSP = tenSP.Trim();
        Price = price;        // đi qua setter nên được kiểm tra
        Quantity = quantity;  // đi qua setter nên được kiểm tra
    }

    /// <summary>Thành tiền của sản phẩm = đơn giá * số lượng.</summary>
    public decimal GiaTriTonKho => Price * Quantity;

    public override string ToString()
    {
        return $"{MaSP,-8} | {TenSP,-25} | {Price,14:N0} | {Quantity,6}";
    }
}
