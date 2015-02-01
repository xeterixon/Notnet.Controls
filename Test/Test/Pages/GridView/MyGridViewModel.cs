using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;


namespace Test
{
	public class MyGridViewModel
	{
		public ObservableCollection<MyGridViewCellModel> Items{ get; set;}
		public MyGridViewModel ()
		{
			Items = new ObservableCollection<MyGridViewCellModel> ();
			for (var i = 1; i < 5; i++) 
			{
				Items.Add (new 	MyGridViewCellModel{ Text = string.Format("Text {0}",i) });
			}
			int counter = 0;
			Device.StartTimer (TimeSpan.FromSeconds (2), () => {
				++counter;
				Items.Add (new MyGridViewCellModel{ Text = string.Format("Timer {0}",counter) });

				return  counter < 10;
			});
		}
	}
}

