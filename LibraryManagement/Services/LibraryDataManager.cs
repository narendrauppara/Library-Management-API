using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class LibraryDataManager :IDataRepository<Book>
    {

        private readonly LibraryManagementDbContext _dbContext;

        public LibraryDataManager(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Book> GetAll()
        {
            return  _dbContext.Books.ToList();
        }
        public Book Get(int bookId)
        {
            return _dbContext.Books.FirstOrDefault(x => x.BookId == bookId);
        }
        public void Add(Book entity)
        {
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();
            
        }
        public void Update(Book entity)
        {
            Book existingBook = _dbContext.Books?.FirstOrDefault(x => x.BookId == entity.BookId);
            existingBook.BookName = entity?.BookName;
            existingBook.FavouriteTo = entity?.FavouriteTo;
            existingBook.ReadyBy = entity?.ReadyBy;
            existingBook.Reviews = entity?.Reviews;
            
            _dbContext.SaveChanges();
        }
        public void Delete(Book entity)
        {
            _dbContext.Books.Remove(entity);
            _dbContext.SaveChanges();
            
        }
        public void MarkBookAsRead(int id, string userEmail)
        {
            Book record =_dbContext.Books?.FirstOrDefault(x => x.BookId == id);
            record.ReadyBy.Add(userEmail);
            _dbContext.SaveChanges();
        }
        public bool WriteReview(int bookId, string review, string userEmail)
        {

            var reviewedby = _dbContext.Books.Select(x => x.ReviewedBy);

            if (_dbContext.Books.Any(x => x.BookId == bookId && reviewedby.Any(x => x.Contains(userEmail))))
            {

                //string Errormessage = "You already have a review for this book";
                return false;
            }
            else {
               var record= _dbContext.Books?.FirstOrDefault(x => x.BookId == bookId);
                record.Reviews.Add(review);
                record.ReviewedBy.Add(userEmail);
                _dbContext.SaveChanges();
                return true;
            }
            
        }
        public List<string> GetReviews(int bookId)
        {
            var reviews = from p in _dbContext.Books
                          where p.BookId == bookId
                          select p.Reviews;
                        
            return (List<string>)reviews;
        }
    }

}
