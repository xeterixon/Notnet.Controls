using System;
using Xamarin.Forms;

namespace Notnet.Controls
{
	public class NCListView : ListView
	{
		public static readonly BindableProperty RefreshTextProperty = BindableProperty.Create<NCListView,string> ((prop) => prop.RefreshText, "");

		public string RefreshText {
			get{ return (string)GetValue (RefreshTextProperty); }
			set{ SetValue (RefreshTextProperty, value); }
		}
		public static readonly BindableProperty EnablePullToRefreshProperty = BindableProperty.Create<NCListView,bool> ((prop) => prop.EnablePullToRefresh, false);

		public bool EnablePullToRefresh {
			get{ return (bool)GetValue (EnablePullToRefreshProperty); }
			set{ SetValue (EnablePullToRefreshProperty, value); }
		}
		public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create<NCListView,Command> ((prop) => prop.RefreshCommand, null);

		public Command RefreshCommand {
			get{ return (Command)GetValue (RefreshCommandProperty); }
			set{ SetValue (RefreshCommandProperty, value); }
		}
		public static readonly BindableProperty IsRefreshingProperty = BindableProperty.Create<NCListView,bool> ((prop) => prop.IsRefreshing, false);

		public bool IsRefreshing {
			get{ return (bool)GetValue (IsRefreshingProperty); }
			set{ SetValue (IsRefreshingProperty, value); }
		}
		public NCListView ()
		{
		}
	}
}

