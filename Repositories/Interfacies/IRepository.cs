using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Repositories.Interfacies
{
    public interface IRepository
    {
        public IEnumerable<Announcement> GetAll();
        public IEnumerable<Announcement> Get(string titleAd);
        public void Create(Announcement model);
        public void CreateLinks(List<LinkPhoto> linkPhotos);
    }
}
