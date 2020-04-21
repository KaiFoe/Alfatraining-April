using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Drawing
{
    public partial class ViewController : UIViewController
    {
        class NoCaretField : UITextField
        {
            public NoCaretField() : base(new CGRect())
            {
                BorderStyle = UITextBorderStyle.RoundedRect;
                BackgroundColor = UIColor.Black;
                TextColor = UIColor.White;
            }

            public override CGRect GetCaretRectForPosition(UITextPosition position)
            {
                return new CGRect();
            }
        }

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIView contentView = new UIView()
            {
                BackgroundColor = UIColor.FromRGB(123, 123, 123)
            };

            View = contentView;
            CGRect rect = UIScreen.MainScreen.Bounds;
            rect.Y += 50;
            rect.Height -= 100;

            UIStackView vertStackView = new UIStackView(rect)
            {
                Axis = UILayoutConstraintAxis.Vertical
            };

            UIStackView horizStackView = new UIStackView(rect)
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Center,
                Distribution = UIStackViewDistribution.EqualSpacing
            };

            CanvasView canvasView = new CanvasView();
            

            contentView.Add(vertStackView);
            vertStackView.AddArrangedSubview(horizStackView);
            vertStackView.AddArrangedSubview(canvasView);

            UILabel placeHolder1 = new UILabel(new CGRect(0, 0, 10, 10));
            UILabel placeHolder2 = new UILabel(new CGRect(0, 0, 10, 10));

            horizStackView.AddArrangedSubview(placeHolder1);

            PickerDataModel<UIColor> colorModel = new PickerDataModel<UIColor>
            {
                Items =
                {
                    new NamedValue<UIColor>("Rot", UIColor.Red),
                    new NamedValue<UIColor>("Grün", UIColor.Green),
                    new NamedValue<UIColor>("Gelb", UIColor.Yellow),
                    new NamedValue<UIColor>("Blau", UIColor.Blue)
                }
            };

            UIPickerView colorPicker = new UIPickerView
            {
                Model = colorModel
            };

            PickerDataModel<float> thicknessModel = new PickerDataModel<float>
            {
                Items =
                {
                    new NamedValue<float>("Dünn", 2),
                    new NamedValue<float>("Medium", 10),
                    new NamedValue<float>("Normal", 20),
                    new NamedValue<float>("Dick", 40)
                }
            };

            UIPickerView thicknessPicker = new UIPickerView
            {
                Model = thicknessModel
            };

            var toolbar = new UIToolbar(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 45))
            {
                BarStyle = UIBarStyle.BlackTranslucent,
                Translucent = true
            };

            //UIFont font = UIFont.SystemFontOfSize(24);
            UIFont font = UIFont.FromName("Chalkduster", 24);

            UITextField txtColor = new UITextField
            {
                Text = "Rot",
                InputView = colorPicker,
                InputAccessoryView = toolbar,
                Font = font
            };

            UITextField txtThickness = new UITextField
            {
                Text = "Dünn",
                InputView = thicknessPicker,
                InputAccessoryView = toolbar,
                Font = font
                
            };
            

            horizStackView.AddArrangedSubview(txtColor);
            horizStackView.AddArrangedSubview(txtThickness);

            UIButton btnClear = new UIButton(UIButtonType.RoundedRect)
            {
                Font = font
            };
            btnClear.Layer.BorderColor = UIColor.White.CGColor;
            btnClear.Layer.BorderWidth = 1;
            btnClear.Layer.CornerRadius = 10;
            btnClear.SetTitle("Clear", UIControlState.Normal);
            btnClear.SetTitleColor(UIColor.Black, UIControlState.Normal);

            horizStackView.AddArrangedSubview(btnClear);

            horizStackView.AddArrangedSubview(placeHolder2);

            btnClear.TouchUpInside += (sender, args) =>
            {
                canvasView.clear();
            };

            thicknessModel.valueChanged += (sender, args) =>
            {
                txtThickness.Text = thicknessModel.SelectedItem.Name;
                canvasView.StrokeWidth = thicknessModel.SelectedItem.Value;
            };

            colorModel.valueChanged += (semder, args) =>
            {
                txtColor.Text = colorModel.SelectedItem.Name;
                //canvasView.StrokeColor = UIColor.FromRGB(123, 123, 123).CGColor;
                canvasView.StrokeColor = colorModel.SelectedItem.Value.CGColor;
            };

            var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                txtColor.ResignFirstResponder();
                txtThickness.ResignFirstResponder();
            });

            toolbar.SetItems(new[] { spacer, doneButton }, true);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}