using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Specialized;

namespace Notnet.Controls
{
    internal class ItemHolder
    {
        public int Index{get;set;}
        public string Text{get;set;}
    }
    public class NCComboBox : StackLayout
    {
		private int currentIndex = 0;
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<NCComboBox,IEnumerable> ((prop) => prop.ItemsSource, default(IEnumerable), 
			propertyChanged:(bindable, oldValue, newValue) =>  ((NCComboBox)bindable).ItemsSourceChanged(),
			propertyChanging:(bindable, oldValue, newValue) =>  ((NCComboBox)bindable).ItemsSourceAboutToChange()
		);


		public IEnumerable ItemsSource {
			get{ return (IEnumerable)GetValue (ItemsSourceProperty); }
			set{ SetValue (ItemsSourceProperty, value); }
		}
		private void ItemsSourceChanged()
		{
			var occ = this.ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged += HandleCollectionChanged;
				AddItems (ItemsSource);
				SetupMenu();
			}

		}

		private void ItemsSourceAboutToChange()
		{
			var occ = ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged -= HandleCollectionChanged;
				ResetMenuItems ();
			}
		}
		protected void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			//TODO Handle more action
			if (e.Action == NotifyCollectionChangedAction.Reset) {
				ResetMenuItems ();
			} else if (e.Action == NotifyCollectionChangedAction.Remove) {
				ResetMenuItems ();
				AddItems (sender as IEnumerable);
			}
			else if (e.Action == NotifyCollectionChangedAction.Add) 
			{
				if (e.NewItems != null) 
				{
					AddItems (e.NewItems);
				}
			}

		}

        public static readonly BindableProperty PropertyDisplayNameProperty = BindableProperty.Create<NCComboBox,string>((obj) => obj.DisplayName, string.Empty);

        public string DisplayName
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
		public static readonly BindableProperty ListBackgroundColorProperty = BindableProperty.Create<NCComboBox,Color> ((prop) => prop.ListBackgroundColor, (Color)Button.BackgroundColorProperty.DefaultValue);

		public Color ListBackgroundColor {
			get{ return (Color)GetValue (ListBackgroundColorProperty); }
			set{ SetValue (ListBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create<NCComboBox,Color> ((prop) => prop.ButtonBackgroundColor, (Color)Button.BackgroundColorProperty.DefaultValue);

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
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create<NCComboBox,int>((obj) => obj.SelectedIndex, -1,BindingMode.OneWayToSource);

        public int SelectedIndex
        {
            get{ return (int)GetValue(SelectedIndexProperty); }
            set{ SetValue(SelectedIndexProperty, value); }
        }
		private void ResetMenuItems()
		{
			Items.Clear ();
			currentIndex = 0;
		}
        private void AddItems( IEnumerable newValue)
        {
			HideList();
            if (newValue == null)
                return;
            foreach (var item in newValue)
            {
                string displayText = item.ToString();
                if (!string.IsNullOrEmpty(DisplayName))
                {
                    var pinfo = item.GetType().GetRuntimeProperty(DisplayName);
                    if (pinfo != null)
                    {
                        var value = pinfo.GetValue(item);
                        if (value != null)
                        {
                            displayText = value.ToString();
                        }
                    }
                }
				Items.Add(new ItemHolder{Text= displayText, Index = currentIndex++});

            }
			SetupMenu ();

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
			} else if (propertyName == NCComboBox.ButtonBackgroundColorProperty.PropertyName) {
				if (Children.Any ()) {
					Children [0].BackgroundColor = ButtonBackgroundColor;
				}
			} else if (propertyName == NCComboBox.TitleProperty.PropertyName) {
				SetupTitle (Title);
			}

		}
		private void SetupMenu()
		{
			_listIsVisible = false;
			_itemsLayout.Children.Clear();
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
					Command = new Command((obj)=>  ItemSelected(obj))
				};
				_itemsLayout.Children.Add(b);

			}
			_menuHeight = 40 * _itemsLayout.Children.Count;
			_itemsLayout.HeightRequest = 0;

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
                Command = new Command(()=>ToogleList())
            };
            Children.Add(button);
			SetupMenu ();
            Children.Add(_scrollView);

        }
		private void ToogleList()
        {
			if (_listIsVisible)
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
			if (!_listIsVisible) {
				_itemsLayout.Animate ("ShowMenu", new Animation ((d) => {
					_itemsLayout.HeightRequest = _menuHeight * d;
				}, 0, 1, Easing.SpringOut),
                    finished:(d,b)=>{
                    _listIsVisible = true;

                });
			}

        }
		private void HideList()
        {
			if (_listIsVisible)
            {
				_itemsLayout.Animate ("HideMenu", new Animation ((d) => {
					_itemsLayout.HeightRequest = _menuHeight * d;
				}, 1, 0, Easing.SpringIn),
                    finished:(d,b)=>{
                    _listIsVisible = false;

                });

            }
        }
		private void SetupTitle(string append)
		{
			
			((Button)Children [0]).Text = Title;
			var title = Title;
			if(Title != append){
				title = Title + " : " + append;
			}
			((Button)Children [0]).Text = title;

		}
		private void ItemSelected(object obj)
		{
			var holder = obj as ItemHolder;
			SetupTitle (holder.Text);	
			HideList();
            SelectedIndex = holder.Index;
		}

		double _menuHeight = 0;
		private StackLayout _itemsLayout;
		private bool _listIsVisible = false;
        private ScrollView _scrollView;
        public NCComboBox()
        {
            Spacing = 0;
            _itemsLayout = new StackLayout
            {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 0
            };
            _scrollView = new ScrollView
            {
                    Orientation= ScrollOrientation.Vertical,
                    Content = _itemsLayout
            };
            Items = new List<ItemHolder>();
            SetupUI();
        }
    }
}
