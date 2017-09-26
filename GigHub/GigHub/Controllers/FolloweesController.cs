using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artist = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(i => i.Followee)
                .ToList();

            return View(artist);
        }
    }
}
