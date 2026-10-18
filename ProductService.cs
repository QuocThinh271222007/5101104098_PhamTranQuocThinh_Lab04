namespace Lab04_QuanLySanPham;

/// <summary>
/// Xử lý nghiệp vụ sản phẩm: kiểm tra dữ liệu, gọi Repository, phát event.
/// </summary>
public class ProductService
{
    private readonly Repository<Product> _repository = new();

    /// <summary>Event phát ra khi thêm sản phẩm thành công.</summary>
    public event Action<Product>? ProductAdded;

    /// <summary>Event phát ra khi xóa sản phẩm thành công.</summary>
    public event Action<Product>? ProductRemoved;

    /// <exception cref="DuplicateProductException">Mã sản phẩm đã tồn tại.</exception>
    public void AddProduct(Product product)
    {
        if (_repository.FindById(product.MaSP) != null)
            throw new DuplicateProductException(product.MaSP);

        _repository.Add(product);
        ProductAdded?.Invoke(product);
    }

    /// <exception cref="ProductNotFoundException">Mã sản phẩm không tồn tại.</exception>
    public void RemoveProduct(string maSP)
    {
        Product? product = _repository.FindById(maSP);
        if (product == null)
            throw new ProductNotFoundException(maSP);

        _repository.Remove(maSP);
        ProductRemoved?.Invoke(product);
    }

    public IReadOnlyList<Product> GetAll() => _repository.GetAll();

    public Product? FindById(string maSP) => _repository.FindById(maSP);

    /// <summary>Tìm theo tên chứa từ khóa (không phân biệt hoa thường), dùng Func.</summary>
    public IEnumerable<Product> SearchByName(string keyword)
    {
        Func<Product, bool> dieuKien = p =>
            p.TenSP.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        return _repository.Find(dieuKien);
    }

    /// <summary>Lọc theo điều kiện bất kỳ do người gọi truyền vào.</summary>
    public IEnumerable<Product> Filter(Func<Product, bool> predicate)
    {
        return _repository.Find(predicate);
    }

    /// <summary>Lọc theo khoảng giá [minPrice, maxPrice].</summary>
    public IEnumerable<Product> FilterByPrice(decimal minPrice, decimal maxPrice)
    {
        return Filter(p => p.Price >= minPrice && p.Price <= maxPrice);
    }

    /// <summary>Tổng giá trị kho = tổng (đơn giá * số lượng).</summary>
    public decimal TinhTongGiaTriKho()
    {
        return _repository.GetAll().Sum(p => p.GiaTriTonKho);
    }
}
