using System;
using Xamarin.Forms;

namespace Notnet.Controls
{
	public class NCTabbedView : ContentView
	{
		public NCTabBar TabBar{ get; set;}
		public NCTabbedView ()
		{
			var stack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Spacing = 0,
			};
			var label = new Label{ 
				Text="TTT", 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			TabBar = new NCTabBar ();

			for (int i = 0; i < 5; i++) 
			{
				var str = string.Format("{0}",i);
				TabBar.AddTab (new Label{ Text = str }, str);

			}
			stack.Children.Add (label);

			stack.Children.Add (TabBar);
			TabBar.SetupTabBar ();
			Content = stack;
		}
	}
}

