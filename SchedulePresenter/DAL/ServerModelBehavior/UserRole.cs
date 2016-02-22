using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class UserRole
    {
        private serverDBEntities context;

        public UserRole()
        {

        }

        public UserRole(serverDBEntities context)
        {
            this.context = context;
        }
    }
}
