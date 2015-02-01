using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Test
{
	public class MyListViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<string> Items{get;set;}
		public Command RefreshItems{ get; set; }
		private bool _isRefresing;
		public bool IsRefreshing { 
			get{ return _isRefresing; }
			set {
				if (_isRefresing != value) {
					_isRefresing = value;
					OnPropertyChanged ();
				}
			}
		}
		public MyListViewModel ()
		{
			Items = new ObservableCollection<string> ();
			RefreshItems = new Command (async () => { await RefreshData();});
		}
		private async Task RefreshData()
		{
			if (IsRefreshing)
				return;
			Items.Clear ();
			IsRefreshing = true;
			await Task.Delay(TimeSpan.FromSeconds(2));
			for (var i = 1; i < 20; i++) {
				Items.Add (string.Format ("Test item {0}", i));
			}
			IsRefreshing = false;

		}
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null && propertyName != null) {
				handler(this,new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

