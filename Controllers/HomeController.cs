using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const int PageSize = 10;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, bool isFullAdSearchString, int? pageNumber)
        {
            IEnumerable<Announcement> announcements = Enumerable.Empty<Announcement>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = await client.GetAsync($"https://localhost:44390/api/announcement/{searchString}");
                if (responseTask.IsSuccessStatusCode)
                {
                    announcements = responseTask.Content.ReadAsAsync<IList<Announcement>>().Result;
                }
                else 
                {
                    announcements = Enumerable.Empty<Announcement>();

                    ModelState.AddModelError(string.Empty, "Not Found.");
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["FullAd"] = isFullAdSearchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "name";
            ViewData["DateSortParm"] = sortOrder == "date" ? "dateDesc" : "date";
            switch (sortOrder)
            {
                case "name":
                    announcements = announcements.OrderBy(ad => ad.Name);
                    break;
                case "nameDesc":
                    announcements = announcements.OrderByDescending(ad => ad.Name);
                    break;
                case "date":
                    announcements = announcements.OrderBy(ad => ad.DateOfCreation);
                    break;
                case "dateDesc":
                    announcements = announcements.OrderByDescending(ad => ad.DateOfCreation);
                    break;
                default:
                    break;
            }

            return View(await PaginatedList<AnnouncementViewModel>.CreateAsync(GetViewModel(announcements.ToList()), pageNumber ?? 1, PageSize));
        }
        
        public async Task<ActionResult> Create(AnnouncementViewModel ad)
        {
            int count = Request.Form.Files.Count;
            if (ad.MainLinkPhoto != null && ad.MainLinkPhoto.Split(" ").Length > 3)
            {
                ModelState.AddModelError("photoLink", "To much photos' links.");
            }

            if (ModelState.IsValid)
            {
                
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseTask = await client.PostAsJsonAsync("https://localhost:44390/api/announcement/", ad);
                    if (!responseTask.IsSuccessStatusCode)
                    {
                        return BadRequest();
                    }
                }
            }
            return View(ad);
        }

        private IEnumerable<AnnouncementViewModel> GetViewModel(List<Announcement> ad)
        {
            List<AnnouncementViewModel> resultModel = new List<AnnouncementViewModel>();
            ad.ForEach((Announcement item) =>
            {
                resultModel.Add(new AnnouncementViewModel
                {
                    Name = item.Name,
                    Price = item.Price,
                    CreatedDate = item.DateOfCreation.ToString(),
                    MainLinkPhoto = item.LinkPhoto is null || item.LinkPhoto.Count == 0 ? "No Picture" : "Main Photo"
                });
            });

            return resultModel;
        }
    }
}
