using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace DragDrop
{
    public partial class TouchViewController : UIViewController
    {
        #region Private Variablen
        private bool imageHighlighted = false;
        private bool touchStartedInside;
        #endregion

        #region Konstruktor
        public TouchViewController (IntPtr handle) : base (handle)
        {
        }
        #endregion

        #region Überladene Methoden
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            //Wennn MultiTouch enabled ist, gebe Anzahl der Finger die tappen zurück
            lblStatus.Text = string.Format("Number of fingers: {0}", touches.Count);

            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                //Checke ob eins der Bilder angetappt wurde
                if (imgTouch.Frame.Contains(touch.LocationInView(viewTouch)))
                {
                    //Erstes Bild getouched
                    imgTouch.Image = UIImage.FromBundle("TouchMe_Touched.png");
                    lblStatus.Text = "Touches began";
                } else if (touch.TapCount == 2 && imgTap.Frame.Contains(touch.LocationInView(viewTouch)))
                {
                    //Zweites Bild duoble-Touch
                    if (imageHighlighted)
                    {
                        imgTap.Image = UIImage.FromBundle("DoubleTapMe.png");
                        lblStatus.Text = "Double tapped off";
                    } else 
                    {
                        imgTap.Image = UIImage.FromBundle("DoubleTapMe_Highlighted.png");
                        lblStatus.Text = "Double tapped on";
                    }
                    imageHighlighted = !imageHighlighted;
                } else if (imgDrag.Frame.Contains(touch.LocationInView(View)))
                {
                    //Drittes Bild getouched, Vorbereiten für "drag"
                    touchStartedInside = true;
                }
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            //setze unsere Hilfsvariable "touchStartedInside" wieder zurück
            touchStartedInside = false;
            imgTouch.Image = UIImage.FromBundle("TouchMe.png");
            lblStatus.Text = "";
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            UITouch touch = (UITouch)touches.AnyObject;
            if (touch != null)
            {
                //Touch Image
                if(imgTouch.Frame.Contains(touch.LocationInView(viewTouch)))
                {
                    imgTouch.Image = UIImage.FromBundle("TouchMe.png");
                    lblStatus.Text = "Touch ended";
                }
                //Zürcksetzen unserer Hilfsvariable
                touchStartedInside = false;
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            UITouch touch = (UITouch)touches.AnyObject;
            if (touch != null)
            {
                //TouchImage ausgewählt
                if (imgTouch.Frame.Contains(touch.LocationInView(viewTouch)))
                    lblStatus.Text = "Touches moved";
                //DragImage
                //Schauen ob wir innerhalb des DragImage getouched haben
                if (touchStartedInside)
                {
                    //Bewege das Image
                    nfloat offsetX = touch.PreviousLocationInView(View).X - touch.LocationInView(View).X;
                    nfloat offsetY = touch.PreviousLocationInView(View).Y - touch.LocationInView(View).Y;

                    imgDrag.Frame = new CGRect(new CGPoint(imgDrag.Frame.X - offsetX, imgDrag.Frame.Y - offsetY), imgDrag.Frame.Size);
                }
            }

        }
        #endregion
    }
}