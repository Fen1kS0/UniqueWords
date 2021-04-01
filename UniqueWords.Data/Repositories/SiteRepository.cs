using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniqueWords.Data.Entities;

namespace UniqueWords.Data.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly AppDbContext _context;

        public SiteRepository()
        {
            _context = new AppDbContext();
        }

        public IEnumerable<Site> GetAll()
        {
            return _context.Sites.ToList();
        }

        public void Add(Site site)
        {
            _context.Add(site);
            _context.SaveChanges();
        }
        
        public void Remove(Site site)
        {
            _context.Remove(site);
            _context.SaveChanges();
        }

        public void Rewrite(Site site)
        {
            _context.Entry(site).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}