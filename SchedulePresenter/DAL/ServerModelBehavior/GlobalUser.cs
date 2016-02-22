using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class GlobalUser
    {
        private serverDBEntities context;

        public GlobalUser(serverDBEntities context)
        {
            this.context = context;
        }

        public GlobalUser GetUserDataForLoginAttempt(string login)
        {
            var users = from user in context.GlobalUser
                        where user.LOGIN == login
                        select user;

            return users.FirstOrDefault();        
        }    

        public List<GlobalUser> GetSuperAdminList()
        {
            var superAdmins = from admin in context.GlobalUser
                              where admin.USER_TYPE_DV_ID == 2
                              select admin;

            return superAdmins.ToList();
        }

        public List<GlobalUser> GetAdminList()
        {
            var superAdmins = from admin in context.GlobalUser
                              where admin.USER_TYPE_DV_ID == 3
                              select admin;

            return superAdmins.ToList();
        }

        public List<GlobalUser> GetList()
        {
            var admins = from admin in context.GlobalUser
                              select admin;

            return admins.ToList();
        }        
    }
}
