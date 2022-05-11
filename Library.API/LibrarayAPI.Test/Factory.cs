using Library.API.Controllers;
using Library.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarayAPI.Test
{
    public class Factory
    {
       
        public static BooksController CreateBooksController(IBookService bookService) => new BooksController(bookService);

        public static IBookService CreateBooksServices() => new BookService();

    }
}
