using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models.Owner;

namespace eCommerceSite.Models
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Cetagory.Cetagory> Cetagorie { get; set; }
        public DbSet<Joining.Cetagory_Owner_Post> Cetagorie_Owner_Post { set; get; }

        public DbSet<Owner.Owner> Owner { set; get; }

        public DbSet<Post.Post> Post { set; get; }

        public DbSet<Post.PhotoGellary> PhotoGellaries { set; get; }

      
    }
}
