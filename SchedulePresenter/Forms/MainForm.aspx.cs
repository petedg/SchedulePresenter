using SchedulePresenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchedulePresenter.Forms
{
    public partial class MainForm : System.Web.UI.Page
    {
        private serverDBEntities context;
        private Department departmentBehavior;
        private CurrentSchedule currentScheduleBehavior;
        private Group groupBehavior;
        private Week weekBehavior;
        private Semester semesterBehavior;

        private List<Major> MajorsList;
        private Department currentDepartment;

        private List<LinkButton> departmentsButtons = new List<LinkButton>();

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new serverDBEntities();
            departmentBehavior = new Department(context);
            currentScheduleBehavior = new CurrentSchedule(context);
            groupBehavior = new Group(context);
            weekBehavior = new Week(context);
            semesterBehavior = new Semester(context);

            List<Department> departments = departmentBehavior.GetList();

            List<Week> weeksForSemester = weekBehavior.GetListForSemester(semesterBehavior.GetActiveSemester());               
            var weekBoxItemsSource = from week in weeksForSemester
                                     select new { WeekID = week.ID, DateSpan = week.START_DATE.Date.ToShortDateString() + "  -  " + week.END_DATE.Date.ToShortDateString() };              
            
            foreach (Department department in departments)
            {
                LinkButton label = new LinkButton();
                label.Text = department.NAME;
                label.CssClass = "departmentLinktButton";
                label.Click += label_Click;
                label.CommandArgument = department.ID.ToString();
                departmentsPanel.Controls.Add(label);
                departmentsButtons.Add(label);
            }

            if (!this.IsPostBack)
            {
                if (departmentsButtons.Count != 0)
                {
                    label_Click(departmentsButtons[0], new EventArgs());
                }

                DropDownList1.DataSource = weekBoxItemsSource.ToList();
                DropDownList1.DataTextField = "DateSpan";
                DropDownList1.DataValueField = "WeekID";
                DropDownList1.DataBind();

                Week currentWeek = weekBehavior.GetCurrentWeek();
                if (currentWeek != null)
                {
                    DropDownList1.SelectedValue = currentWeek.ID.ToString();
                }
            }

            if (treeViewGroups.SelectedNode != null)
            {
                buttonPdf.Visible = true;
                buttonPng.Visible = true;
                Label1.Visible = true;
                DropDownList1.Visible = true;
            }
            else
            {
                buttonPdf.Visible = false;
                buttonPng.Visible = false;
                Label1.Visible = false;
                DropDownList1.Visible = false;
            }            
        }

        void label_Click(object sender, EventArgs e)
        {
            int departmentID = Int32.Parse(((LinkButton)sender).CommandArgument);
            currentDepartment = departmentBehavior.GetDepartmentById(departmentID);
            Label2.Text = "WYBÓR GRUPY" + "<br />" + "- " + currentDepartment.NAME + " -"; 

            populateTreeView(currentDepartment);
            treeViewGroups.CollapseAll();
            if (treeViewGroups.SelectedNode != null)
            {
                treeViewGroups.SelectedNode.Selected = false;
            }

            buttonPdf.Visible = false;
            buttonPng.Visible = false;
            Label1.Visible = false;
            DropDownList1.Visible = false;
        }

        private void populateTreeView(Department department)
        {
            treeViewGroups.Nodes.Clear();
            MajorsList = new TreeViewData(context, department).MajorList;

            foreach (Major major in MajorsList)
            {
                TreeNode root = new TreeNode
                {
                    Text = major.NAME,
                    Value = "M" + major.ID.ToString(),
                };
                root.ImageUrl = @"/Resources/Images/appbar_pen1.png";
                root.SelectAction = TreeNodeSelectAction.None;
                treeViewGroups.Nodes.Add(root);
                populateTreeViewWithSubgroups_S1(major, root);                
            }
        }


        private void populateTreeViewWithSubgroups_S1(Major major, TreeNode treeNode)
        {
            foreach (dynamic composite in major.CompositeSubgroupsList)
            {
                TreeNode child = new TreeNode
                {
                    Text = composite.Subgroup.NAME,
                    Value = "S" + composite.Subgroup.ID,
                };
                child.ImageUrl = @"/Resources/Images/appbar_tiles_nine1.png";
                child.SelectAction = TreeNodeSelectAction.None;
                treeNode.ChildNodes.Add(child);

                populateTreeViewWithSubgroups_S2(composite, child);
                populateTreeViewWithGroups(composite, child);
            }
        }

        private void populateTreeViewWithSubgroups_S2(dynamic parentComposite, TreeNode treeNode)
        {
            foreach (dynamic composite in parentComposite.CompositeSubgroupsList)
            {
                TreeNode child = new TreeNode
                {
                    Text = composite.Subgroup.NAME,
                    Value = "S" + composite.Subgroup.ID,
                };
                child.ImageUrl = @"/Resources/Images/appbar_tiles_sixteen1.png";
                child.SelectAction = TreeNodeSelectAction.None;                
                treeNode.ChildNodes.Add(child);

                populateTreeViewWithSubgroups_S2(composite, child);
                populateTreeViewWithGroups(composite, child);
            }
        }

        private void populateTreeViewWithGroups(dynamic composite, TreeNode treeNode)
        {
            foreach (Group group in composite.Groups)
            {
                TreeNode child = new TreeNode
                {
                    Text = group.NAME,
                    Value = group.ID.ToString(),
                };
                child.ImageUrl = @"/Resources/Images/appbar_folder_people1.png";                
                treeNode.ChildNodes.Add(child);                
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            Label1.Text = groupBehavior.GetGroupById(Int32.Parse(treeViewGroups.SelectedValue)).NAME;
        }

        protected void buttonPdf_Click(object sender, EventArgs e)
        {
            int groupID = Int32.Parse(treeViewGroups.SelectedValue);
            int weekID = Int32.Parse(DropDownList1.SelectedValue);

            Group gg = groupBehavior.GetGroupById(groupID);

            CurrentSchedule currentSchedule = currentScheduleBehavior.GetCurrentSchedule(groupID, weekID);

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=" + gg.NAME.Replace(' ', '_') + ".pdf");
            Response.AddHeader("content-length", currentSchedule.SCHEDULE_PDF.Length.ToString());
            Response.BinaryWrite(currentSchedule.SCHEDULE_PDF);
        }

        protected void buttonPng_Click(object sender, EventArgs e)
        {
            int groupID = Int32.Parse(treeViewGroups.SelectedValue);
            int weekID = Int32.Parse(DropDownList1.SelectedValue);

            Group gg = groupBehavior.GetGroupById(groupID);

            CurrentSchedule currentSchedule = currentScheduleBehavior.GetCurrentSchedule(groupID, weekID);

            Response.ContentType = "application/png";
            Response.AppendHeader("Content-Disposition", "inline; filename=" + gg.NAME.Replace(' ', '_') + ".png");
            Response.AddHeader("content-length", currentSchedule.SCHEDULE_PNG.Length.ToString());
            Response.BinaryWrite(currentSchedule.SCHEDULE_PNG);
        }
    }
}