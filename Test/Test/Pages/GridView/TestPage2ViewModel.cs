using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;


namespace Test
{
    public class TestPage2ViewModel
    {
        public ObservableCollection<UserLoginControlsModel> UserLoginControls{ get; set;}

        public TestPage2ViewModel()
        {
            UserLoginControls = new ObservableCollection<UserLoginControlsModel>();
            UserLoginControls.Add(new UserLoginControlsModel{ Placeholder="Username", LabelText="U"});
            UserLoginControls.Add(new UserLoginControlsModel{ Placeholder="Password", LabelText="P"});

        }
    }
}

