#region

using System;
using Windows.UI.Xaml.Navigation;
using GiphyCat.ViewModel;

#endregion

namespace GiphyCat
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        ///     Gets the view's ViewModel.
        /// </summary>
        public MainViewModel Vm
        {
            get { return (MainViewModel)DataContext; }
        }

        protected override void LoadState(object state)
        {
            //TODO: Load the save page state
        }

        protected override object SaveState()
        {
            //TODO: save page state
            return null;
        }
    }
}