using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAdministrationWebApi.Models.Extensions
{
    [ModelMetadataType(typeof(PublisherMetaData))]
    public partial class Publisher
    {
    }

    public class PublisherMetaData
    {
        public int PublisherId { get; set; }

        [Required]
        public string PuilisherName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
