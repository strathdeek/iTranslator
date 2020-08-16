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

namespace iTranslator
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel headerText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView resultsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField searchTermEntry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel searchTermLanguageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton submitButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (headerText != null) {
                headerText.Dispose ();
                headerText = null;
            }

            if (resultsTableView != null) {
                resultsTableView.Dispose ();
                resultsTableView = null;
            }

            if (searchTermEntry != null) {
                searchTermEntry.Dispose ();
                searchTermEntry = null;
            }

            if (searchTermLanguageLabel != null) {
                searchTermLanguageLabel.Dispose ();
                searchTermLanguageLabel = null;
            }

            if (submitButton != null) {
                submitButton.Dispose ();
                submitButton = null;
            }
        }
    }
}