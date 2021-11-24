using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Services.Interfaces;
using System;
using System.Linq;

namespace ContentPersonalization.Core.Services
{
    public class InterestAreaService : IInterestAreaService
    {
        private readonly IPageService _pageService;

        public InterestAreaService(IPageService pageService)
        {
            _pageService = pageService;
        }

        private double GetScoreForPersona(IPersona persona, IInterestArea interestArea)
        {
            if (persona == null)
                throw new Exception("persona is null");

            // calculate sum of scores
            var sumOfWeights = persona.WeightedInterestAreas.Sum(x => x.Weight);

            // find weighted object
            var weightedInterestArea = persona.WeightedInterestAreas.FirstOrDefault(x =>
                x.InterestAreaKey.Equals(interestArea.InterestAreaKey, StringComparison.InvariantCultureIgnoreCase));

            if (weightedInterestArea == null)
                return 0;

            // convert the weighted object to scored object
            return Convert.ToDouble(weightedInterestArea.Weight) / Convert.ToDouble(sumOfWeights) * 100d;
        }

        public double GetInterestAreaScore(IVisitor visitor, IPersona persona, IInterestArea interestArea)
        {
            if (visitor == null)
                throw new Exception("visitor is null");
            if (persona == null)
                throw new Exception("persona is null");
            if (interestArea == null)
                throw new Exception("interestArea is null");

            // given a certain persona
            // personaScore = percentage of interest area compared to sum of all interest areas
            var personaScore = GetScoreForPersona(persona, interestArea);

            // given a certain visitor
            // c = sum score of all visits to a page on a given interest area for a given persona
            // visitScore = percentage of total sum(c)
            var visitedPages = _pageService.GetVisitedPages(visitor);
            var sumOfPages = 0;
            var sumOfInterestArea = 0;
            foreach (var visitedPage in visitedPages)
            {
                if (!_pageService.HasWeightedInterestAreas(visitedPage))
                    continue;

                sumOfPages += visitedPage.Visits.Count * visitedPage.WeightedInterestAreas.Sum(x => x.Weight);

                var interestAreaOnPage = visitedPage.WeightedInterestAreas.FirstOrDefault(x =>
                    x.InterestAreaKey.Equals(interestArea.InterestAreaKey,
                        StringComparison.InvariantCultureIgnoreCase));

                if (interestAreaOnPage == null)
                    continue;

                sumOfInterestArea += visitedPage.Visits.Count * interestAreaOnPage.Weight;
            }

            // visitsScore is the percentage of the (individual interest area * number of page visits)
            // compared to (sum of all interest areas * number of page visits)
            var visitsScore = Convert.ToDouble(sumOfInterestArea) / Convert.ToDouble(sumOfPages) * 100d;

            var result = Convert.ToDouble(personaScore) * (Convert.ToDouble(visitsScore) / 100);

            return result;
        }
    }
}
