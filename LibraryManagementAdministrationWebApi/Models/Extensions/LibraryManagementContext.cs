using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetLibraryManagementWebApi.Models.Extensions
{
    public partial class LibraryManagementContext: DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasMany(b => b.Book).WithOne();
   
        }
    }
}
