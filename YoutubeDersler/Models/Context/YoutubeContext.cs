using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeDersler.Models.Context
{
    public class YoutubeContext : DbContext
    {
        public YoutubeContext(DbContextOptions<YoutubeContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
