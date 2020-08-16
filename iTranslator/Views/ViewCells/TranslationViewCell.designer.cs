// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace iTranslator.Views.ViewCells
{
    [Register ("TranslationViewCell")]
    partial class TranslationViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel languageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel typeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel wordLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (languageLabel != null) {
                languageLabel.Dispose ();
                languageLabel = null;
            }

            if (typeLabel != null) {
                typeLabel.Dispose ();
                typeLabel = null;
            }

            if (wordLabel != null) {
                wordLabel.Dispose ();
                wordLabel = null;
            }
        }
    }
}