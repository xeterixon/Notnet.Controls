using System;
using System.Collections.Generic;

namespace Test
{
    public class Item
    {
        public string Text{get;set;}
    }
    public class MyComboViewModel
    {
        public List<Item> MyItems{ get; set;}
        public MyComboViewModel()
        {
            MyItems = new List<Item>();
            MyItems.Add(new Item{ Text = "Sing" });
            MyItems.Add(new Item{ Text = "Listen to music" });
            MyItems.Add(new Item{ Text = "Go eat" });
            MyItems.Add(new Item{ Text = "Play soccer" });
            MyItems.Add(new Item{ Text = "Bake a cake" });
            MyItems.Add(new Item{ Text = "Do some other things" });
        }
    }
}

