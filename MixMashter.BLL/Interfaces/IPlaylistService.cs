using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixMashter.Models.Entities;

namespace MixMashter.BLL.Interfaces
{
    public interface IPlaylistService
    {
        Task<Playlist?> GetByIdAsync(int id);
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task<Playlist?> CreateAsync(Playlist playlist);
        Task<bool> UpdateAsync(Playlist playlist);
        Task<bool> DeleteAsync(int id);

        // bonus : compter le nombre de chansons dans une playlist , vu dans un autre projet donc potentiellement à ajouter en bonus chokotoff
        Task<int> GetSongCountAsync(int playlistId);
    }
}

