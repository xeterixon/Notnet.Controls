using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Test
{
    public class Item
    {
        public string Text{get;set;}
    }
    public class MyComboViewModel : INotifyPropertyChanged
    {
        public ObservableCollection <Item> MyItems{ get; set;}
        int _selectedIndex;
        public int SelectedIndex
        {
            get{ return _selectedIndex; }
            set{
				_selectedIndex = value; 
				OnPropertyChanged("WhatToDo");
				if (_selectedIndex == 0) {
					SetList0 ();
				} else if (_selectedIndex == 1) {
					SetList1 ();
				} else if (_selectedIndex == 2) {
					RemoveLast ();
				}
			}
        }
        public string WhatToDo
        {
            get
            {
                if (_selectedIndex >= 0)
                {
                    return MyItems[_selectedIndex].Text;
                }
                return "Nothing selected";
            }
        }
        public MyComboViewModel()
        {
			MyItems = new ObservableCollection<Item>();
            MyItems.Add(new Item{ Text = "Sing" });
            MyItems.Add(new Item{ Text = "Listen to music" });
            MyItems.Add(new Item{ Text = "Go eat" });
            MyItems.Add(new Item{ Text = "Play soccer" });
            MyItems.Add(new Item{ Text = "Bake a cake" });
            MyItems.Add(new Item{ Text = "Do some other things" });
        }
		private void RemoveLast()
		{
			MyItems.RemoveAt (MyItems.Count - 1);
		}
		private void SetList0()
		{
			MyItems.Clear ();
			MyItems.Add(new Item{ Text = "Sing" });
			MyItems.Add(new Item{ Text = "Listen to music" });
			MyItems.Add(new Item{ Text = "Listen to music" });
			MyItems.Add(new Item{ Text = "Listen to music" });
		}
		private void SetList1()
		{
			MyItems.Clear ();
			MyItems.Add(new Item{ Text = "Sing" });
			MyItems.Add(new Item{ Text = "Listen to music" });
			MyItems.Add(new Item{ Text = "Go eat" });
			MyItems.Add(new Item{ Text = "Play soccer" });
			MyItems.Add(new Item{ Text = "Bake a cake" });
			MyItems.Add(new Item{ Text = "Do some other things" });
		}

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (name == null)
                return;
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

