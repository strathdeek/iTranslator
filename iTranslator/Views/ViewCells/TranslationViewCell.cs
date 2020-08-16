using System;
using CoreGraphics;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using iTranslator.Enums;
using iTranslator.Utility.Extensions;
using iTranslator.ViewItems;
using UIKit;

namespace iTranslator.Views.ViewCells
{
    public partial class TranslationViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TranslationViewCell");
        public static readonly UINib Nib;

        private TranslationViewItem translationViewItem;
        public Binding<string, string> textBinding;
        public Binding<Culture, string> languageBinding;
        public Binding<TranslationType, string> translationTypeBinding;
        static TranslationViewCell()
        {
            Nib = UINib.FromName("TranslationViewCell", NSBundle.MainBundle);
        }
        protected TranslationViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            textBinding?.Detach();
            languageBinding?.Detach();
            translationTypeBinding?.Detach();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ContentView.Frame = new CGRect(ContentView.Frame.X, ContentView.Frame.Y, ContentView.Frame.Width, ContentView.Frame.Height - 15);
        }
        public void ConfigureBindings(TranslationViewItem viewItem)
        {

            translationViewItem = viewItem;
            languageBinding = this.SetBinding(() => translationViewItem.Culture, () => languageLabel.Text, BindingMode.TwoWay)
                .ConvertSourceToTarget((arg) =>
               {
                   return arg.Name();
               })
                .ConvertTargetToSource((arg) =>
                {
                    return arg.ToCulture();
                });

            textBinding = this.SetBinding(() => translationViewItem.Word, () => wordLabel.Text);

            translationTypeBinding = this.SetBinding(() => translationViewItem.Type, () => typeLabel.Text)
                .ConvertSourceToTarget((arg) =>
                {
                    return arg.Name();
                })
                .ConvertTargetToSource((arg) =>
                {
                    return arg.ToTranslationType();
                });
            wordLabel.ApplyStyle(Styles.UILabelTranslationHeader);
            languageLabel.ApplyStyle(Styles.UILabelTranslationDetail);
            typeLabel.ApplyStyle(Styles.UILabelTranslationDetail);
            //todo set cell color based on synonym or translation
            if (viewItem.Type == TranslationType.translation)
            {
                this.ApplyStyle(Styles.UIViewCellTranslationViewCell);
            }
            else
            {
                this.ApplyStyle(Styles.UIViewCellSynonymViewCell);

                this.BackgroundColor = BackgroundColor.ColorWithAlpha(viewItem.SynonymScore);
            }
        }
    }
}
