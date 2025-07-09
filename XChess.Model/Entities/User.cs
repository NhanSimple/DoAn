using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XChess.Model.Common;

namespace XChess.Model.Entities
{
    public class User : AuditableEntity<long>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        // Quan hệ 1-nhiều với các bảng liên quan
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
        public virtual ICollection<MatchResult> MatchResults { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }
}
