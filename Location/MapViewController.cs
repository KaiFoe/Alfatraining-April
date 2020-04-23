using Foundation;
using MapKit;
using System;
using UIKit;

namespace Location
{
    public partial class MapViewController : UIViewController
    {
        private UISearchController searchController;

        public MapViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Erstelle eine Map
            MKMapView mapView = new MKMapView(UIScreen.MainScreen.Bounds);
            View = mapView;

            //Erstelle eine Instanz des SearchVontrollers
            var searchResultsController = new SearchResultsController(mapView);

            //Erstelle einen Updator für den SearchController
            var searchUpdator = new SearchResultsUpdator();
            searchUpdator.UpdateSearchResults += searchResultsController.Search;

            //Hinzufügen des SearchControllers
            searchController = new UISearchController(searchResultsController)
            {
                SearchResultsUpdater = searchUpdator
            };
        }
    }
}