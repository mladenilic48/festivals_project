using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using FinalniTest.Interfaces;
using FinalniTest.Controllers;
using FinalniTest.Models;

namespace FinalniTest.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetReturnsProductWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IFestivaliRepository>();
            mockRepository.Setup(x => x.GetById(2)).Returns(new Festival { Id = 2 });

            var controller = new FestivalController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Id);
        }


        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IFestivaliRepository>();
            var controller = new FestivalController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(12);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IFestivaliRepository>();
            mockRepository.Setup(x => x.GetById(12)).Returns(new Festival { Id = 12 });
            var controller = new FestivalController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(12);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }


        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IFestivaliRepository>();
            var controller = new FestivalController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Festival { Id = 12, Naziv = "Gitarijada" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(12, createdResult.RouteValues["id"]);
        }



    }  
}
