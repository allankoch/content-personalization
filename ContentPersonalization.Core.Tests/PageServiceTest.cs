using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPersonalization.Core.Tests
{
    [TestClass]
    public class PageServiceTest : BaseTestClass
    {
        private List<IVisitedPage> GetVisitedPages()
        {
            var pageService = new PageService<VisitedPage>(VisitorRepository.Object, PageRepository.Object);
            var visitor = VisitorRepository.Object.GetVisitor(Guid.Empty);

            return pageService.GetVisitedPages(visitor);
        }

        [DataRow(5)]
        [TestMethod]
        public void Visitor_Visited_X_Times_In_Total(int expectedNumberOfVisits)
        {
            // Arrange & Act
            var visitedPages = GetVisitedPages();

            // Assert
            Assert.IsNotNull(visitedPages);
            Assert.IsTrue(visitedPages.Count == expectedNumberOfVisits);
        }

        [DataRow(2, 3)]
        [TestMethod]
        public void Visitor_Visited_Page1_X_Times_And_Page2_X_Times(int page1NumberOfVisits, int page2NumberOfVisits)
        {
            // Arrange & Act
            var visitedPages = GetVisitedPages();

            // Assert
            Assert.IsNotNull(visitedPages);
            Assert.IsTrue(visitedPages.Count(x => x.PageKey == "page-1") == page1NumberOfVisits,
                $"Page 1 was not visited {page1NumberOfVisits} times");
            Assert.IsTrue(visitedPages.Count(x => x.PageKey == "page-2") == page2NumberOfVisits,
                $"Page 2 was not visited {page2NumberOfVisits} times");
        }
    }
}
