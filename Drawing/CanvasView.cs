using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Drawing
{
    class CanvasView : UIView
    {
        List<Polyline> completedPolylines = new List<Polyline>();
        Dictionary<IntPtr, Polyline> inProgressPolylines = new Dictionary<IntPtr, Polyline>();

        public CGColor StrokeColor { get; set; } = new CGColor(1.06f, 0, 0);
        public float StrokeWidth { get; set; } = 2;

        public CanvasView()
        {
            BackgroundColor = UIColor.White;
            MultipleTouchEnabled = false;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.SetLineCap(CGLineCap.Butt);
                context.SetLineJoin(CGLineJoin.Round);

                foreach(Polyline polyline in completedPolylines)
                {
                    context.SetStrokeColor(polyline.Color);
                    context.SetLineWidth(polyline.Thickness);
                    context.AddPath(polyline.Path);
                    context.DrawPath(CGPathDrawingMode.Stroke);
                }

                foreach(Polyline polyline in inProgressPolylines.Values)
                {
                    context.SetStrokeColor(polyline.Color);
                    context.SetLineWidth(polyline.Thickness);
                    context.AddPath(polyline.Path);
                    context.DrawPath(CGPathDrawingMode.Stroke);
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                Polyline polyline = new Polyline
                {
                    Color = StrokeColor,
                    Thickness = StrokeWidth
                };

                //Alternative
                /*Polyline polyline = new Polyline();
                  polyline.Color = StrokeColor;
                  polyline.Thickness = StrokeWidth;
                */

                polyline.Path.MoveToPoint(touch.LocationInView(this));
                inProgressPolylines.Add(touch.Handle, polyline);
            }
            SetNeedsDisplay();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            foreach(UITouch touch in touches.Cast<UITouch>())
            {
                Polyline polyline = inProgressPolylines[touch.Handle];
                inProgressPolylines.Remove(touch.Handle);
                polyline.Path.AddLineToPoint(touch.LocationInView(this));
                completedPolylines.Add(polyline);
            }
            SetNeedsDisplay();
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            foreach (UITouch touch in touches.Cast<UITouch>())
            {
                inProgressPolylines[touch.Handle].Path.AddLineToPoint(touch.LocationInView(this));
            }
            SetNeedsDisplay();
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            inProgressPolylines.Clear();
            SetNeedsDisplay();
        }

        public void clear()
        {
            completedPolylines.Clear();
            SetNeedsDisplay();
        }
    }
}