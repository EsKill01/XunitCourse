using Library.API.Controllers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibrarayAPI.Test
{
    public class BooksControllerTest
    {
        //public BooksControllerTest(IBookService service)
        //{

        //}

        BooksController _controller;
        IBookService _service;
        Book _book;

        public BooksControllerTest()
        {
            _service = Factory.CreateBooksServices();
            _controller = Factory.CreateBooksController(_service);
            _book = new Book()
            {
                Title = "Creado nuevo",
                Author = "Esteban Barboza",
                Description = "Ojala"
            }; 
        }

        [Fact]
        public void GetAllTest()
        {
            //Arrange
            //Act

            var result = _controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Book>>(list?.Value);

            var listBooks = list?.Value as List<Book>;
            Assert.Equal(5, listBooks?.Count);
        }

        [Theory]
        [InlineData("ffa62044-9d94-4a0e-9cc4-eae9c05e3409", "ffa62044-9d94-4a0e-9cc4-eae9c0he1d09")]
        public void GetByIdTest(Guid id, Guid noId)
        {
            //Arange

            //Act


            var result = _controller.Get(id);
            var noResult = _controller.Get(noId);

            //Assert

            Assert.IsType<NotFoundResult>(noResult.Result);
            Assert.IsType<OkObjectResult>(result.Result);

            var objResponse = result.Result as OkObjectResult;

            Assert.NotNull(objResponse);


            Assert.IsType<Book>(objResponse?.Value);

            var obj = objResponse?.Value as Book;


            Assert.Equal(id, obj?.Id);

        }

        

        [Fact]
        public void AddTest()
        {
            //Arange

            var okPost = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Titulo del libro nuevo",
                Author = "Author nuevo",
                Description = "Descripción"
            };

            //Act

            var result = _controller.Post(okPost);

            //Assert


            Assert.IsType<CreatedAtActionResult
                >(result);

            var objResponse = result as CreatedAtActionResult;

            Assert.NotNull(objResponse);

            Assert.IsType<Book>(objResponse?.Value);

            var obj = objResponse?.Value as Book;

            Assert.Equal(okPost.Author, obj?.Author);
            Assert.Equal(okPost.Title, obj?.Title);
            Assert.Equal(okPost.Description, obj?.Description);


            //Arange

            var noOkPost = new Book
            {
                Id = Guid.NewGuid(),
                Author = "Esteban ddsds",
                Description = "dadas"
            };

            //Act

            _controller.ModelState.AddModelError("Title", "Falta el titulo");
            var resultBadRequest = _controller.Post(noOkPost);

            //Assert



            Assert.IsType<BadRequestObjectResult>(resultBadRequest);
            
        }

        [Theory]
        [InlineData("ffa62044-9d94-4a0e-9cc4-eae9c05e3409", "ffa62044-9d94-4a0e-9cc4-eae9c05e3400")]
        public void RemoveTest(Guid id, Guid idBad)
        {
            //Arange


            //Act


            Assert.Equal(5, _service.GetAll().Count());

            var result = _controller.Remove(id);
            var resultBad = _controller.Remove(idBad);


            //Assert

            Assert.IsType<NotFoundResult>(resultBad);
            Assert.IsType<OkResult>(result);

            Assert.Equal(4, _service.GetAll().Count());


        }

        [Fact]
        public void UpdateTest()
        {
            //arange

            var UpdateBook = new Book
            {
                Title = "Update",
                Author = "Update",
                Description = "Update",
                Id = new Guid("5e8b2c33-2a6c-4c38-9a21-abb33c46ff25")
            };



            //act

            var result = _controller.Put(UpdateBook);
     

            //assert

            var createActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createActionResult.Value);
            var bookResponse = Assert.IsType<Book>(createActionResult.Value);
            Assert.NotNull(bookResponse);
            Assert.Equal(UpdateBook.Id, bookResponse.Id);


            //arange

            var BadUpdateBook = new Book
            {
                Title = "Update",
                Author = "Update",
                Description = "Update",
                Id = new Guid("5e8b2c33-2a6c-4c38-9a21-abb33c46f123")
            };

            //act


            _controller.ModelState.AddModelError("Title", "Falta titulo");
            var resultBad = _controller.Put(BadUpdateBook);


            //assert

            Assert.IsType<BadRequestObjectResult>(resultBad);

        }

    }
}