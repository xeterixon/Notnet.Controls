using System;
using Xamarin.Forms;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Notnet.Controls
{
	/// <summary>
	/// Gallery view.
	/// The ItemsSource property should be bound to a ObservableCollection.
	/// </summary>
	public class GridView<TViewCell,TModel> : WrapLayout
		where TViewCell: View, new()
	{
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<GridView<TViewCell,TModel>,IEnumerable> ((prop) => prop.ItemsSource, default(IEnumerable), 
			propertyChanged:(bindable, oldValue, newValue) =>  ((GridView<TViewCell,TModel>)bindable).ItemsSourceChanges(),
			propertyChanging:(bindable, oldValue, newValue) =>  ((GridView<TViewCell,TModel>)bindable).ItemsSourceAboutToChange()
		);


		public IEnumerable ItemsSource {
			get{ return (IEnumerable)GetValue (ItemsSourceProperty); }
			set{ SetValue (ItemsSourceProperty, value); }
		}
		private void ItemsSourceChanges()
		{
			var occ = this.ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged += HandleCollectionChanged;
				AddItems (ItemsSource);
			}

		}
		private void ItemsSourceAboutToChange()
		{
			var occ = ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged -= HandleCollectionChanged;
				Children.Clear ();
			}
		}
		private void AddItems(IEnumerable list)
		{
			foreach (var item in list)
			{
				Children.Add ( new TViewCell{ BindingContext = item});
			}

		}

		protected void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			//TODO Handle more action
			if (e.Action == NotifyCollectionChangedAction.Reset) {
				Children.Clear ();
			}
			else if (e.Action == NotifyCollectionChangedAction.Add) 
			{
				if (e.NewItems != null) 
				{
					AddItems (e.NewItems);
				}
			}
			
		}
		public GridView ()
		{
			Orientation = StackOrientation.Horizontal;
		}
	}
}

