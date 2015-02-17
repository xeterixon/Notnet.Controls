using System;

using Xamarin.Forms;

namespace Test
{
    public class MyListView2 : ContentPage
    {
        public MyListView2()
        {
            Content = new StackLayout
            { 
                Children =
                {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


