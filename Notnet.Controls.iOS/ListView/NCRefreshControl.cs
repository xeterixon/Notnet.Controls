using System;
using UIKit;
using Xamarin.Forms;


namespace Notnet.Controls.iOS
{
	internal class NCRefreshControl : UIRefreshControl
	{
		private string _message;
		private bool _isRefreshing;
		public bool IsRefreshing
		{
			get{ return _isRefreshing; }
			set{ 
				_isRefreshing = value; 
				if (_isRefreshing) {
					BeginRefreshing ();
				} else {
					EndRefreshing ();
				}
			}
		}
		public Command RefreshCommand{get;set;}
		public string Message{ 
			get{ return _message;}
			set{
				_message = value;
				this.AttributedTitle = new Foundation.NSAttributedString (_message);
			} 
		}
		public NCRefreshControl ()
		{
			this.ValueChanged += async (object sender, EventArgs e) => {
				var command = RefreshCommand;
				if(command != null)
				{
					command.Execute(null);
				}
			};
		}
	}
}

