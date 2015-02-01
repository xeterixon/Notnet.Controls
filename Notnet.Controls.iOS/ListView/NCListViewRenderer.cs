using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Notnet.Controls;
using Notnet.Controls.iOS;
using UIKit;
[assembly: ExportRenderer(typeof(NCListView),typeof(NCListViewRenderer))]
namespace Notnet.Controls.iOS
{
	public class NCListViewRenderer : ListViewRenderer
	{
		private NCRefreshControl _refreshControl;
		public NCListViewRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			if (_refreshControl != null) {
				return;
			}
			var listview = Element as NCListView;
			if (listview.EnablePullToRefresh) {
				_refreshControl = new NCRefreshControl
				{
					Message = listview.RefreshText,
					RefreshCommand = listview.RefreshCommand
				};
				this.Control.AddSubview (_refreshControl);
			}

		}
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (_refreshControl != null) {
				var listview = Element as NCListView;
				if (e.PropertyName == NCListView.IsRefreshingProperty.PropertyName) {
					_refreshControl.IsRefreshing = listview.IsRefreshing;
				}
			}
			if (e.PropertyName == NCListView.ItemsSourceProperty.PropertyName) 
			{
				var vc = ViewController;
				var c = Control;
			}
		}
	}
}

