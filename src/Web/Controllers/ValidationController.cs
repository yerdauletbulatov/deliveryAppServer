using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ValidationController : Controller
    {
        
        private AppIdentityDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ValidationController(AppIdentityDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [AcceptVerbs("GET","POST")]
        public bool CheckEmail(string email, string id)
        {
            var users = _userManager.Users.AsQueryable();
            if (id != null)
                users = users.Where(u => u.Id != id);
            return !users.Any(u => u.Email.ToLower() == email.ToLower());
        }
    }
}