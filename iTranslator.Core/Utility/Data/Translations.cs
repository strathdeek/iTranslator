using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace iTranslator.Utility.Data
{
	[XmlRoot(ElementName = "LINK")]
	public class Link
	{
		[XmlAttribute(AttributeName = "word")]
		public string Word { get; set; }
		[XmlAttribute(AttributeName = "culture")]
		public string Culture { get; set; }
	}

	[XmlRoot(ElementName = "RECORD")]
	public class Record
	{
		[XmlElement(ElementName = "LINK")]
		public List<Link> Links { get; set; }
		[XmlAttribute(AttributeName = "word")]
		public string Word { get; set; }
		[XmlAttribute(AttributeName = "culture")]
		public string Culture { get; set; }
	}

	[XmlRoot(ElementName = "TRANSLATIONS")]
	public class Translations
	{
		[XmlElement(ElementName = "RECORD")]
		public List<Record> Records { get; set; }
	}
}
