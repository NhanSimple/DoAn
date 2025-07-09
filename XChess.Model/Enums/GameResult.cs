using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Enums
{
    public enum GameResult
    {
        [Display(Name = "Thắng")]
        Win,

        [Display(Name = "Thua")]
        Lose,

        [Display(Name = "Hòa")]
        Draw
    }
}
