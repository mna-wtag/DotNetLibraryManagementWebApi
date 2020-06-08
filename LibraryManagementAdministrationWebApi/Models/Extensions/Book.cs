using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLibraryManagementWebApi.Models
{
    public partial class Book
    {
        [NotMapped]
        public int AuthorId { get; set; }

        [NotMapped]
        public int PublisherId { get; set; }
    }

    public class BookMetaData
    {
        public int BookId { get; set; }

        [Required]
        public string Tittle { get; set; }

        [Required]
        public string BookType { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public int? AuthId { get; set; }

        [Required]
        public int? PubId { get; set; }

        [Required]
        public string BookStatus { get; set; }
    }
}
