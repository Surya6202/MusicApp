using MusicApp.Helpers;
using MusicApp.Models;
using MusicApp.Repository;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    [CustomAuthorize]
    public class MusicController : Controller
    {
        private readonly IMusicRepository _musicRepository;

        public MusicController(MusicRepository musicRepository)
        {
            _musicRepository = musicRepository ??
                throw new ArgumentNullException(nameof(musicRepository));
        }

        [HttpGet]
        [ActionName("GetMusicDetails")]
        public async Task<ActionResult> GetMusicDetailsAsync()
        {
            var musicDetails = await _musicRepository.GetMusicDetailsAsync();

            if (musicDetails == null)
            {
                return new HttpStatusCodeResult(404, "Music details not found");

            }

            return View(musicDetails);
        }

        [HttpGet]
        [ActionName("GetMusic")]
        public async Task<ActionResult> GetMusicDetailsByIdAsync(string id)
        {
            var musicDetails = await GetMusicById(id);

            if (musicDetails == null)
            {
                return new HttpStatusCodeResult(404, "Music details not found");

            }

            return View(musicDetails);
        }

        [HttpGet]
        [ActionName("NewMusic")]
        public async Task<ActionResult> NewMusicAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("NewMusic")]
        public async Task<ActionResult> NewMusicAsync(Music music)
        {
            if (music == null)
            {
                return new HttpStatusCodeResult(500);
            }

            if (!await _musicRepository.AddMusicAsync(music))
            {
                return new HttpStatusCodeResult(503);
            }

            return RedirectToAction("GetMusicDetails");
        }

        [HttpGet]
        [ActionName("UpdateMusic")]
        public async Task<ActionResult> UpdateMusicAsync(string id)
        {
            return View(await GetMusicById(id));
        }

        [HttpPost]
        [ActionName("UpdateMusic")]
        public async Task<ActionResult> UpdateMusicAsync(Music music)
        {
            if (music == null)
            {
                return new HttpStatusCodeResult(500);
            }

            if (!await _musicRepository.UpdateMusicAsync(music))
            {
                return new HttpStatusCodeResult(503);
            }

            return RedirectToAction("GetMusic",new {id = music.MusicId});
        }

        [HttpGet]
        [ActionName("DeleteMusic")]
        public async Task<ActionResult> DeleteMusicAsync(string id)
        {
            return View(await GetMusicById(id));
        }

        [HttpPost]
        [ActionName("DeleteMusic")]
        public async Task<ActionResult> DeleteMusicAsync(Music music)
        {
            if (music == null)
            {
                return new HttpStatusCodeResult(500);
            }

            if (!await _musicRepository.DeleteMusicAsync(music.MusicId))
            {
                return new HttpStatusCodeResult(503);
            }

            return RedirectToAction("GetMusicDetails");
        }

        private async Task<Music> GetMusicById(string id)
        {
            var decryptedId = CommonHelper.DecryptId(id);
            return await _musicRepository.GetMusicAsync(decryptedId);
        }
    }
}