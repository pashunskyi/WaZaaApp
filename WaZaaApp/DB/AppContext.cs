using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaZaaApp
{
    class AppContext : DbContext
    {
        //виставити дефолтні налаштування
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        //створити бд, якшо вона не створена
        public AppContext()
        {
            Database.EnsureCreated();
        }
        //конект до бд
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WaZaaAppDB;Trusted_Connection=True;");
        }
        public  DbSet<Chat> Chats { get; set; }
        public  DbSet<Message> Messages { get; set; }
        public  DbSet<User> Users { get; set; }

    }
}
