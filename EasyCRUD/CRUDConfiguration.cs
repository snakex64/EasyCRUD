
namespace EasyCRUD;

public class CRUDConfiguration<TItem, TSelector>
{
	public Func<TSelector, Task<IEnumerable<TItem>>>? GetItems { get; set; }

	public Func<TSelector, Task<TItem>>? GetItem { get; set; }

	public Func<TItem, Task>? CreateItem { get; set; }

	public Func<TItem, Task>? UpdateItem { get; set; }

	public Func<TItem, Task>? DeleteItem { get; set; }
}
