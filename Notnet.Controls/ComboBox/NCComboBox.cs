using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Notnet.Controls
{
    internal class ItemHolder
    {
        public int Index{get;set;}
        public string Text{get;set;}
    }
    public class NCComboBox : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<NCComboBox,IEnumerable>((obj) => obj.ItemsSource, default(IEnumerable), propertyChanged:(bindable,oldValue,newValue)=>(bindable as NCComboBox).OnItemsSourceChanged(oldValue,newValue));

        public IEnumerable ItemsSource
        {
            get{ return (IEnumerable)GetValue(ItemsSourceProperty); }
            set{ SetValue(ItemsSourceProperty, value); }
        }
        public static readonly BindableProperty PropertyDisplayNameProperty = BindableProperty.Create<NCComboBox,string>((obj) => obj.PropertyDisplayName, string.Empty);

        public string PropertyDisplayName
        {
            get{ return (string)GetValue(PropertyDisplayNameProperty); }
            set{ SetValue(PropertyDisplayNameProperty, value); }
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create<NCComboBox,string>((obj) => obj.Title, string.Empty);

        public string Title
        {
            get{ return (string)GetValue(TitleProperty); }
            set{ SetValue(TitleProperty, value); }
        }

        private IList<ItemHolder> Items{ get; set; }
        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            Items.Clear();
            HideList();
            if (newValue == null)
                return;
            int index = 0;
            foreach (var item in newValue)
            {
                string displayText = item.ToString();
                if (!string.IsNullOrEmpty(PropertyDisplayName))
                {
                    var pinfo = item.GetType().GetRuntimeProperty(PropertyDisplayName);
                    if (pinfo != null)
                    {
                        var value = pinfo.GetValue(item);
                        if (value != null)
                        {
                            displayText = value.ToString();
                        }
                    }
                }
                Items.Add(new ItemHolder{Text= displayText, Index = index++});
               
            }
            SetupUI();
        }
        private void SetupUI()
        {
            ListIsVisible = false;
            Children.Clear();
            ItemsLayout.Children.Clear();
            var button = new Button{ 
                Text = Title, 
                BackgroundColor = Color.FromHex("#ACACAC"),
                BorderRadius=0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(()=>ToogleList())
            };
            Children.Add(button);
            foreach (var item in Items)
            {
                var b = new Button{ 
                    Text = item.Text, 
                    VerticalOptions = LayoutOptions.FillAndExpand, 
                    FontSize=14, HeightRequest=26,
                    BorderRadius=0,
                    CommandParameter = item,
                    Command = new Command((obj)=> ItemSelected(obj))
                };
                ItemsLayout.Children.Add(b);

            }
        }
        private void ToogleList()
        {
            if (ListIsVisible)
            {
                HideList();
            }
            else
            {
                ShowList();
            }
        }
        private void ShowList()
        {
            Children.Add(ItemsLayout);
            ListIsVisible = true;
        }
        private void ItemSelected(object obj)
        {
            var holder = obj as ItemHolder;
            ((Button)Children[0]).Text = Title + " : " + holder.Text;
            HideList();
        }
        private void HideList()
        {
            if (ListIsVisible)
            {
                Children.Remove(ItemsLayout);
                ListIsVisible = false;
            }
        }
        private StackLayout ItemsLayout;
        private bool ListIsVisible = false;
        public NCComboBox()
        {
            Spacing = 0;
            ItemsLayout = new StackLayout
            {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 0
            };
            Items = new List<ItemHolder>();
            SetupUI();
        }
    }
}

