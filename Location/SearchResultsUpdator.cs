using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Location
{
    public class SearchResultsUpdator : UISearchResultsUpdating
    {
        public event Action<string> UpdateSearchResults = delegate { };
        public override void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            UpdateSearchResults(searchController.SearchBar.Text);
        }
    }
}