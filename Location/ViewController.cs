using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;
using Xamarin.Essentials;

namespace Location
{
    public partial class ViewController : UIViewController
    {

        public static double AccuracyHundredMeters { get; }
        private CLLocationManager locationManager;
        private CLLocation location;
        PermissionStatus status;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            MapDelegate mapDelegate = new MapDelegate();
            mapView.Delegate = mapDelegate;

            #region Location ausgeben (Text)
            locationManager = new CLLocationManager();
            
            permissionCheck();
            locationManager.RequestAlwaysAuthorization();

            btnStart.TouchUpInside += BtnStart_TouchUpInside;
            btnEnd.TouchUpInside += BtnEnd_TouchUpInside;
            #endregion
        }

        private async void permissionCheck()
        {
            status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        }


        private void LocationManager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs args)
        {
            location = args.Locations[0];
            double latitude = Math.Round(location.Coordinate.Latitude, 4);
            double longitude = Math.Round(location.Coordinate.Longitude, 4);
            double accuracy = Math.Round(location.HorizontalAccuracy, 0);

            lblOutput.Text = string.Format("Latitude: {0}\nLongitude: {1}\nAccuracy: {2}m", latitude, longitude, accuracy);

            MKCoordinateRegion region = new MKCoordinateRegion();
            region.Center = location.Coordinate;

            mapView.SetRegion(region, true);

            //Aktuelle Position anzeigen
            mapView.ShowsUserLocation = true;
            //Zoom über Pinch-Geste (standardmäßig aktiviert)
            mapView.ZoomEnabled = true;

            mapView.MapType = MKMapType.Standard;

            //Zoom-Level selber setzen
            /* MKCoordinateSpan span = new MKCoordinateSpan();
            span.LatitudeDelta = 0.5;
            span.LongitudeDelta = 0.5;
            region.Span = span;

            mapView.SetRegion(region, true); */

            //Pin auf die Karte setzen
            mapView.AddAnnotation(new MKPointAnnotation()
            {
                Title = "Eifel-Turm",
                Coordinate = new CLLocationCoordinate2D(48.858261, 2.294507)
            });

            MKPointAnnotation firstAnnotation = new MKPointAnnotation();
            firstAnnotation.Coordinate = new CLLocationCoordinate2D(48.858261, 2.294507);
            firstAnnotation.Title = "Eifelturm";

            MKPointAnnotation secondAnnotation = new MKPointAnnotation();
            secondAnnotation.Coordinate = new CLLocationCoordinate2D(48.8583, 2.294507);
            secondAnnotation.Title = "Sehenswürdigkeit";
            

            //Hinzufügen einer Annotation
            //mapView.AddAnnotation(firstAnnotation);

            //Hinzufügen mehrerer Annotations
            mapView.AddAnnotations(new IMKAnnotation[] { firstAnnotation, secondAnnotation });

            
            MKCircle circleOverlay = MKCircle.Circle(location.Coordinate, 10);
            mapView.AddOverlay(circleOverlay);

            MKPolygon areaOverlay = MKPolygon.FromCoordinates(
                new CLLocationCoordinate2D[]
                {
                    new CLLocationCoordinate2D(37.787834, -122.406417),
                    new CLLocationCoordinate2D(37.797834, -122.406417),
                    new CLLocationCoordinate2D(37.797834, -122.416417),
                    new CLLocationCoordinate2D(37.787834, -122.416417),
                    new CLLocationCoordinate2D(37.787834, -122.406417)
                });
            mapView.AddOverlay(areaOverlay);

        }

        private void BtnEnd_TouchUpInside(object sender, EventArgs e)
        {
            locationManager.StopUpdatingLocation();
            lblOutput.Text = "Standortbestimmung beendet...";
        }

        private void BtnStart_TouchUpInside(object sender, EventArgs e)
        {
                locationManager.LocationsUpdated += LocationManager_LocationsUpdated;
                lblOutput.Text = "Bestimme Standort...";
                locationManager.StartUpdatingLocation();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}