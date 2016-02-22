using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Subgroup
    {
        private serverDBEntities context;
        private Group groupBehavior;

        public Subgroup(serverDBEntities context)
        {
            this.context = context;
            groupBehavior = new Group(context);
        }

        public Subgroup GetSubgroupById(int subgroupId)
        {
            var subgroups = from subgroup in context.Subgroup
                            where subgroup.ID == subgroupId
                            select subgroup;

            return subgroups.FirstOrDefault();
        }

        public List<object> GetSubgroupsForMajor(Major major)
        {
            int activeSemesterID = new Semester(context).GetActiveSemester().ID;

            var subgroups = from subgroup in context.Subgroup
                            where subgroup.MAJOR_ID == major.ID && subgroup.SEMESTER_ID == activeSemesterID && subgroup.SUBGROUP_ID == null
                            select subgroup;

            return subgroups.ToList<object>();
        }

        public List<Subgroup> GetSubgroupsForParentSubgroup(Subgroup parentSubgroup)
        {
            int activeSemesterID = new Semester(context).GetActiveSemester().ID;

            var subgroups = from subgroup in context.Subgroup
                            where subgroup.MAJOR_ID == parentSubgroup.MAJOR_ID && subgroup.SEMESTER_ID == activeSemesterID && subgroup.SUBGROUP_ID == parentSubgroup.ID
                            select subgroup;

            return subgroups.ToList();
        }                

        public List<object> NestedSubgroupsList { get; set; }
        public List<object> GroupsList { get; set; }
        public List<dynamic> NestedSubgroupsAndGroups { get; set; }
    }
}
