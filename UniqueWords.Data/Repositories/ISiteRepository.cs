using System.Collections.Generic;
using UniqueWords.Data.Entities;

namespace UniqueWords.Data.Repositories
{
    public interface ISiteRepository
    {
        public IEnumerable<Site> GetAll();
        
        public void Add(Site site);

        public void Remove(Site site);

        public void Rewrite(Site site);
    }
}