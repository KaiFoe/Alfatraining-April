using AVFoundation;
using Foundation;
using System;
using UIKit;

namespace VideoPlayer
{
    public partial class ViewController : UIViewController
    {

        AVPlayer avPlayer;
        AVPlayerLayer avPlayerLayer;
        AVAsset avAsset;
        AVPlayerItem avPlayerItem;



        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            btnPlay.TouchUpInside += BtnPlay_TouchUpInside;
        }

        private void BtnPlay_TouchUpInside(object sender, EventArgs e)
        {
            avAsset = AVAsset.FromUrl(NSUrl.FromFilename("video.mp4"));
            avPlayerItem = new AVPlayerItem(avAsset);
            avPlayer = new AVPlayer(avPlayerItem);
            avPlayerLayer = AVPlayerLayer.FromPlayer(avPlayer);
            avPlayerLayer.Frame = View.Frame;
            View.Layer.AddSublayer(avPlayerLayer);
            avPlayer.Play();
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}