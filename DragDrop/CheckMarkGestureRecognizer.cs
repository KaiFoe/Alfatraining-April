using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace DragDrop
{
    class CheckMarkGestureRecognizer : UIGestureRecognizer
    {
        private CGPoint midPoint = CGPoint.Empty;
        private bool strokeUp = false;

        public override void Reset()
        {
            strokeUp = false;
            midPoint = CGPoint.Empty;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (touches.Count != 1)
                base.State = UIGestureRecognizerState.Failed;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            base.State = UIGestureRecognizerState.Failed;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (base.State == UIGestureRecognizerState.Possible && strokeUp)
                base.State = UIGestureRecognizerState.Recognized;
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            
            if (base.State != UIGestureRecognizerState.Failed)
            {
                CGPoint newPoint = (touches.AnyObject as UITouch).LocationInView(View);
                CGPoint previousPoint = (touches.AnyObject as UITouch).PreviousLocationInView(View);

                if (!strokeUp)
                {
                    if (newPoint.X >= previousPoint.X && newPoint.Y >= previousPoint.Y)
                    {
                        midPoint = newPoint;
                    }
                    else if (newPoint.X >= previousPoint.X && newPoint.Y <= previousPoint.Y)
                    {
                        strokeUp = true;
                    } else
                    {
                        base.State = UIGestureRecognizerState.Failed;
                    }
                }
            }
        }
    }
}