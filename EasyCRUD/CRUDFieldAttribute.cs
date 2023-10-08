using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCRUD;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class CRUDFieldAttribute : Attribute
{
	public string? Text { get; set; }

	public bool ReadOnly { get; set; }
}
