using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Core.XML
{
    public static class XmlParser
    {
        /// <summary>
        /// Deserializes the XML document.
        /// 
        /// Combines XmlReader and LINQ to XML by creating an XElement from an XmlReader
        /// so only a small portion of the document will be stored in memory at any one time.
        /// Useful when parsing large files.
        /// </summary>
        /// <typeparam name="T">Type used for the XML deserialization</typeparam>
        /// <param name="fileUri">XML input File URI</param>
        /// <param name="elementName">XML element to deserialize</param>
        public static IEnumerable<T> ParseElementByElement<T>(string fileUri, string elementName)
        {
            using (XmlReader reader = XmlReader.Create(fileUri))
            {
                reader.MoveToContent();
                while (!reader.EOF)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == elementName)
                    {
                        XElement el = XElement.ReadFrom(reader) as XElement;
                        if (el != null)
                        {
                            yield return el.ToObject<T>();
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }
            }
        }

        private static T ToObject<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }
}
