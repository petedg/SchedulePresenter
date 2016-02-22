using SchedulePresenter.DAL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    class TreeViewData
    {
        private serverDBEntities context;
        private Major majorBehavior;
        private Subgroup subgroupBehavior;
        private Group groupBehavior;
        private Department departmentBehavior;
        private Location locationBehavior;
        private Room roomBehavior;

        public List<Major> MajorList { get; set; }
        public List<Department> DepartmentList { get; set; }

        public TreeViewData(serverDBEntities context, Department department)
        {
            this.context = context;

            majorBehavior = new Major(context);
            subgroupBehavior = new Subgroup(context);
            groupBehavior = new Group(context);
            departmentBehavior = new Department(context);
            locationBehavior = new Location(context);
            roomBehavior = new Room(context);
            
            initializeGroupList(department);            
        }        

        private void initializeGroupList(Department department)
        {
            MajorList = majorBehavior.GetMajorsForDepartment(department);

            foreach (Major m in MajorList)
            {
                m.CompositeSubgroupsList = new List<dynamic>();
                
                foreach(Subgroup sub in subgroupBehavior.GetSubgroupsForMajor(m).Cast<Subgroup>().ToList())                                   
                {
                    List<Group> groupsForParentSubgroup = groupBehavior.GetGroupsForParentSubgroup(sub).Cast<Group>().ToList();               

                    dynamic compositeSubgroupsAndGroups = new ExpandoObject();
                    compositeSubgroupsAndGroups.Name = sub.NAME;                                                                             
                    compositeSubgroupsAndGroups.Subgroup = sub;                                                                                
                    compositeSubgroupsAndGroups.Groups = groupsForParentSubgroup;
                    compositeSubgroupsAndGroups.CompositeSubgroupsList = new List<dynamic>();

                    m.CompositeSubgroupsList.Add(compositeSubgroupsAndGroups);
                }

                foreach (dynamic s in m.CompositeSubgroupsList)
                {
                    List<Subgroup> subgroupsForParentSubgroup = subgroupBehavior.GetSubgroupsForParentSubgroup(s.Subgroup);                    

                    foreach (Subgroup sub in subgroupBehavior.GetSubgroupsForParentSubgroup(s.Subgroup))
                    {
                        List<Group> groupsForParentSubgroup = groupBehavior.GetGroupsForParentSubgroup(sub).Cast<Group>().ToList();

                        dynamic compositeSubgroupsAndGroups = new ExpandoObject();
                        compositeSubgroupsAndGroups.Name = sub.NAME;
                        compositeSubgroupsAndGroups.Subgroup = sub;
                        compositeSubgroupsAndGroups.Groups = groupsForParentSubgroup;
                        compositeSubgroupsAndGroups.CompositeSubgroupsList = new List<dynamic>();

                        s.CompositeSubgroupsList.Add(compositeSubgroupsAndGroups);                                            
                    }                   
                }
            }
        }                
    }
}
