using System;
using Xamarin.Forms;
using Notnet.Controls;

namespace Test
{
    public class UserLoginGrid : NCGridView<UserLoginGridCell,UserLoginControlsModel>
    {
        public UserLoginGrid()
        {
        }
    }
    public class UserLoginGridCell : StackLayout
    {
        public UserLoginGridCell()
        {
            Orientation = StackOrientation.Horizontal;
            WidthRequest = 180;
            HeightRequest = 50;
            Spacing = 5;
//            Padding = new Thickness(5, 5, 5, 5);

            var label1 = new Label{ 
                XAlign=TextAlignment.Center, YAlign=TextAlignment.Center, 
                FontSize=12,
                HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.FillAndExpand,

                };
            label1.SetBinding(Label.TextProperty, "LabelText");

            var entry = new Entry{ HorizontalOptions = LayoutOptions.FillAndExpand };
            entry.SetBinding(Entry.PlaceholderProperty, "Placeholder");
            Children.Add(label1);
            Children.Add(entry);
        }
    }
    public class UserLoginControlsModel
    {
        public string LabelText{ get; set;}
        public string Placeholder{ get; set;}
    }
}

