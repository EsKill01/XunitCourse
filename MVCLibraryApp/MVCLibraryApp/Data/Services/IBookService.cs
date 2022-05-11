using MVCLibraryApp.Data.Models;

namespace MVCLibraryApp.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();

        Book Add(Book book);

        Book GetById(Guid id);

        void Remove(Guid id);

        Book? Update(Book book);
    }
}
