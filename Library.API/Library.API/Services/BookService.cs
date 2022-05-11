using Library.API.Models;

namespace Library.API.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>()
            {
                new Book()
                {
                    Title = "Managin Oneselft",
                    Author = "Peter Ducker",
                    Description = "We live in an age of unprecedented opportunity",
                    Id = new Guid("5e8b2c33-2a6c-4c38-9a21-abb33c46ff25")
                },
                new Book()
                {
                    Title = "Managin Oneselft222",
                    Author = "Peter Ducker",
                    Description = "We live in an age of unprecedented opportunity",
                    Id = new Guid("27f6feee-8539-4a4e-9a69-115cc521487e")
                },
                new Book()
                {
                    Title = "Managin Oneselft333",
                    Author = "Peter Ducker",
                    Description = "We live in an age of unprecedented opportunity",
                    Id = new Guid("3ec2951b-0e68-4f3a-8d17-c71f2fc23d36")
                },
                new Book()
                {
                    Title = "Managin Oneselft444",
                    Author = "Peter Ducker",
                    Description = "We live in an age of unprecedented opportunity",
                    Id = new Guid("02766f5d-d001-4750-9c79-5d85adde6120")
                },
                new Book()
                {
                    Title = "Managin Oneselft5555",
                    Author = "Peter Ducker",
                    Description = "We live in an age of unprecedented opportunity",
                    Id = new Guid("ffa62044-9d94-4a0e-9cc4-eae9c05e3409")
                }
            };
        }

        public Book? Add(Book book)
        {
            if (book == null)
            {
                return null;
            }

            book.Id = Guid.NewGuid();

            _books.Add(book);

            return book;
        }

        public IEnumerable<Book> GetAll() => _books;

        public Book? GetById(Guid id)
        {
            var findObject = _books.Find(x => x.Id == id);

            if (findObject == null)
            {
                return null;
            }

            return findObject;
        }


        public void Remove(Guid id)
        {
            var findObject = GetById(id);

            if (findObject != null)
            {
                _books.Remove(findObject);
            }
        }

        public Book Update(Book book)
        {
            var findObject = GetById(book.Id);

            if (findObject != null)
            {
                _books.Remove(findObject);
            }

            _books.Add(book);

            return book;
        }
    }
}
