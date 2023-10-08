using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCRUD.MudBlazor;

public partial class MudFormCRUDView<TItem, TSelector> : CRUDView<TItem, TSelector>
{
	/// <summary>
	/// Currently shown item, null when loading.
	/// </summary>
	private TItem? CurrentItem { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);
	}

	protected override async Task OnSelectorChanged()
	{
		if (Selector != null && Configuration.GetItem != null)
			CurrentItem = await Configuration.GetItem(Selector);
	}

	private EventCallback<T> CreateTextBoxValueChangedCallback<T>(CRUDFieldDescription field)
	{
		return EventCallback.Factory.Create<T>(this, x => OnTextBoxValueChanged(x, field));
	}

	private async Task OnTextBoxValueChanged<T>(T newValue, CRUDFieldDescription field)
	{
		if (IsLoading || CurrentItem == null)
			return;

		field.SetFieldValue(CurrentItem, newValue);

		if (Configuration.UpdateItem != null)
			await Configuration.UpdateItem(CurrentItem);
	}
}
