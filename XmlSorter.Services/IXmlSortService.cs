namespace XmlSorter.Services
{
    public interface IXmlSortService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="overwriteSourceFile"></param>
        /// <param name="targetFilePath"></param>
        /// <param name="sortByTagName"></param>
        /// <param name="sortBySpecificAttributes"></param>
        /// <param name="sortingAttributes"></param>
        /// <param name="sortAttributes"></param>
        /// <returns></returns>
        void Sort(string sourceFilePath,
                  bool overwriteSourceFile,
                  string targetFilePath,
                  bool sortByTagName,
                  bool sortBySpecificAttributes, 
                  IEnumerable<string> sortingAttributes, bool sortAttributes);
    }
}
