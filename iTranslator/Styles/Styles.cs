using System;
using CoreGraphics;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Services.Interfaces;
using UIKit;

namespace iTranslator
{
    public static class Styles
    {
        public const string UILabelTranslationHeader = "UILabelTranslationHeader";
        public const string UILabelTranslationDetail = "UILabelTranslationDetail";
        public const string UILabelTranslationPageHeader = "UILabelTranslationPageHeader";
        public const string UILabelSearchTermCultureLabel = "UILabelSearchTermCultureLabel";
        public const string UIViewCellTranslationViewCell = "UIViewCellTranslationViewCell";
        public const string UIViewCellSynonymViewCell = "UIViewCellSynonymViewCell";
        public const string UITextFieldResultsFound = "UITextFieldResultsFound";
        public const string UITextFieldNoResultsFound = "UITextFieldNoResultsFound";

        public static void RegisterStyles()
        {
            var styleService = SimpleIoc.Default.GetInstance<IStyleService>();

            styleService.RegisterStyle( new Style()
            {
                Name = UILabelTranslationHeader,
                Styling = (view) =>
                {
                    var label = view as UILabel;

                    label.Font = UIFont.BoldSystemFontOfSize(17);
                }
            });

            styleService.RegisterStyle(new Style()
            {
                Name = UILabelTranslationDetail,
                Styling = (view) =>
                {
                    var label = view as UILabel;

                    label.Font = UIFont.ItalicSystemFontOfSize(14);
                }
            });
            
            styleService.RegisterStyle(new Style()
            {
                Name = UILabelTranslationPageHeader,
                Styling = (view) =>
                {
                    var label = view as UILabel;

                    label.Font = UIFont.BoldSystemFontOfSize(30);
                }
            });

            styleService.RegisterStyle(new Style()
            {
                Name = UILabelSearchTermCultureLabel,
                Styling = (view) =>
                {
                    var label = view as UILabel;

                    label.Font = UIFont.BoldSystemFontOfSize(14);
                    label.TextColor = UIColor.SystemGrayColor;
                }
            });

            styleService.RegisterStyle(new Style()
            {
                Name = UIViewCellTranslationViewCell,
                Styling = (view) =>
                {
                    var viewCell = view as UITableViewCell;
                    viewCell.Layer.CornerRadius = 20f;
                    viewCell.Layer.MasksToBounds = true;
                    viewCell.BackgroundColor = UIColor.FromRGBA(132, 172, 206,200);
                }
            });
            
            styleService.RegisterStyle(new Style()
            {
                Name = UIViewCellSynonymViewCell,
                Styling = (view) =>
                {
                    var viewCell = view as UITableViewCell;
                    viewCell.Layer.CornerRadius = 20f;
                    viewCell.Layer.MasksToBounds = true;
                    viewCell.BackgroundColor = UIColor.FromRGB(255, 238, 136);
                }
            });

            styleService.RegisterStyle(new Style()
            {
                Name = UITextFieldResultsFound,
                Styling = (view) =>
                {
                    var entry = view as UITextField;
                    entry.BackgroundColor = UIColor.SystemGreenColor.ColorWithAlpha(.5f);
                }
            });
            
            styleService.RegisterStyle(new Style()
            {
                Name = UITextFieldNoResultsFound,
                Styling = (view) =>
                {
                    var entry = view as UITextField;
                    entry.BackgroundColor = UIColor.SystemRedColor.ColorWithAlpha(.5f);
                }
            });

        }
    }
}
