using ContentPersonalization.Core.Models.Interfaces;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Repositories.Interfaces
{
    public interface IPageRepository
    {
        /// <summary>
        /// Returns a single page given a key
        /// </summary>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        IPage GetPage(string pageKey);

        /// <summary>
        /// Returns all pages
        /// </summary>
        /// <returns></returns>
        List<IPage> GetPages();
    }
}
