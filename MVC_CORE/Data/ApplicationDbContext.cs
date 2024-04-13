using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MVC_CORE.Models;

namespace MVC_CORE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
       
        public DbSet<student_videos> Student_Videos { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<topic> Topics { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<Questions> questions { get; set; }
    }
}
