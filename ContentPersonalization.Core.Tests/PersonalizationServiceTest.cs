using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Services;
using ContentPersonalization.Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPersonalization.Core.Tests
{
    [TestClass]
    public class PersonalizationServiceTest : BaseTestClass
    {
        private IPersonalizationService _personalizationService;

        private List<IScoredPersona> GetResult()
        {
            IPageService iPageService = new PageService<VisitedPage>(VisitorRepository.Object, PageRepository.Object);
            IInterestAreaService interestAreaService = new InterestAreaService(iPageService);
            _personalizationService =
                new PersonalizationService<ScoredPersona, ScoredInterestArea>(PersonaRepository.Object,
                    InterestAreaRepository.Object, interestAreaService);
            var visitor = VisitorRepository.Object.GetVisitor(Guid.Empty);

            return _personalizationService.GetScoredPersonas(visitor);
        }

        [DataRow(4)]
        [TestMethod]
        public void Returns_X_Scored_Personas(int expectedNumberOfRecords)
        {
            // Arrange & Act
            var result = GetResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == expectedNumberOfRecords);
        }

        [DataRow("benny")]
        [TestMethod]
        public void Visitor_Scores_The_Highest(string personaKey)
        {
            // Arrange & Act
            var result = GetResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().PersonaKey == personaKey);
        }

        [DataRow(29.43d)]
        [TestMethod]
        public void Highest_Score_Is_X(double expectedScoreWith2Digits)
        {
            // Arrange & Act
            var result = GetResult();

            // Assert
            Assert.IsNotNull(result);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            Assert.IsTrue(Math.Round(_personalizationService.GetTotalScore(result.First()), 2) == expectedScoreWith2Digits);
        }
    }
}
