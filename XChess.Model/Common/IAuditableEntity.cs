using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XChess.Model.Common
{
    public  interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }
        long? CreatedID { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime UpdatedDate { get; set; }
        long? UpdatedID { get; set; }
        string UpdatedBy { get; set; }
    }
}
