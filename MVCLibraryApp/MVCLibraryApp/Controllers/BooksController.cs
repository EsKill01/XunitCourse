using Microsoft.AspNetCore.Mvc;
using MVCLibraryApp.Data.Models;
using MVCLibraryApp.Data.Services;

namespace MVCLibraryApp.Controllers
{
    public class BooksController : Controller
    {
        public readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var result = _service.GetAll();
            return View(result);
        }

        public ActionResult Details(Guid id)
        {
            var result = _service.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.Add(item);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            var result = _service.GetById(id);
            return View(result);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _service.Remove(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Put(Guid id)
        {
            var result = _service.GetById(id);
            return View(result);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Put(Guid id, Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.Update(book);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
