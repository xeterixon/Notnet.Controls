using System;
using Xamarin.Forms;
namespace Test
{
	public class MainView : TabbedPage
	{
		public MainView ()
		{
			Title="Notnet Controls Tests";
			Children.Add (new TestGridView ());
		}
	}
}

