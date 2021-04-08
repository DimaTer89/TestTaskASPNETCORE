using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Models;
using TestTask.Repositories.Interfacies;

namespace TestTask.Repositories
{
    public class EntityRepository : IRepository
    {
        private ApplicationContext appContext;
        public EntityRepository(ApplicationContext appContext)
        {
            this.appContext = appContext;
            if (!appContext.Announcements.Any())
            {
                InitDatabase();
            }
        }

        public void Create(Announcement model)
        {
            appContext.Announcements.Add(model);
            appContext.SaveChanges();
        }

        public void CreateLinks(List<LinkPhoto> linkPhotos)
        {
            appContext.Links.AddRange(linkPhotos);
        }

        public IEnumerable<Announcement> Get(string titleAd)
        {
            return appContext.Announcements.Where(currentModel => currentModel.Name.Contains(titleAd));
        }

        public IEnumerable<Announcement> GetAll()
        {
            return appContext.Announcements.Include(link => link.LinkPhoto);
        }

        private void InitDatabase()
        {
            LinkPhoto photoPathOne = new LinkPhoto { Path = @"c:\Users\User\photo1.jpg" };
            LinkPhoto photoPathTwo = new LinkPhoto { Path = @"c:\Users\User\photo2.jpg" };
            LinkPhoto photoPathThree = new LinkPhoto { Path = @"c:\Users\User\photo3.jpg" };
            LinkPhoto photoPathFour = new LinkPhoto { Path = @"c:\Users\User\photo4.jpg" };
            LinkPhoto photoPathFive = new LinkPhoto { Path = @"c:\Users\User\photo5.jpg" };
            appContext.Links.AddRange(photoPathOne, photoPathTwo, photoPathThree, photoPathFour, photoPathFive);
            List<Announcement> testData = new List<Announcement>()
            {
                new Announcement
                {
                    Name = "Ботинки",
                    LinkPhoto = new List<LinkPhoto> { photoPathOne, photoPathTwo },
                    Description = "Ботинки, детская обувь, 30 размер, для мальчиков",
                    Price = 25m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 2",
                    LinkPhoto = new List<LinkPhoto> { photoPathThree, photoPathFour },
                    Description = "Description 2",
                    Price = 15.5m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 3",
                    LinkPhoto = new List<LinkPhoto> { photoPathFive },
                    Description = "Description 3",
                    Price = 125m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Ботинки",
                    LinkPhoto = new List<LinkPhoto> { photoPathOne, photoPathTwo },
                    Description = "Ботинки, детская обувь, 30 размер, для мальчиков",
                    Price = 25m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 2",
                    LinkPhoto = new List<LinkPhoto> { photoPathThree, photoPathFour },
                    Description = "Description 2",
                    Price = 15.5m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 3",
                    LinkPhoto = new List<LinkPhoto> { photoPathFive },
                    Description = "Description 3",
                    Price = 125m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Ботинки",
                    LinkPhoto = new List<LinkPhoto> { photoPathOne, photoPathTwo },
                    Description = "Ботинки, детская обувь, 30 размер, для мальчиков",
                    Price = 25m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 2",
                    LinkPhoto = new List<LinkPhoto> { photoPathThree, photoPathFour },
                    Description = "Description 2",
                    Price = 15.5m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 3",
                    LinkPhoto = new List<LinkPhoto> { photoPathFive },
                    Description = "Description 3",
                    Price = 125m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Ботинки",
                    LinkPhoto = new List<LinkPhoto> { photoPathOne, photoPathTwo },
                    Description = "Ботинки, детская обувь, 30 размер, для мальчиков",
                    Price = 25m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 2",
                    LinkPhoto = new List<LinkPhoto> { photoPathThree, photoPathFour },
                    Description = "Description 2",
                    Price = 15.5m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 3",
                    LinkPhoto = new List<LinkPhoto> { photoPathFive },
                    Description = "Description 3",
                    Price = 125m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Ботинки",
                    LinkPhoto = new List<LinkPhoto> { photoPathOne, photoPathTwo },
                    Description = "Ботинки, детская обувь, 30 размер, для мальчиков",
                    Price = 25m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 2",
                    LinkPhoto = new List<LinkPhoto> { photoPathThree, photoPathFour },
                    Description = "Description 2",
                    Price = 15.5m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                },
                new Announcement
                {
                    Name = "Name 3",
                    LinkPhoto = new List<LinkPhoto> { photoPathFive },
                    Description = "Description 3",
                    Price = 125m,
                    DateOfCreation = new DateTime(2021, 1, 14, 12, 45, 0)
                }
            };
            
            appContext.Announcements.AddRange(testData);
            appContext.SaveChanges();
        }
    }
}
