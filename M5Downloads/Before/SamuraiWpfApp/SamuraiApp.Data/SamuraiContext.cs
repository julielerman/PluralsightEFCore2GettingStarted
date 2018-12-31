using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; //ref needed for 2.2 logging implementation
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console; //package is needed but this reference is no longer needed with 2.2
using SamuraiApp.Domain;
using System.Configuration; //this is for configuration manager which you'll add in during the exercise

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        //note the newer way to set up logging below in the constructor
        //this more readable code arrived in .net core 2.2. It will
        //be simplified further in 3.0.

        //public static readonly LoggerFactory MyConsoleLoggerFactory
        //    = new LoggerFactory(new[] {
        //      new ConsoleLoggerProvider((category, level)
        //        => category == DbLoggerCategory.Database.Command.Name
        //       && level == LogLevel.Information, true) });


        private ILoggerFactory MyConsoleLoggerFactory; //<--this is part of the new 2.2 implementation

        public SamuraiContext()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                        .AddConsole()
                        .AddFilter
                        (DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information));

            MyConsoleLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(
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
