using CoreGraphics;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using iTranslator.ViewItems;
using iTranslator.Viewmodels;
using iTranslator.Views;
using iTranslator.Views.ViewCells;
using System;
using System.Threading.Tasks;
using UIKit;

namespace iTranslator
{
    public partial class ViewController : UIViewController
    {

        private MainViewModel Vm;

        private Binding<string, string> headerTextBinding;
        private Binding<string, string> searchTermBinding;
        private Binding<string, string> languageLabelBinding;

        public ViewController(IntPtr handle) : base(handle)
        {
            Vm = new MainViewModel();
        }

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            InitializeTable();
            SetBindings();
            ApplyStyles();
        }

        private void InitializeTable()
        {
            var tableViewController = Vm.Translations.GetController(CreateTranslationCell, BindTranslationCell);
            tableViewController.TableView = resultsTableView;
            resultsTableView.RegisterClassForCellReuse(typeof(TranslationViewCell), TranslationViewCell.Key);
        }

        private void SetBindings()
        {
            headerTextBinding = this.SetBinding(() => Vm.HeaderText, () => headerText.Text);
            searchTermBinding = this.SetBinding(() => Vm.SearchTerm, () => searchTermEntry.Text, BindingMode.TwoWay);
            languageLabelBinding = this.SetBinding(() => Vm.SearchTermCulture, () => searchTermLanguageLabel.Text);
            submitButton.SetCommand(Vm.SubmitCommand);
            submitButton.SetTitle(Vm.SubmitButtonText, UIControlState.Normal);
            submitButton.TouchDown += (s,e) => AnimateTextField();
            Vm.UpdateUI += Vm_UpdateUI;
        }

        private void Vm_UpdateUI(object sender, bool resultsFound)
        {
            if (resultsFound)
            {
                searchTermEntry.ApplyStyle(Styles.UITextFieldResultsFound);
            }
            else
            {
                searchTermEntry.ApplyStyle(Styles.UITextFieldNoResultsFound);
            }
        }

        private void AnimateTextField()
        {
            nfloat scale = 0.95f;
            double timing = 0.1;

            UIView.Animate(timing,0.0,UIViewAnimationOptions.CurveEaseInOut ,() =>
            {
                searchTermEntry.Transform = CGAffineTransform.MakeScale(scale, scale);
            },
            ()=>
            {
                UIView.Animate(timing, 0.0, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    searchTermEntry.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
                }, () =>{});
            });
        }

        private void ApplyStyles()
        {
            headerText.ApplyStyle(Styles.UILabelTranslationPageHeader);
            searchTermLanguageLabel.ApplyStyle(Styles.UILabelSearchTermCultureLabel);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void BindTranslationCell(UITableViewCell cell,
                          TranslationViewItem translationViewItem,
                          NSIndexPath path)
        {
            (cell as TranslationViewCell).ConfigureBindings(translationViewItem);
        }

        private UITableViewCell CreateTranslationCell(NSString cellIdentifier)
        {
            var cell = resultsTableView.DequeueReusableCell("cell_id");
            
            return cell;
        }
    }
}