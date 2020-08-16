using System;
using System.Collections.Generic;
using System.Linq;
using iTranslator.Services.Interfaces;

namespace iTranslator.Services
{
    public class StyleService : IStyleService
    {
        List<Style> styles;

        public StyleService()
        {
            styles = new List<Style>();
        }

        public Style FetchStyle(string styleName)
        {
            var style = styles.FirstOrDefault(x => x.Name.Equals(styleName));

            return style;
        }

        public void RegisterStyle(Style style)
        {
            if (!styles.Any(x => x.Name.Equals(style.Name)))
            {
                styles.Add(style);
            }
        }
    }
}
