using GalaSoft.MvvmLight.Ioc;
using iTranslator.Services;
using iTranslator.Services.Interfaces;
using UIKit;

namespace iTranslator
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            SimpleIoc.Default.Register<IStyleService, StyleService>();
            SimpleIoc.Default.Register<ITranslationService, TranslationService>();

            Styles.RegisterStyles();
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}