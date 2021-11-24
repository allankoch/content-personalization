using ContentPersonalization.Core.Models.Interfaces;
using System.Collections.Generic;

namespace ContentPersonalization.Core.Services.Interfaces
{
    public interface IPageService
    {
        /// <summary>
        /// Returns a list of visited pages by a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        List<IVisitedPage> GetVisitedPages(IVisitor visitor);

        /// <summary>
        /// Returns true if the visited page has weighted interest areas
        /// </summary>
        /// <param name="visitedPage"></param>
        /// <returns></returns>
        bool HasWeightedInterestAreas(IVisitedPage visitedPage);
    }
}
