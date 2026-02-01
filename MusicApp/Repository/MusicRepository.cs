using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private readonly AppDbContext _dbContext;

        public MusicRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext ?? 
                throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<List<Music>> GetMusicDetailsAsync()
        {
            var musicDetails = await _dbContext.MusicDetails.ToListAsync();

            if (musicDetails.Count  == 0)
            {
                return null;
            }

            return await Task.FromResult(musicDetails);
        }

        public async Task<Music> GetMusicAsync(int musicId)
        {
            var music = await _dbContext.MusicDetails.FindAsync(musicId);

            if (music == null)
            {
                return null;
            }

            return await Task.FromResult(music);
        }

        public async Task<bool> AddMusicAsync(Music music)
        {
            var isSuccess = false;
            if (music == null)
            {
                return isSuccess;
            }

            try
            {
                music.IsPublished = music.ReleasedDate <= DateTime.UtcNow;
                _dbContext.MusicDetails.Add(music);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }

        public async Task<bool> UpdateMusicAsync(Music music)
        {
            var isSuccess = false;
            if (music == null)
            {
                return isSuccess;
            }

            try
            {
                var existingMusic = await GetMusicAsync(music.MusicId);

                if (existingMusic == null)
                    return isSuccess;

                existingMusic.MusicName = music.MusicName;
                existingMusic.PublishedYear = music.PublishedYear;
                existingMusic.ReleasedDate = music.ReleasedDate;
                existingMusic.IsPublished = music.ReleasedDate <= DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }

        public async Task<bool> DeleteMusicAsync(int id)
        {
            var isSuccess = false;
            var music = await GetMusicAsync(id);

            if (music == null)
            {
                return isSuccess;
            }

            try
            {
                _dbContext.MusicDetails.Remove(music);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception)
            {
                return false;
            }

            return isSuccess;
        }
    }
}