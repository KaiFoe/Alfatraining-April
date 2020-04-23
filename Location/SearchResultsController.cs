using CoreLocation;
using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Location
{
    internal class SearchResultsController : UITableViewController
    {
        private MKMapView mapView;
        static readonly string mapItemCellId = "mapItemCellId";

        public List<MKMapItem> MapItems { get; set; }

        public SearchResultsController(MKMapView mapView)
        {
            this.mapView = mapView;
            MapItems = new List<MKMapItem>();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return MapItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(mapItemCellId);

            if (cell == null)
                cell = new UITableViewCell();

            cell.TextLabel.Text = MapItems[indexPath.Row].Name;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //Item der Map hinzufügen
            CLLocationCoordinate2D coord = MapItems[indexPath.Row].Placemark.Location.Coordinate;
            mapView.AddAnnotations(new MKPointAnnotation()
            {
                Title = MapItems[indexPath.Row].Name,
                Coordinate = coord
            });
            mapView.SetCenterCoordinate(coord, true);
            DismissViewController(false, null);
        }

        public void Search (string forSearchString)
        {
            //erstelle SUchanfrage
            var searchRequest = new MKLocalSearchRequest();
            searchRequest.NaturalLanguageQuery = forSearchString;
            searchRequest.Region = new MKCoordinateRegion(mapView.UserLocation.Coordinate, new MKCoordinateSpan(0.25, 0.25));

            var localSearch = new MKLocalSearch(searchRequest);

            localSearch.Start(delegate (MKLocalSearchResponse response, NSError error)
            {
                if (response != null && error == null)
                {
                    this.MapItems = response.MapItems.ToList();
                    this.TableView.ReloadData();
                }
                else
                {

                }
            });
        }
    }
}