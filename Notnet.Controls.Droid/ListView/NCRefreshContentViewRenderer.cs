using System;
using Android.Support.V4.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Notnet.Controls;
using Notnet.Controls.Droid;
using Android.Views;

[assembly : ExportRenderer(typeof(NCPullRefreshContentView),typeof(NCPullRefreshContentViewRenderer))]
namespace Notnet.Controls.Droid
{
    public class NCPullRefreshContentViewRenderer : SwipeRefreshLayout,  IVisualElementRenderer,  SwipeRefreshLayout.IOnRefreshListener
    {
        private bool isInit;
        private ViewGroup viewGroup;
        public NCPullRefreshContentViewRenderer():base(Forms.Context)
        {
            isInit = false;
        }
        private NCPullRefreshContentView TheView
        {
            get
            { 
                return Element == null ?  null : (NCPullRefreshContentView)Element; 
            }
        }

        public void OnRefresh()
        {
            if (TheView == null)
            {
                return;
            }
            var command = TheView.RefreshCommand;
            if (command != null)
            {
                command.Execute(null);
            }
        }

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        public void SetElement(Xamarin.Forms.VisualElement element)
        {
            var oldElement = Element;
            if (oldElement != null)
            {
                oldElement.PropertyChanged -= HandlePropertyChanged;
            }
            Element = element;
            if (Element != null)
            {
                UpdateContent();
                Element.PropertyChanged += HandlePropertyChanged;
            }
            if (!isInit)
            {
                isInit = true;
                Tracker = new VisualElementTracker(this);
                SetOnRefreshListener(this);

            }
            if (ElementChanged != null)
            {
                ElementChanged(this, new VisualElementChangedEventArgs(oldElement, Element));
            }
        }

        void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NCPullRefreshContentView.IsRefreshingProperty.PropertyName) {
                this.Refreshing = this.TheView.IsRefreshing;
            } else if (e.PropertyName == "Content") {
                UpdateContent ();
            }
        }
        public override bool Refreshing
        {
            get
            {
                return base.Refreshing;
            }
            set
            {
                base.Refreshing = value;
            }
        }
        public override bool CanChildScrollUp()
        {
            var listview = viewGroup.GetChildAt(0) as Android.Widget.ListView;
            if (listview != null)
            {
                if (listview.FirstVisiblePosition == 0)
                {
                    var item = listview.GetChildAt(0);
                    return item != null && item.Top != 0;
                }
                return false;
            }
            return false;
        }
        public Xamarin.Forms.SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            viewGroup.Measure(widthConstraint, heightConstraint);
            return new SizeRequest(new Size(viewGroup.MeasuredWidth, viewGroup.MeasuredHeight));
        }
        private void UpdateContent()
        {
            if (TheView.Content == null)
            {
                return;
            }
            if (viewGroup != null)
            {
                RemoveView(viewGroup);
            }
            var renderer  = RendererFactory.GetRenderer(TheView.Content);
            viewGroup = renderer.ViewGroup;
            AddView(viewGroup);

        }
        public void UpdateLayout()
        {
            if (Tracker != null)
            {
                Tracker.UpdateLayout();
            }
        }
        public VisualElementTracker Tracker{ get; private set; }

        public Android.Views.ViewGroup ViewGroup
        {
            get
            {
                return this;
            }
        }

        public Xamarin.Forms.VisualElement Element{ get; private set; }
    }
}

