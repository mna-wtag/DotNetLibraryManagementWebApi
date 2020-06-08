using System;
using System.Collections.Generic;

namespace DotNetLibraryManagementWebApi.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Book = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
