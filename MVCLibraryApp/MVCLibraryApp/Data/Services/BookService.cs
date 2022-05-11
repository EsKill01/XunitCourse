using MVCLibraryApp.Data.Models;

namespace MVCLibraryApp.Data.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        private bool _dataLoad = false;

        public BookService(AppDbContext context)
        {
            _context = context;

            if (!_dataLoad)
            {
                MockData.MockData.AddTestData(context);
                _dataLoad = true;
            }
        }

        public Book Add(Book book)
        {
            book.Id = Guid.NewGuid();

            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public IEnumerable<Book> GetAll() => _context.Books.ToList();

        public Book GetById(Guid id) => _context.Books.FirstOrDefault(x => x.Id == id);

        public void Remove(Guid id)
        {
           var existing = _context.Books.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                _context.Books.Remove(existing);
                _context.SaveChanges();
            }
        }

        public Book? Update(Book book)
        {
            var exist = GetAll().FirstOrDefault(x => x.Id == book.Id);

            if (exist != null)
            {
                _context.Books.Update(exist);
                _context.SaveChanges();

                return exist;
            }
            else
            {
                return null;
            }

        }
    }
}
