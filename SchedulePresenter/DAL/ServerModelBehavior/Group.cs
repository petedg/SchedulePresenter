using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Group
    {
        private serverDBEntities context;

        public Group(serverDBEntities context)
        {
            this.context = context;
        }

        public Group GetGroupById(int groupId)
        {
            var groups = from group_g in context.Group
                         where group_g.ID == groupId
                         select group_g;

            return groups.FirstOrDefault();
        }

        public List<object> GetGroupsForParentSubgroup(Subgroup parentSubgroup)
        {
            var groups = from group_1 in context.Group
                         where group_1.SUBGROUP_ID == parentSubgroup.ID
                         select group_1;

            return groups.ToList<object>();
        }      

    }
}
