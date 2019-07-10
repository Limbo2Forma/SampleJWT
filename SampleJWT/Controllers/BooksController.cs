using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using SampleJWT.Models;
using SampleJWT.Filters;

namespace SampleJWT.Controllers {

    [RoutePrefix("api/books")]
    [JwtAuthentication]
    public class BooksController : ApiController {
        readonly Author[] Authors = new Author[] {
                new Author() { AuthorId = 1, Name = "Ralls, Kim" },
                new Author() { AuthorId = 2, Name = "Corets, Eva" },
                new Author() { AuthorId = 3, Name = "Randall, Cynthia" },
                new Author() { AuthorId = 4, Name = "Thurman, Paula" }
        };
        Book[] Books = new Book[] {
                new Book() {
                    BookId = 1,  Title = "Midnight Rain", Genre = "Fantasy",
                    PublishDate = new DateTime(2000, 12, 16), AuthorId = 1, Description =
                    "A former architect battles an evil sorceress.", Price = 14.95M
                },

                new Book() {
                    BookId = 2, Title = "Maeve Ascendant", Genre = "Fantasy",
                    PublishDate = new DateTime(2000, 11, 17), AuthorId = 2, Description =
                    "After the collapse of a nanotechnology society, the young" +
                    "survivors lay the foundation for a new society.", Price = 12.95M
                },

                new Book() {
                    BookId = 3, Title = "The Sundered Grail", Genre = "Fantasy",
                    PublishDate = new DateTime(2001, 09, 10), AuthorId = 2, Description =
                    "The two daughters of Maeve battle for control of England.", Price = 12.95M
                },

                new Book() {
                    BookId = 4, Title = "Lover Birds", Genre = "Romance",
                    PublishDate = new DateTime(2000, 09, 02), AuthorId = 3, Description =
                    "When Carla meets Paul at an ornithology conference, tempers fly.", Price = 7.99M
                },

                new Book() {
                    BookId = 5, Title = "Splish Splash", Genre = "Romance",
                    PublishDate = new DateTime(2000, 11, 02), AuthorId = 4, Description =
                    "A deep sea diver finds true love 20,000 leagues beneath the sea.", Price = 6.99M
                },
        };

        // GET api/Books
        [Route("")]
        public IEnumerable<Book> GetBooks() {
            return Books;
        }

        // GET: api/Books/5
        [Route("{id:int}")]
        public IHttpActionResult GetBook(int id) {
            var book = Books.FirstOrDefault((b) => b.BookId == id);
            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }

        // GET: api/Books/5/details
        [Route("{id:int}/details")]
        public IHttpActionResult GetBookDetail(int id) {
            var book = Books.FirstOrDefault((b) => b.BookId == id);

            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }

        // GET: api/Books/Fantasy
        [Route("{genre}")]
        public IHttpActionResult GetFirstBookByGenre(string genre) {
            var book = Books.FirstOrDefault((b) => b.Genre == genre);
            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }

        // GET: api/authors/1/books
        [Route("~/api/authors/{authorId}/books")]
        public IHttpActionResult GetBooksByAuthor(int authorId) {
            var book = Books.FirstOrDefault((b) => b.AuthorId == authorId);
            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }

        // GET: api/Books/date/1999-09-09
        // GET: api/Books/date/1999/09/09
        [Route("date/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("date/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        //*: {pubdate} should match the rest of the URI
        public IHttpActionResult GetBooks(DateTime pubdate) {
            var book = Books.FirstOrDefault(b => DbFunctions.TruncateTime(b.PublishDate)
                    == DbFunctions.TruncateTime(pubdate));

            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }
    }
}