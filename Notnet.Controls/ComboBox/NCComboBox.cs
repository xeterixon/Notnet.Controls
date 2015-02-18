using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

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
		public static readonly BindableProperty ListBackgroundColorProperty = BindableProperty.Create<NCComboBox,Color> ((prop) => prop.ListBackgroundColor, Color.Transparent);

		public Color ListBackgroundColor {
			get{ return (Color)GetValue (ListBackgroundColorProperty); }
			set{ SetValue (ListBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create<NCComboBox,Color> ((prop) => prop.ButtonBackgroundColor, Color.Transparent);

		public Color ButtonBackgroundColor {
			get{ return (Color)GetValue (ButtonBackgroundColorProperty); }
			set{ SetValue (ButtonBackgroundColorProperty, value); }
		}
        private IList<ItemHolder> Items{ get; set; }
		public static readonly BindableProperty ListIsVisibleProperty = BindableProperty.Create<NCComboBox,bool> ((prop) => prop.ListIsVisible, false);

		public bool ListIsVisible {
			get{ return (bool)GetValue (ListIsVisibleProperty); }
			set{ SetValue (ListIsVisibleProperty, value); }
		}
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
		protected override void OnPropertyChanged (string propertyName)
		{

			if (propertyName == NCComboBox.ListBackgroundColorProperty.PropertyName) {
				_itemsLayout.BackgroundColor = ListBackgroundColor;
			} else if (propertyName == NCComboBox.ListIsVisibleProperty.PropertyName) {
				if (ListIsVisible) {
					ShowList ();
				} else {
					HideList ();
				}
			}
			else if (propertyName == NCComboBox.ButtonBackgroundColorProperty.PropertyName) 
			{
				if (Children.Any ()) {
					Children [0].BackgroundColor = ButtonBackgroundColor;
				}
			}

		}
        private void SetupUI()
        {
			_listIsVisible = false;
            Children.Clear();
            _itemsLayout.Children.Clear();
            var button = new Button{ 
                Text = Title, 
				BackgroundColor = ButtonBackgroundColor,
                BorderRadius=0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Command = new Command(async ()=>await ToogleList())
            };
            Children.Add(button);
            foreach (var item in Items)
            {
                var b = new Button{ 
                    Text = item.Text, 
                    VerticalOptions = LayoutOptions.FillAndExpand, 
                    FontSize=16, 
					HeightRequest=40,
                    BorderRadius=0,
					BorderWidth = 0,
					BackgroundColor = ListBackgroundColor,
                    CommandParameter = item,
                    Command = new Command(async (obj)=> await ItemSelected(obj))
                };
                _itemsLayout.Children.Add(b);

            }
			_menuHeight = 40 * _itemsLayout.Children.Count;
			_itemsLayout.HeightRequest = 0;
			Children.Add(_itemsLayout);

        }
		private async Task ToogleList()
        {
			if (_listIsVisible)
            {
                await HideList();
            }
            else
            {
                await ShowList();
            }
        }
		private async Task ShowList()
        {
			if (!_listIsVisible) {
				_itemsLayout.Animate ("ShowMenu", new Animation ((d) => {
					_itemsLayout.HeightRequest = _menuHeight * d;
					_listIsVisible = true;
				}, 0, 1, Easing.SpringOut, () => {
					_listIsVisible = true;
				}));
			}

        }
		private async Task HideList()
        {
			if (_listIsVisible)
            {
				_itemsLayout.Animate ("HideMenu", new Animation ((d) => {
					_itemsLayout.HeightRequest = _menuHeight * d;
					_listIsVisible = false;
				}, 1, 0, Easing.SpringIn, () => {
					_listIsVisible = false;
				}));

            }
        }
		private async Task ItemSelected(object obj)
		{
			var holder = obj as ItemHolder;
			((Button)Children[0]).Text = Title + " : " + holder.Text;
			await HideList();
		}

		double _menuHeight = 0;
		private StackLayout _itemsLayout;
		private bool _listIsVisible = false;
        public NCComboBox()
        {
            Spacing = 0;
            _itemsLayout = new StackLayout
            {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 0
            };
            Items = new List<ItemHolder>();
            SetupUI();
        }
    }
}

