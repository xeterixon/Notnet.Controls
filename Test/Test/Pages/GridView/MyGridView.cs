using System;

using Xamarin.Forms;
using Notnet.Controls;
namespace Test
{
	public class MyGridViewCell: StackLayout
	{
		public MyGridViewCell()
		{
			HeightRequest = 100;
			WidthRequest = 140;
			BackgroundColor = Color.Gray;
			Orientation = StackOrientation.Vertical;

			var image = new BoxView {

				BackgroundColor = Color.Red,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand

			};
			var label = new Label {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Green
			};
			label.SetBinding (Label.TextProperty, "Text");
			Children.Add (image);
			Children.Add (label);
		}
	}
	public class MyGridViewCellModel
	{
		public string Text{get;set;}
	}
	public class MyGridView : NCGridView<MyGridViewCell,MyGridViewCellModel>
	{

	}
}


