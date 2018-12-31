using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        ////dotnet Core 2.2 brings a new way to implement logging which is simpler 
        ////than what you'll see in the video. Therefore I'm leaving the NEWER code here so you can
        ////just uncomment it as needed. Note that I have also added some usings above (also commented)
        ////to go with the following code.
        ////In the video you will also use the MyConsoleLoggerFactory defined below in onconfiguring, but that step will be the same. 
        ////You'll still need to follow the step (from the video) to add Microsoft.Extensions.Logging.Console although the 'using' statement will not be needed

        //private ILoggerFactory MyConsoleLoggerFactory; //<--this is part of the new 2.2 implementation

        //public SamuraiContext()
        //{
        //    IServiceCollection serviceCollection = new ServiceCollection();
        //    serviceCollection.AddLogging(builder => builder
        //                .AddConsole()
        //                .AddFilter
        //                (DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information));

        //    MyConsoleLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 "Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData; Trusted_Connection = True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
