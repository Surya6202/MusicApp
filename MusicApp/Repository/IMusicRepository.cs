using MusicApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicApp.Repository
{
    public interface IMusicRepository
    {
        Task<List<Music>> GetMusicDetailsAsync();

        Task<Music> GetMusicAsync(int musicId);

        Task<bool> AddMusicAsync(Music music);

        Task<bool> UpdateMusicAsync(Music music);

        Task<bool> DeleteMusicAsync(int id);
    }
}
