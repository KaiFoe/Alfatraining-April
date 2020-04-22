using CoreLocation;
using Foundation;
using System;
using UIKit;

namespace Location
{
    public partial class ViewController : UIViewController
    {

        public static double AccuracyHundredMeters { get; }
        private CLLocationManager locationManager;
        private CLLocation location;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            #region Location ausgeben (Text)
            locationManager = new CLLocationManager();

            locationManager.RequestAlwaysAuthorization();

            locationManager.LocationsUpdated += LocationManager_LocationsUpdated;

            btnStart.TouchUpInside += BtnStart_TouchUpInside;
            btnEnd.TouchUpInside += BtnEnd_TouchUpInside;
            #endregion
        }

        private void LocationManager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs args)
        {
            location = args.Locations[0];
            double latitude = Math.Round(location.Coordinate.Latitude, 4);
            double longitude = Math.Round(location.Coordinate.Longitude, 4);
            double accuracy = Math.Round(location.HorizontalAccuracy, 0);

            lblOutput.Text = string.Format("Latitude: {0}\nLongitude: {1}\nAccuracy: {2}m", latitude, longitude, accuracy);
        }

        private void BtnEnd_TouchUpInside(object sender, EventArgs e)
        {
            locationManager.StopUpdatingLocation();
            lblOutput.Text = "Standortbestimmung beendet...";
        }

        private void BtnStart_TouchUpInside(object sender, EventArgs e)
        {
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