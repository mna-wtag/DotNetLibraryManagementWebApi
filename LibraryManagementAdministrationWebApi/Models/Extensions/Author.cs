using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAdministrationWebApi.Models
{
    [ModelMetadataType(typeof(AuthorMetadata))]
    public partial class Author
    {
    }

    public class AuthorMetadata
    {
        public int AuthorId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string HomeAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [FromQuery]
        public virtual ICollection<Book> Book { get; set; }
    }
}
