using System.Xml.Linq;

namespace XmlSorter.Services
{
    public class XmlSortService : IXmlSortService
    {
        #region Methods

        public void Sort(string sourceFilePath,
                         bool overwriteSourceFile, 
                         string targetFilePath,
                         bool sortByTagName,
                         bool sortBySpecificAttributes, 
                         IEnumerable<string> sortingAttributes,
                         bool sortAttributes)
        {
            var xe = XElement.Load(sourceFilePath);
            SortElement(xe, sortAttributes, sortBySpecificAttributes, sortingAttributes.ToList());

            string tempTargetFilePath = sourceFilePath;
            if (!overwriteSourceFile)
            {
                string extension = Path.GetExtension(sourceFilePath);
                tempTargetFilePath = Path.ChangeExtension(sourceFilePath, $".sorted{extension}");
            }

            xe.Save(tempTargetFilePath);
        }

        #endregion Methods

        #region Helpers

        private void SortElement(XElement xe, bool sortAttributes, bool sortBySpecificAttributes, List<string> sortingAttributes)
        {
            var nodesToBePreserved = xe.Nodes().Where(p => p.GetType() != typeof(XElement));
            if (sortAttributes)
            {
                xe.ReplaceAttributes(xe.Attributes().OrderBy(x => x.Name.LocalName));
            }

            if (!sortBySpecificAttributes || !sortingAttributes.Any())
            {
                xe.ReplaceNodes((xe.Elements().OrderBy(x => x.Name.LocalName).Union((nodesToBePreserved).OrderBy(p => p.ToString()))).OrderBy(n => n.NodeType.ToString()));
            }
            else
            {
                foreach (var att in sortingAttributes)
                {
                    xe.ReplaceNodes((xe.Elements().OrderBy(x => x.Name.LocalName).ThenBy(x => (string)x.Attribute(att)).Union((nodesToBePreserved).OrderBy(p => p.ToString()))).OrderBy(n => n.NodeType.ToString()));
                }
            }

            foreach (var xc in xe.Elements())
            {
                SortElement(xc, sortAttributes, sortBySpecificAttributes, sortingAttributes);
            }
        }

        #endregion Helpers
    }
}
