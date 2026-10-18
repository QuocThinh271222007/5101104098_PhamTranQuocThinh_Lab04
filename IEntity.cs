namespace Lab04_QuanLySanPham;

/// <summary>
/// Giao diện cho mọi đối tượng có khóa chính dạng chuỗi.
/// Dùng làm ràng buộc generic: Repository&lt;T&gt; where T : IEntity.
/// </summary>
public interface IEntity
{
    string Id { get; }
}
