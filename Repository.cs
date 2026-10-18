namespace Lab04_QuanLySanPham;

/// <summary>
/// Kho lưu trữ generic trong bộ nhớ. T phải có Id (IEntity).
/// Repository chỉ lo lưu/tìm dữ liệu, không chứa nghiệp vụ.
/// </summary>
public class Repository<T> where T : IEntity
{
    private readonly List<T> _items = new();

    /// <summary>Thêm phần tử mới.</summary>
    public void Add(T item)
    {
        _items.Add(item);
    }

    /// <summary>Xóa phần tử theo Id. Trả về true nếu xóa được.</summary>
    public bool Remove(string id)
    {
        T? item = FindById(id);
        if (item == null) return false;
        return _items.Remove(item);
    }

    /// <summary>Tìm theo Id (không phân biệt hoa thường). Không thấy trả về null.</summary>
    public T? FindById(string id)
    {
        return _items.FirstOrDefault(x =>
            string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>Tìm các phần tử thỏa điều kiện predicate (Func&lt;T,bool&gt;).</summary>
    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return _items.Where(predicate).ToList();
    }

    /// <summary>Lấy tất cả (trả về bản sao để bên ngoài không sửa trực tiếp danh sách).</summary>
    public IReadOnlyList<T> GetAll()
    {
        return _items.ToList();
    }
}
