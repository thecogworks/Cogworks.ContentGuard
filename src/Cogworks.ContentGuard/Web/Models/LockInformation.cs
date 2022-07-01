using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cogworks.ContentGuard.Web.Models
{
    public class LockInformation
    {
        public bool IsPageLocked { get; set; }
        public string CurrentlyEditingUserName { get; set; } = string.Empty;   
    }
}
