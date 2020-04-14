// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ToDoListe_NavBar
{
    [Register ("TaskListController")]
    partial class TaskListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILongPressGestureRecognizer longPressGesture { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableTasks { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (longPressGesture != null) {
                longPressGesture.Dispose ();
                longPressGesture = null;
            }

            if (tableTasks != null) {
                tableTasks.Dispose ();
                tableTasks = null;
            }
        }
    }
}