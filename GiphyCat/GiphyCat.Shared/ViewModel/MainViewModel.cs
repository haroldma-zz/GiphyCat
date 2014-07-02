using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GiphyCat.Common;
using GiphyCat.DataService;
using GiphyCat.Model;

namespace GiphyCat.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IGiphyDataService _dataService;
        private readonly INavigationService _navigationService;
        private IncrementalObservableCollection<GiphyItem> _trending;
        private GiphyPagination _trendingPagination;

        /// <summary>
        /// Gets the TrendingItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IncrementalObservableCollection<GiphyItem> TrendingItems
        {
            get
            {
                return _trending;
            }

            set
            {
                Set(ref _trending, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IGiphyDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize();
        }

        /// <summary>
        /// Initializes the data using IGiphyDataService
        /// </summary>
        private async void Initialize()
        {
            _trendingPagination = new GiphyPagination() {offset = -1, total_count = 5};

            TrendingItems = new IncrementalObservableCollection<GiphyItem>(
                () => _trendingPagination.offset < _trendingPagination.total_count,
                (uint count) =>
                {
                    Func<Task<LoadMoreItemsResult>> taskFunc = async () =>
                    {
                        var feed = await _dataService.SearchAsync("reddit",offset: _trendingPagination.offset + 1);

                        foreach (var t in feed.data)
                            TrendingItems.Add(t);

                        _trendingPagination = feed.pagination;

                        return new LoadMoreItemsResult()
                        {
                            Count = (uint)feed.data.Count
                        };
                    };
                    var loadMorePostsTask = taskFunc();
                    return loadMorePostsTask.AsAsyncOperation();
                });
        }
    }
}