using MusicApp.Helpers;
using MusicApp.Models;
using MusicApp.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace MusicApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [ActionName("NewUser")]
        [CustomAuthorize]
        public ActionResult NewUserAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("NewUser")]
        [CustomAuthorize]
        public async Task<ActionResult> NewUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await _userRepository.AddUserAsync(user))
            {
                return new HttpStatusCodeResult(503, "Failed to add the user");
            }

            return RedirectToAction("NewUser");
        }

        [HttpGet]
        [ActionName("GetUsers")]
        [CustomAuthorize]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userRepository.GetUserDetailsAsync();

            if (users == null || users.Count == 0)
            {
                return new HttpStatusCodeResult(500);
            }

            return View(users);
        }

        [HttpGet]
        [ActionName("GetUser")]
        [CustomAuthorize]
        public async Task<ActionResult> GetUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);

            if (user == null)
            {
                return new HttpStatusCodeResult(404, "User Not Found");
            }

            return View(user);
        }

        [HttpGet]
        [ActionName("UpdateUser")]
        [CustomAuthorize]
        public async Task<ActionResult> UpdateUserAsync(string id)
        {
            return View(await GetUserByIdAsync(id));
        }

        [HttpPost]
        [ActionName("UpdateUser")]
        [CustomAuthorize]
        public async Task<ActionResult> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await _userRepository.UpdateUserAsync(user))
            {
                return new HttpStatusCodeResult(503, "Failed to update the user");
            }

            return RedirectToAction("GetUser", new {id = user.UserId});
        }

        [HttpGet]
        [ActionName("DeleteUser")]
        [CustomAuthorize]
        public async Task<ActionResult> DeleteUserAsync(string id)
        {
            return View(await GetUserByIdAsync(id));
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        [CustomAuthorize]
        public async Task<ActionResult> DeleteUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await _userRepository.DeleteUserAsync(user.UserId))
            {
                return new HttpStatusCodeResult(503, "Failed to delete the user");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("LoginUser")]
        [AllowAnonymous]
        public ActionResult LoginUserAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("LoginUser")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUserAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new HttpStatusCodeResult(400, "Username and password are required");
            }

            var users = await _userRepository.GetUserDetailsAsync();
            var user = users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user == null)
            {
                return new HttpStatusCodeResult(401, "Invalid credentials");
            }

            FormsAuthentication.SetAuthCookie(userName, false);

            return RedirectToAction("GetMusicDetails", "Music");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginUser");
        }

        private async Task<User> GetUserByIdAsync(string id)
        {
            var decryptedId = CommonHelper.DecryptId(id);
            return await _userRepository.GetUserAsync(decryptedId);
        }
    }
}