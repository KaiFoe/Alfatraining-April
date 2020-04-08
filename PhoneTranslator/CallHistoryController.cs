using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace PhoneTranslator
{
    public partial class CallHistoryController : UITableViewController
    {
        public List<string> PhoneNumbers { get; set; }
        public static NSString callHistoryCellId = new NSString("CallHistoryCell");

        public CallHistoryController (IntPtr handle) : base (handle)
        {
            TableView.RegisterClassForCellReuse(typeof(UITableViewCell), callHistoryCellId);
            TableView.Source = new CallHistoryDataSource(this);
            PhoneNumbers = new List<string>();
        }
    }
}