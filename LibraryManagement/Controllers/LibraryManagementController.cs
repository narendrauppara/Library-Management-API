using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web;
using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Model.Authentication;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class LibraryManagementController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IDataRepository<Book> _dbrepo;
        public LibraryManagementController(IDataRepository<Book> repo, ILogger<LibraryManagementController> logger)
        {
            _logger = logger;
            _dbrepo = repo;
        }

        [HttpGet]
        [Route("getlistofbooks")]
        
        public  IActionResult GetListofBooks()
        {
            _logger.LogInformation("Getting list of books");
            List<string> bookList = new List<string>();
            try
            {
               IEnumerable<Book> books = _dbrepo.GetAll();

                return Ok(books);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at GetList of books method." + ex.Message);
                throw new Exception(ex.Message);
            }
            
        }
        [HttpGet]
        [Route("booktoread/{bookId}")]
        public IActionResult BookToRead(int bookId)
        {
            var bookid = HttpUtility.HtmlEncode(bookId); 
            try
            {
                Book book = _dbrepo.Get(int.Parse(bookid));
                if (book == null)
                {
                    return NotFound("The Book record couldn't be found.");
                }
                return Ok(book);
            
            }
            catch(Exception ex)
            {
                _logger.LogError("Custom Error at BookTORead  method." + ex.Message);
                throw new Exception(ex.Message);
            }
            
        }
        [HttpPost]
        [Route("markbookasread/{bookId}/{userEmail}")]
        public IActionResult MarkBookAsRead(int bookId, string userEmail)
        {
            var bookid = HttpUtility.HtmlEncode(bookId);
            try
            {
               _dbrepo.MarkBookAsRead(bookId, userEmail);
                return Ok("Marked sucessfully");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [Route("writereviewtobook/{bookId}/{bookReview}/{userEmail}")]
        public IActionResult WriteReviewToBook(int bookId,string bookReview,string userEmail)
        {
            var bookid = HttpUtility.HtmlEncode(bookId);

            try
            {

                bool result =  _dbrepo.WriteReview(int.Parse(bookid), bookReview, userEmail);
                if (result !=true)
                {
                    return BadRequest("You cannot change the review already written for the book");
                }
                return StatusCode(201, "Review is created successfully");
              
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("getreviews/{bookId}")]
        public IActionResult GetReviews(int bookId)
        {
            var bookid = HttpUtility.HtmlEncode(bookId);
           
            try
            {
                List<string> reviews = _dbrepo.GetReviews(int.Parse(bookid));
                if(reviews.Count == 0)
                {
                    return  NoContent();
                }
                return Ok(reviews);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("addbook/Book")]
        public IActionResult AddBook(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("book details are null.");
                }
                _dbrepo.Add(book);
                return StatusCode(201, "Book is added successfully");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("updatebook/Book")]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("book details are null.");
                }
                _dbrepo.Update(book);
                return StatusCode(202, "Book is updated successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles =UserRoles.Admin)]
        [Route("deletebook/Book")]
        public IActionResult DeleteBook(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("book details are null.");
                }
                _dbrepo.Delete(book);
                return StatusCode(200, "Book is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
