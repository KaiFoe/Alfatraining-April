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
    class CustomMKPinAnnotationView : MKPinAnnotationView
    {
		public CustomMKPinAnnotationView(IMKAnnotation annotation, string annotationId) : base(annotation, annotationId)
		{
		}

		public string FormsIdentifier { get; set; }

		public override UIView HitTest(CGPoint point, UIEvent uievent)
		{
			UIView hitView = base.HitTest(point, uievent);
			if (hitView != null)
				Superview.BringSubviewToFront(this);

			return hitView;
		}
		public override bool PointInside(CGPoint point, UIEvent uievent)
		{
			CGRect rect = Bounds;
			bool isInside = rect.Contains(point);

			if (!isInside)
			{
				foreach (UIView view in Subviews)
				{
					isInside = view.Frame.Contains(point);
					if (isInside)
						//Aktion stehen die ausgeführt wird
						break;
				}
			}

			return isInside;
		}
	}
}