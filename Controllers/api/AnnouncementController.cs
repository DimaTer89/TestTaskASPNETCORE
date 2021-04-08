using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.Repositories.Interfacies;

namespace TestTask.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private IRepository repository;
        public AnnouncementController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Announcement>> GetAll()
        {
            return new JsonResult(repository.GetAll());
        }

        [HttpGet("{titleAd}")]
        public ActionResult GetOneAnnouncement(string titleAd)
        {
            IEnumerable<Announcement> currentAnnouncement = repository.Get(titleAd);
            if (currentAnnouncement is null)
            {
                return NotFound();
            }

            return new JsonResult(currentAnnouncement);
        }

        [HttpPost]
        public ActionResult Create(AnnouncementViewModel announcement)
        {
            if (announcement is null)
            {
                return BadRequest();
            }
            Announcement ad = new Announcement();
            ad.Name = announcement.Name;
            ad.Description = announcement.Description;
            ad.Price = announcement.Price;
            ad.DateOfCreation = DateTime.Now;
            if (!string.IsNullOrEmpty(announcement.MainLinkPhoto))
            {
                string[] links = announcement.MainLinkPhoto.Split(",");
                List<LinkPhoto> linkPhotos = new List<LinkPhoto>();
                for (int i = 0; i < links.Length; i++)
                {
                    linkPhotos.Add(new LinkPhoto { Path = links[i] });
                }
                repository.CreateLinks(linkPhotos);
                ad.LinkPhoto = linkPhotos.ToList();
            }

            repository.Create(ad);
            return Ok(ad.Id);
        }
    }
}
