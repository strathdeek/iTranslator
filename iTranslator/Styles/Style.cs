using System;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Services.Interfaces;
using UIKit;

namespace iTranslator
{
    public class Style
    {
        public string Name { get; set; }

        public Action<UIView> Styling { get; set; }
    }

    public static class StyleExtensionMethods
    {
        public static void ApplyStyle(this UIView view, string styleName)
        {
            var styleService = SimpleIoc.Default.GetInstance<IStyleService>();

            var style = styleService.FetchStyle(styleName);
            if (style!=null && view!=null)
            {
                style.Styling(view);
            }
        }
    }
}
