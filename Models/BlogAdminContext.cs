using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcBlogAdmin.Models
{
    public class BlogAdminContext : DbContext
    {
        public BlogAdminContext(DbContextOptions<BlogAdminContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Blog> Blog { get; set; }
    }
}