using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLOCALDB ; database = CoreBlogDB ; integrated security = true ;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(x => x.HomeTeam)
                .WithMany(y => y.HomeMatches)
                .HasForeignKey(z => z.HomeTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.GuestTeam)
                .WithMany(y => y.AwayMatches)
                .HasForeignKey(z => z.GuestTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // HomeMatches => WriterSender
            // AwayMatches => WriterReceiver

            // HomeTeam => SenderUser
            // GuestTeam => ReceiverUser

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.SenderUser)
                .WithMany(y => y.WriterSender)
                .HasForeignKey(z => z.SenderID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(y => y.WriterReceiver)
                .HasForeignKey(z => z.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        /* Bu kod, Entity Framework Core (EF Core) kullanılarak bir ilişki modelini kurmak için Fluent API'yi kullanır. Bu özellik, iki farklı ilişkiyi belirler: "HomeTeam" ve "GuestTeam" arasındaki birçoklu ilişki.

        OnModelCreating yöntemi, veritabanı modelinin tanımlandığı yerdir.Bu yöntem, bir ModelBuilder nesnesi tarafından kullanılarak veritabanı modeli için kuralların ayarlanmasına izin verir.

        Bu kod örneğinde, Match tablosu birincil tablodur ve HomeTeam ve GuestTeam tablolarıyla ilişkilidir.Ayrıca, Match tablosunun HomeTeamID ve GuestTeamID alanları, sırasıyla, HomeTeam ve GuestTeam tablolarının anahtar alanları ile eşleşir.


        Bu kod, Match tablosunun HomeTeam alanı için bir ilişki belirtir ve HasOne, WithMany ve HasForeignKey metodları aracılığıyla bu ilişkiyi tanımlar. Aynı işlem GuestTeam alanı için de yapılır.
        

        .OnDelete(DeleteBehavior.ClientSetNull) ifadesi her iki durumda da kullanılır ve "ClientSetNull" silme davranışını belirtir.Bu, bir takım silindiğinde, o takımla ilgili tüm maç kayıtlarının takım alanlarının null olarak ayarlanmasını sağlar. 
        */

        public DbSet<About> Abouts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogRayting> BlogRaytings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message2> Message2s { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Writer> Writers { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
