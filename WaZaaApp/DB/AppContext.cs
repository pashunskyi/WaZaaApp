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
            //optionsBuilder.UseSqlServer("Data Source=192.168.0.178;Initial Catalog=WaZaaAppDB;User ID=sa;Password=Qwerty1-");
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WaZaaAppDB2;Trusted_Connection=True;");
        }
        public  DbSet<Chat> Chats { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<UsersChat> UsersChats { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UsersChat>(userChat =>
            {
                userChat.HasKey(ur => new { ur.UserId, ur.ChatId });

                userChat.HasOne(ur => ur.Chat)
                    .WithMany(r => r.UsersChats)
                    .HasForeignKey(ur => ur.ChatId)
                    .IsRequired();

                userChat.HasOne(ur => ur.User)
                    .WithMany(r => r.UsersChats)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

    }
}
