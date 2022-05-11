using Moq;
using MVCLibraryApp.Controllers;
using MVCLibraryApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMVC.Test
{
    public static class Factory
    {
        public static Mock<IBookService> GetMockIBookService () => new Mock<IBookService> ();

        public static BooksController GetBooksController(IBookService bookService) => new BooksController(bookService);
    }
}
