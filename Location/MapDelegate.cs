using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using MapKit;
using UIKit;

namespace Location
{
    class MapDelegate : MKMapViewDelegate
    {
        public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
        {
            MKPolygon polygon = overlay as MKPolygon;
            MKCircle circle = overlay as MKCircle;

            if (polygon != null)
            {
                MKPolygonView mkPolygonView = new MKPolygonView(polygon);

                mkPolygonView.FillColor = UIColor.Red;
                mkPolygonView.StrokeColor = UIColor.Blue;

                return mkPolygonView;
            }

            if (circle != null)
            {
                MKCircleView mKCircleView = new MKCircleView(circle);
                mKCircleView.FillColor = UIColor.FromRGBA(255, 0, 0, 60);
                return mKCircleView;
            }
            return null;
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKPointAnnotation anno = annotation as MKPointAnnotation;

            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation("Marker");
            annotationView = new CustomMKPinAnnotationView(anno, "Marker");

            return annotationView;
        }

        
    }
}