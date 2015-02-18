using System;
using Xamarin.Forms;

namespace Notnet.Controls
{
    public class NCPullRefreshContentView : ContentView
    {
        public static readonly BindableProperty RefreshTextProperty = BindableProperty.Create<NCPullRefreshContentView,string> ((prop) => prop.RefreshText, "");

        public string RefreshText {
            get{ return (string)GetValue (RefreshTextProperty); }
            set{ SetValue (RefreshTextProperty, value); }
        }
        public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create<NCPullRefreshContentView,Command> ((prop) => prop.RefreshCommand, null);

        public Command RefreshCommand {
            get{ return (Command)GetValue (RefreshCommandProperty); }
            set{ SetValue (RefreshCommandProperty, value); }
        }
        public static readonly BindableProperty IsRefreshingProperty = BindableProperty.Create<NCPullRefreshContentView,bool> ((prop) => prop.IsRefreshing, false);

        public bool IsRefreshing {
            get{ return (bool)GetValue (IsRefreshingProperty); }
            set{ SetValue (IsRefreshingProperty, value); }
        }
        public NCPullRefreshContentView()
        {
        }
    }
}
