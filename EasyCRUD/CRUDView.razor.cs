using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCRUD;

[CascadingTypeParameter(nameof(TItem))]
[CascadingTypeParameter(nameof(TSelector))]
public abstract class CRUDView<TItem, TSelector> : ComponentBase
{

	[Parameter, EditorRequired]
	public CRUDConfiguration<TItem, TSelector> Configuration { get; set; } = null!;

	private TSelector? PreviousSelector;
	[Parameter]
	public TSelector? Selector { get; set; }

	[Parameter]
	public Func<IEnumerable<CRUDFieldDescription>> FieldDescriptions { get; set; } = ClassFieldDescriptionExtractor.ExtractsFieldsAndProperties<TItem>;

	public bool IsLoading { get; set; }

	protected abstract Task OnSelectorChanged();

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		if(Selector?.Equals(PreviousSelector) != true)
		{
			IsLoading = true;
			PreviousSelector = Selector;

			OnSelectorChanged().ContinueWith(x =>
			{
				IsLoading = false;
 				_ = InvokeAsync(StateHasChanged);
			});
		}
	}
}

