using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Repositories.Interfaces;
using ContentPersonalization.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPersonalization.Core.Services
{
    public class PageService<T> : IPageService 
        where T : IVisitedPage
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IPageRepository _pageRepository;

        public PageService(IVisitorRepository visitorRepository, IPageRepository pageRepository)
        {
            _visitorRepository = visitorRepository;
            _pageRepository = pageRepository;
        }

        public List<IVisitedPage> GetVisitedPages(IVisitor visitor)
        {
            var visits = _visitorRepository.GetVisits(visitor.VisitorId);
            var allPages = _pageRepository.GetPages();

            var list = new List<IVisitedPage>();

            // split visits by pages, for faster querying when many visits
            List<(IPage Page, ICollection<IVisit> Visits)> pageVisits = new List<(IPage, ICollection<IVisit>)>();

            foreach (var page in allPages)
            {
                var pageVisit = (page, visits);
                pageVisits.Add(pageVisit);
            }

            foreach (var visit in visits)
            {
                var page = allPages.FirstOrDefault(x =>
                    x.PageKey.Equals(visit.PageKey, StringComparison.InvariantCultureIgnoreCase));

                if (page == null)
                    continue;

                var visitedPage = (IVisitedPage) Activator.CreateInstance(typeof(T));
                visitedPage.PageKey = page.PageKey;
                visitedPage.Url = page.Url;
                visitedPage.WeightedInterestAreas = page.WeightedInterestAreas;
                visitedPage.Visits = pageVisits.FirstOrDefault(x =>
                    x.Page.PageKey.Equals(page.PageKey, StringComparison.InvariantCultureIgnoreCase)).Visits;
                        
                list.Add(visitedPage);
            }

            return list;
        }

        public bool HasWeightedInterestAreas(IVisitedPage visitedPage)
        {
            return visitedPage != null && visitedPage.WeightedInterestAreas.Any();
        }
    }
}
