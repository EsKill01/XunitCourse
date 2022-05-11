using LibraryMVC.Test.Data.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCLibraryApp.Controllers;
using MVCLibraryApp.Data.Models;
using MVCLibraryApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryMVC.Test
{
    public class BooksControllerTest
    {
        [Fact] //Tambien se usa cuando no llevan parametros los metodos
        public void IndexUnitTest()
        {
            //Arange

            var mockRepo = Factory.GetMockIBookService();
                mockRepo.Setup(n => n.GetAll()).Returns(MockData.AddTestData());
            var controller = Factory.GetBooksController(mockRepo.Object);

            //Act

            var result = controller.Index();

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(viewResult?.ViewData.Model);

            Assert.Equal(5, viewResultBooks?.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201")]
        public void DetailsUnitTest(Guid id, Guid noId)
        {
            //arange
            var mockRepo = Factory.GetMockIBookService();
            var mockRepo2 = Factory.GetMockIBookService();

            mockRepo.Setup(n => n.GetById(id)).Returns(MockData.AddTestData()?.FirstOrDefault(c => c.Id == id));
            mockRepo2.Setup(n => n.GetById(noId)).Returns(MockData.AddTestData()?.FirstOrDefault());

            var controller = Factory.GetBooksController(mockRepo.Object);
            var controller2 = Factory.GetBooksController(mockRepo2.Object);


            //act

            var result = controller.Details(id);
            var result2 = controller.Details(noId);

            //assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultBook = Assert.IsAssignableFrom<Book>(viewResult?.ViewData.Model);


            Assert.Equal(id, viewResultBook?.Id);

            Assert.IsType<NotFoundResult>(result2);
        }

        [Fact]
        public void CreateUnitTest()
        {
            //arange

            var mockService = Factory.GetMockIBookService();

            var controller = Factory.GetBooksController(mockService.Object);

            //act
            var result = controller.Create();

            //assert

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateObjectUnitTest()
        {
            //arange
            var book = new Book
            {
                Author = "Esteban Barboza",
                Description = "El libro de la libertad",
                Title = "Oscuridad",
                Id = Guid.NewGuid(),
            };

            var mockService = Factory.GetMockIBookService(); 
            mockService.Setup(n => n.Add(book)).Returns(book);
            var controller = Factory.GetBooksController(mockService.Object);
            var badController = new BooksController(mockService.Object);
            badController.ModelState.AddModelError("Title", "Falta el titulo");

            //act

            var result = controller.Create(book);

            var badResult = badController.Create(book);


            //assert

           var badRequest = Assert.IsType<BadRequestObjectResult>(badResult);
            Assert.IsType<SerializableError>(badRequest.Value);

            var redirestToActionResult = Assert.IsType<RedirectToActionResult>(result);
    
            Assert.Equal("Index", redirestToActionResult.ActionName);
            Assert.Null(redirestToActionResult.ControllerName);


        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c201")]
        public void DeleteView(Guid id, Guid badId)
        {
            //arange

            var mockRepo = Factory.GetMockIBookService();
            var mockRepo2 = Factory.GetMockIBookService();
            mockRepo.Setup(n => n.GetById(id)).Returns(MockData.AddTestData().First(c => c.Id == id));
            mockRepo2.Setup(n => n.GetById(id)).Returns(MockData.AddTestData().FirstOrDefault(c => c.Id == badId));

            var controller = Factory.GetBooksController(mockRepo.Object);
            var controller2 = Factory.GetBooksController(mockRepo.Object);

            //act

            var result = controller.Delete(id);
            var result2 = controller2.Delete(badId);

            //assert

            Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewResult>(result2);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void DeleteObjet(Guid id)
        {
            //arange
            var mockRepo = Factory.GetMockIBookService();
            var controller = Factory.GetBooksController(mockRepo.Object);
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.AddTestData);

            //act

            var result = controller.Delete(id, null);

            //assert

            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Null(actionResult.ControllerName);

        }

        [Fact]
        public void UpdateTest()
        {
            //arange

            var mockRepo = Factory.GetMockIBookService();
            var controller = Factory.GetBooksController(mockRepo.Object);
            var controllerBad = Factory.GetBooksController(mockRepo.Object);
            controllerBad.ModelState.AddModelError("Title", "Error");

            mockRepo.Setup(n => n.GetAll()).Returns(MockData.AddTestData());

            var updateObject = new Book
            {
                Author = "Update",
                Description = "Update",
                Title = "Update",
                Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")
            };

            //act

            var result = controller.Put(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), updateObject);
            var badResult = controllerBad.Put(new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), updateObject);

            //assert

            var badRequest = Assert.IsType<BadRequestObjectResult>(badResult);
            Assert.IsType<SerializableError>(badRequest.Value);

            var redirestToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirestToActionResult.ActionName);
            Assert.Null(redirestToActionResult.ControllerName);

        }
    }
}