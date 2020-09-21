using LibraryManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LibraryManagement.Repository
{
   
        public interface IDataRepository<TEntity>
        {
            IEnumerable<TEntity> GetAll();
            TEntity Get(int id);
            void Add(TEntity entity);
            void Update(TEntity entity);
            void Delete(TEntity entity);
            void MarkBookAsRead(int id, string name);
            bool WriteReview(int id, string review, string userEmail);
            List<string> GetReviews(int bookId);
        }

}
