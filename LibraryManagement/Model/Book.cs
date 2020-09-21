using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Model
{
    public class Book
    {
        [Column("BookId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BookId { get; set; }
        [Column("BookName")]
        [Required]
        [StringLength(50)]
        public string BookName { get; set; }

        [Column("ReadyBy")]
        [StringLength(50)]
        public List<string> ReadyBy { get; set; }
        
        [Column("FavouriteTo")]
        [StringLength(50)]
        public List<string> FavouriteTo { get; set; }
        
        [Column("Reviews")]
        [StringLength(500)]
        public List<string> Reviews { get; set; }
        
        [Column("ReviewBy")]
        [StringLength(50)]
        public List<string> ReviewedBy { get; set; }

    }
}
