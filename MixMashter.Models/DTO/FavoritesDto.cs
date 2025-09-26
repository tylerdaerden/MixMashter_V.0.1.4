using MixMashter.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMashter.Models.DTO
{
    public class FavoriteReadDto
    {
        public int UserId { get; set; }
        public int MashupId { get; set; }
        public DateTime AddedDate { get; set; }

        // Pour enrichir la réponse (comme fait pour la playlist et être plus user friendly en affichant un max d'info , ainsi ce sera opti pour le front)
        public MashupReadDto Mashup { get; set; } = null!; //ici j'utilise le DTO MashupReadDto crée comme autre DTO pour renvoyer plus d'infos sur le mashup en question
    }



}
