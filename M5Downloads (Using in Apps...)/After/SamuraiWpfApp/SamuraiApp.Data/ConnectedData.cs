using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System.Collections.ObjectModel;
using System.Linq;

namespace SamuraiApp.Data
{
    public class ConnectedData
    {
        private SamuraiContext _context;

        public ConnectedData()
        {
            _context = new SamuraiContext();
            _context.Database.Migrate();
        }

        public Samurai CreateNewSamurai()
        {
            var samurai = new Samurai { Name = "New Samurai" };
            _context.Samurais.Add(samurai);
            return samurai;
        }

        public ObservableCollection<Samurai> SamuraisListInMemory()
        {
            if (_context.Samurais.Local.Count == 0)
            {
                _context.Samurais.ToList();
            }
            return _context.Samurais.Local.ToObservableCollection();
        }

        public Samurai LoadSamuraiGraph(int samuraiId) 
        {
            var samurai = _context.Samurais.Find(samuraiId); //gets from tracker if its there
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
            _context.Entry(samurai).Collection(s => s.Quotes).Load();

            return samurai;
        }

        public bool ChangesNeedToBeSaved()
        {
            return _context.ChangeTracker.Entries()
                .Any(e => e.State == EntityState.Added
                       | e.State == EntityState.Modified
                       | e.State == EntityState.Deleted);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}