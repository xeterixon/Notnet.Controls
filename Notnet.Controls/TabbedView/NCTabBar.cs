using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Notnet.Controls
{
	public class TabButtonContainer
	{
		public View View{get;set;}
		public Button Button{get;set;}
		public string Name{get;set;}
		public View DisplayView
		{
			get{ return Button ?? View; }
		}
	}
	public class NCTabBar : ContentView
	{
		private Grid _grid;
		public Dictionary<string,TabButtonContainer> Pages{ get; private set; }
		public NCTabBar ()
		{
			HorizontalOptions = LayoutOptions.FillAndExpand;
			Pages = new Dictionary<string, TabButtonContainer> ();
			_grid = new Grid
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				ColumnSpacing = 0
			};
			_grid.RowDefinitions.Add (new RowDefinition{ Height = 6});
			_grid.RowDefinitions.Add (new RowDefinition{ Height = 50 });

			_grid.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength(1,GridUnitType.Star)});
			for (int i = 0; i < 5; i++) 
			{
				_grid.ColumnDefinitions.Add (new ColumnDefinition{ Width = 60});
			}
			_grid.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength(1,GridUnitType.Star)});


			AddTab(new Label{ Text = " ", HorizontalOptions = LayoutOptions.FillAndExpand },"RightExpander",false);

			Content = _grid;
		}
		private void ShowPage(string page)
		{
		}
		public void AddTab(View view, string name, bool createButton = true)
		{
			var c = new TabButtonContainer {
				Name = name,
				View = view,
			};
			if (createButton) 
			{
				c.Button = new Button {
					Text = name,
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					BackgroundColor = Color.Blue,
					BorderRadius = 0,
					CommandParameter = name,
					//Command = new Command(async (obj) => await ShowPage(obj))

				};
			}
			Pages.Add (name, c);
		}
		public void SetupTabBar()
		{
			AddTab(new Label{ Text = "", HorizontalOptions = LayoutOptions.FillAndExpand },"LeftExpander",false);

			var bigIndex = (Pages.Count-1) / 2;
			int counter = 0;
			foreach (var kv in Pages) 
			{
				kv.Value.DisplayView.BackgroundColor = Color.FromHex ("#4775A3");
				if (counter == bigIndex) {
					var v = kv.Value.Button;
					v.BackgroundColor = Color.White;
					v.BorderColor = Color.Black;
					v.BorderRadius = 2;
					v.BorderWidth = 1;
					_grid.Children.Add (kv.Value.DisplayView, counter, counter + 1, 0, 2);
				} else {
					_grid.Children.Add (kv.Value.DisplayView, counter, 1);
				}
				counter++;
			}
		}
	}
}

