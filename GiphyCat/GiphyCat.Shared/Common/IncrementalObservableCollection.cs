using System;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace GiphyCat.Common
{
    public class IncrementalObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly Func<bool> _hasMoreItems;
        private readonly Func<uint, IAsyncOperation<LoadMoreItemsResult>> _loadMoreItems;

        public IncrementalObservableCollection(Func<bool> hasMoreItems, Func<uint, IAsyncOperation<LoadMoreItemsResult>> loadMoreItems)
        {
            _hasMoreItems = hasMoreItems;
            _loadMoreItems = loadMoreItems;
        }

        public bool HasMoreItems
        {
            get { return _hasMoreItems(); }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return _loadMoreItems(count);
        }
    }
}
