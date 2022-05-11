using Library.API.Models;

namespace Library.API.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        Book GetById(Guid id);

        Book Add(Book book);

        void Remove(Guid id);

        Book Update(Book book);
    }
}
