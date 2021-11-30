using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;

namespace UnityLab
{
    public class BaseEntity
    {
        [AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        public int Id { get; set; }
    }
}
