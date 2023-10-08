using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyCRUD;

public static class ClassFieldDescriptionExtractor
{
	
	private static CRUDFieldDescription ExtractMemberFieldDescription(MemberInfo memberInfo, Type type)
	{
		var attribute = memberInfo.GetCustomAttribute<CRUDFieldAttribute>();

		if(attribute is null)
			return new(memberInfo.Name, memberInfo.Name, false, type);

		return new(memberInfo.Name, attribute.Text ?? memberInfo.Name, attribute.ReadOnly, type);
	}

	public static IEnumerable<CRUDFieldDescription> ExtractProperties<TItem>()
	{
		var type = typeof(TItem);
		var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		foreach (var property in properties)
			yield return ExtractMemberFieldDescription(property, property.PropertyType);
	}

	public static IEnumerable<CRUDFieldDescription> ExtractFields<TItem>()
	{
		var type = typeof(TItem);
		var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
		foreach (var field in fields)
			yield return ExtractMemberFieldDescription(field, field.FieldType);
	}

	public static IEnumerable<CRUDFieldDescription> ExtractsFieldsAndProperties<TItem>() => ExtractFields<TItem>().Concat(ExtractProperties<TItem>());
}
