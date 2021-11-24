using ContentPersonalization.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContentPersonalization.Core.Tests
{
    [TestClass]
    public class InterestAreaServiceTest : BaseTestClass
    {
        [DataRow("peter", "finans", 7.63d)]
        [DataRow("benny", "helbred", 18.02d)]
        [DataRow("adam", "kunst", 1.8d)]
        [DataRow("viggo", "byggeri", 8.65d)]
        [TestMethod]
        public void Persona_Scores_X_On_InterestArea(string personaName, string interestAreaName, double expectedResultWith2Digits)
        {
            // Arrange
            var pageService = new PageService<VisitedPage>(VisitorRepository.Object, PageRepository.Object);
            var interestAreaService = new InterestAreaService(pageService);
            var visitor = VisitorRepository.Object.GetVisitor(Guid.Empty);
            var persona = PersonaRepository.Object.GetPersona(personaName);
            var interestArea = InterestAreaRepository.Object.GetInterestArea(interestAreaName);

            // Act
            var score = interestAreaService.GetInterestAreaScore(visitor, persona, interestArea);

            // Assert
            Assert.IsTrue(score > 0);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            Assert.IsTrue(Math.Round(score, 2) == expectedResultWith2Digits);
        }
    }
}
