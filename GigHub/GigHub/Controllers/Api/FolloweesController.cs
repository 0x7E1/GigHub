using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/followees")]
    public class FolloweesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("follow")]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (!_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
            {
                if (userId != dto.FolloweeId)
                {
                    var following = new Following
                    {
                        FollowerId = userId,
                        FolloweeId = dto.FolloweeId
                    };

                    _context.Followings.Add(following);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest("You can not follow yourself!");
            }
            return BadRequest("Following already exist!");
        }
    }
}
