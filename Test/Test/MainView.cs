using System;
using Xamarin.Forms;
using Notnet.Controls;


namespace Test
{
	public class MainView : TabbedPage
	{
		public MainView ()
		{
			Title="Notnet Controls Tests";
			Children.Add (new TestGridView ());
            Children.Add (new MyListView2 ());
			Children.Add (new MyTabbedPage ());
		}
	}
}

