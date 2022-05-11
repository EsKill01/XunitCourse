using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService bookService)
        {
            _service = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _service.GetAll();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);   
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(book);

            return CreatedAtAction("Get", new {id = item.Id}, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existItem = _service.GetById(id);

            if (existItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Update(book);

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }
    }
}
