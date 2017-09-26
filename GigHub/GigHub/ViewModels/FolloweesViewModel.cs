using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class FolloweesViewModel
    {
        public IEnumerable<ApplicationUser> Artists { get; set; }
    }
}