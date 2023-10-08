using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyCRUD;

public record class CRUDFieldDescription(string Name, string Text, bool ReadOnly, Type FieldType)
{
	public virtual object? GetFieldValue(object? item)
	{
		if (item == null)
			return null;

		var member = item.GetType().GetMember(Name)[0];
		if (member is FieldInfo fieldInfo)
			return fieldInfo.GetValue(item);
		else if (member is PropertyInfo propertyInfo)
			return propertyInfo.GetValue(item);
		else
			throw new InvalidOperationException($"Member {Name} is not a field or property.");
	}

	public virtual void SetFieldValue(object currentItem, object? newValue)
	{
		if (currentItem == null)
			return;

		var member = currentItem.GetType().GetMember(Name)[0];
		if (member is FieldInfo fieldInfo)
			fieldInfo.SetValue(currentItem, newValue);
		else if (member is PropertyInfo propertyInfo)
			propertyInfo.SetValue(currentItem, newValue);
		else
			throw new InvalidOperationException($"Member {Name} is not a field or property.");
	}
}
