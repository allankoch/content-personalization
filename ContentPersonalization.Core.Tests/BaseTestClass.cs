using ContentPersonalization.Core.Models.Interfaces;
using ContentPersonalization.Core.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPersonalization.Core.Tests
{
    public class BaseTestClass
    {
        protected Mock<IInterestAreaRepository> InterestAreaRepository;
        protected Mock<IPageRepository> PageRepository;
        protected Mock<IPersonaRepository> PersonaRepository;
        protected Mock<IVisitorRepository> VisitorRepository;

        [TestInitialize()]
        public void Startup()
        {
            #region Interest Area Repository Setup
            InterestAreaRepository = new Mock<IInterestAreaRepository>();

            InterestAreaRepository.Setup(m => m.GetInterestAreas()).Returns(InterestAreas);

            InterestAreaRepository.Setup(m => m.GetInterestArea(It.Is<string>(d => d == InterestareakeyByggeri))).Returns(
                InterestAreas.First(x => x.InterestAreaKey == InterestareakeyByggeri));

            InterestAreaRepository.Setup(m => m.GetInterestArea(It.Is<string>(d => d == InterestareakeyKunst))).Returns(
                InterestAreas.First(x => x.InterestAreaKey == InterestareakeyKunst));

            InterestAreaRepository.Setup(m => m.GetInterestArea(It.Is<string>(d => d == InterestareakeyFinans))).Returns(
                InterestAreas.First(x => x.InterestAreaKey == InterestareakeyFinans));

            InterestAreaRepository.Setup(m => m.GetInterestArea(It.Is<string>(d => d == InterestareakeyHelbred))).Returns(
                InterestAreas.First(x => x.InterestAreaKey == InterestareakeyHelbred));
            #endregion

            #region Page Repository Setup
            PageRepository = new Mock<IPageRepository>();

            PageRepository.Setup(m => m.GetPages()).Returns(Pages);

            PageRepository.Setup(m => m.GetPage(It.Is<string>(d => d == PageKey1))).Returns(
                Pages.First(x => x.PageKey == PageKey1));

            PageRepository.Setup(m => m.GetPage(It.Is<string>(d => d == PageKey2))).Returns(
                Pages.First(x => x.PageKey == PageKey2));
            #endregion

            #region Persona Repository Setup
            PersonaRepository = new Mock<IPersonaRepository>();

            PersonaRepository.Setup(m => m.GetPersonas()).Returns(Personas);

            PersonaRepository.Setup(m => m.GetPersona(It.Is<string>(d => d == PersonaKeyPeter))).Returns(
                Personas.First(x => x.PersonaKey == PersonaKeyPeter));

            PersonaRepository.Setup(m => m.GetPersona(It.Is<string>(d => d == PersonaKeyAdam))).Returns(
                Personas.First(x => x.PersonaKey == PersonaKeyAdam));

            PersonaRepository.Setup(m => m.GetPersona(It.Is<string>(d => d == PersonaKeyBenny))).Returns(
                Personas.First(x => x.PersonaKey == PersonaKeyBenny));

            PersonaRepository.Setup(m => m.GetPersona(It.Is<string>(d => d == PersonaKeyViggo))).Returns(
                Personas.First(x => x.PersonaKey == PersonaKeyViggo));
            #endregion

            #region Visitor Repository Setup
            VisitorRepository = new Mock<IVisitorRepository>();

            VisitorRepository.Setup(m => m.GetVisitor(It.IsAny<Guid>())).Returns(new Visitor()
            {
                VisitorId = Guid.Empty,
                CreatedDate = DateTime.MaxValue
            });

            VisitorRepository.Setup(m => m.GetVisits(It.IsAny<Guid>())).Returns(Visits);
            #endregion
        }

        #region Keys
        private const string InterestareakeyByggeri = "byggeri";
        private const string InterestareakeyKunst = "kunst";
        private const string InterestareakeyFinans = "finans";
        private const string InterestareakeyHelbred = "helbred";
        private const string PageKey1 = "page-1";
        private const string PageKey2 = "page-2";
        private const string PersonaKeyPeter = "peter";
        private const string PersonaKeyAdam = "adam";
        private const string PersonaKeyBenny = "benny";
        private const string PersonaKeyViggo = "viggo";
        #endregion

        #region Data
        private static readonly List<IInterestArea> InterestAreas = new List<IInterestArea>
        {
            // interest area 1
            new InterestArea() {Name = "Byggeri", InterestAreaKey = InterestareakeyByggeri},

            // interest area 2
            new InterestArea() {Name = "Kunst", InterestAreaKey = InterestareakeyKunst},

            // interest area 3
            new InterestArea() {Name = "Finans", InterestAreaKey = InterestareakeyFinans},

            // interest area 4
            new InterestArea() {Name = "helbred", InterestAreaKey = InterestareakeyHelbred}
        };
        private static readonly List<IPage> Pages = new List<IPage>
        {
            // page 1
            new Page()
            {
                Url = "/page-1",
                PageKey = PageKey1,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea(InterestareakeyByggeri, 0),
                    CreateWeightedInterestArea(InterestareakeyKunst, 20),
                    CreateWeightedInterestArea(InterestareakeyFinans, 80),
                    CreateWeightedInterestArea(InterestareakeyHelbred, 60)
                }
            },

            // page 2
            new Page()
            {
                Url = "/page-2",
                PageKey = PageKey2,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea(InterestareakeyByggeri, 80),
                    CreateWeightedInterestArea(InterestareakeyKunst, 20),
                    CreateWeightedInterestArea(InterestareakeyFinans, 0),
                    CreateWeightedInterestArea(InterestareakeyHelbred, 40)
                }
            }
        };
        private static readonly List<IPersona> Personas = new List<IPersona>
        {
            // persona 1
            new Persona()
            {
                Name = "Peter",
                Description = "",
                PersonaKey = PersonaKeyPeter,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea("byggeri", 100),
                    CreateWeightedInterestArea("kunst", 10),
                    CreateWeightedInterestArea("finans", 60),
                    CreateWeightedInterestArea("helbred", 0)
                }
            },

            // persona 2
            new Persona()
            {
                Name = "Adam",
                Description = "",
                PersonaKey = PersonaKeyAdam,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea("byggeri", 0),
                    CreateWeightedInterestArea("kunst", 20),
                    CreateWeightedInterestArea("finans", 100),
                    CreateWeightedInterestArea("helbred", 30)
                }
            },

            // persona 3
            new Persona()
            {
                Name = "Benny",
                Description = "",
                PersonaKey = PersonaKeyBenny,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea("byggeri", 30),
                    CreateWeightedInterestArea("kunst", 0),
                    CreateWeightedInterestArea("finans", 50),
                    CreateWeightedInterestArea("helbred", 100)
                }
            },

            // persona 4
            new Persona()
            {
                Name = "Viggo",
                Description = "",
                PersonaKey = PersonaKeyViggo,
                WeightedInterestAreas = new List<IWeightedInterestArea>()
                {
                    CreateWeightedInterestArea("byggeri", 40),
                    CreateWeightedInterestArea("kunst", 100),
                    CreateWeightedInterestArea("finans", 0),
                    CreateWeightedInterestArea("helbred", 10)
                }
            }
        };
        private static readonly HashSet<IVisit> Visits = new HashSet<IVisit>()
        {
            CreateVisit(Pages.First(x => x.PageKey == PageKey1), new Visitor() { CreatedDate = DateTime.MaxValue, VisitorId = Guid.Empty}),
            CreateVisit(Pages.First(x => x.PageKey == PageKey1), new Visitor() { CreatedDate = DateTime.MaxValue, VisitorId = Guid.Empty}),
            CreateVisit(Pages.First(x => x.PageKey == PageKey2), new Visitor() { CreatedDate = DateTime.MaxValue, VisitorId = Guid.Empty}),
            CreateVisit(Pages.First(x => x.PageKey == PageKey2), new Visitor() { CreatedDate = DateTime.MaxValue, VisitorId = Guid.Empty}),
            CreateVisit(Pages.First(x => x.PageKey == PageKey2), new Visitor() { CreatedDate = DateTime.MaxValue, VisitorId = Guid.Empty})
        };

        private static IWeightedInterestArea CreateWeightedInterestArea(string interestAreaKey, int weight)
        {
            var interestArea = InterestAreas.First(x => x.InterestAreaKey == interestAreaKey);

            return new WeightedInterestArea()
            {
                InterestAreaKey = interestArea.InterestAreaKey,
                Name = interestArea.Name,
                Weight = weight
            };
        }
        private static IVisit CreateVisit(IPage page, IVisitor visitor)
        {
            return new Visit()
            {
                PageKey = page.PageKey,
                Url = page.Url,
                VisitDate = DateTime.MaxValue,
                VisitId = Guid.NewGuid(),
                VisitorId = visitor.VisitorId
           };
        }
        #endregion
    }

    public class VisitedPage : IVisitedPage
    {
        public string PageKey { get; set; }
        public string Url { get; set; }
        public ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
        public ICollection<IVisit> Visits { get; set; }
    }

    public class ScoredInterestArea : IScoredInterestArea
    {
        public string InterestAreaKey { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
    }

    public class ScoredPersona : IScoredPersona
    {
        public string PersonaKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
        public ICollection<IScoredInterestArea> ScoredInterestAreas { get; set; }
    }

    public class Persona : IPersona
    {
        public string PersonaKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
    }

    public class InterestArea : IInterestArea
    {
        public string InterestAreaKey { get; set; }
        public string Name { get; set; }
    }

    public class WeightedInterestArea : IWeightedInterestArea
    {
        public string InterestAreaKey { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }

    public class Page : IPage
    {
        public string PageKey { get; set; }
        public string Url { get; set; }
        public ICollection<IWeightedInterestArea> WeightedInterestAreas { get; set; }
    }

    public class Visit : IVisit
    {
        public Guid VisitId { get; set; }
        public Guid VisitorId { get; set; }
        public string PageKey { get; set; }
        public string Url { get; set; }
        public DateTime VisitDate { get; set; }
    }

    public class Visitor : IVisitor
    {
        public Guid VisitorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
