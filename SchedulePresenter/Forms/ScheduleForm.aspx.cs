using SchedulePresenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SchedulePresenter.Forms
{
    public partial class ScheduleForm : System.Web.UI.Page
    {
        private serverDBEntities context;
        private Department departmentBehavior;
        private CurrentSchedule currentScheduleBehavior;
        private Group groupBehavior;
        private Week weekBehavior;
        private Semester semesterBehavior;

        private List<Major> MajorsList;

        private int? choosenGroupID;

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new serverDBEntities();
            departmentBehavior = new Department(context);
            currentScheduleBehavior = new CurrentSchedule(context);
            groupBehavior = new Group(context);
            weekBehavior = new Week(context);
            semesterBehavior = new Semester(context);

            List<Department> departments = departmentBehavior.GetList();            

            foreach (Department department in departments)
            {
                HtmlGenericControl listItem = createListTextItem(department.NAME, null);
                menuList.Controls.Add(listItem);
                populateTreeView(department, listItem);
            }            

            if (!this.IsPostBack)
            {
                List<Week> weeksForSemester = weekBehavior.GetListForSemester(semesterBehavior.GetActiveSemester());
                var weekBoxItemsSource = from week in weeksForSemester
                                         select new { WeekID = week.ID, DateSpan = week.START_DATE.Date.ToShortDateString() + "  -  " + week.END_DATE.Date.ToShortDateString() };

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

            if (Request.QueryString["group"] != null)
            {
                if (currentScheduleBehavior.HasCurrentSchedule(Int32.Parse(Request.QueryString["group"])))
                {
                    choosenGroupID = Int32.Parse(Request.QueryString["group"]);
                    label.InnerText = groupBehavior.GetGroupById((int)choosenGroupID).NAME;
                    DropDownList1.Visible = true;
                    buttonPdf.Visible = true;
                    buttonPng.Visible = true;
                }
                else
                {
                    label.InnerText = "Brak aktualnego planu dla grupy ";
                    choosenGroupID = Int32.Parse(Request.QueryString["group"]);
                    label.InnerText += groupBehavior.GetGroupById((int)choosenGroupID).NAME;                    
                }
            }
            else
            {
                choosenGroupID = null;
                label.InnerText = "Aby rozpocząć, wybierz grupę z menu...";
                DropDownList1.Visible = false;
                buttonPdf.Visible = false;
                buttonPng.Visible = false;
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if(context != null)
                context.Dispose();
        }

        private HtmlGenericControl createListTextItem(string content, string url)
        {
            HtmlGenericControl listItemLink = new HtmlGenericControl("a");
            listItemLink.Attributes.Add("href", url);            
            listItemLink.InnerText = content;

            HtmlGenericControl listItem = new HtmlGenericControl("li");
            listItem.Controls.Add(listItemLink);

            return listItem;
        }


        private HtmlGenericControl createNestedUL()
        {
            HtmlGenericControl nestedUL = new HtmlGenericControl("ul");
            return nestedUL;
        }

        private void populateTreeView(Department department, HtmlGenericControl nodeListItem)
        {
            MajorsList = new TreeViewData(context, department).MajorList;

            if (MajorsList.Count > 0)
            {
                HtmlGenericControl nestedUL = createNestedUL();                

                foreach (Major major in MajorsList)
                {
                    HtmlGenericControl listItem = createListTextItem(major.NAME, null);
                    nestedUL.Controls.Add(listItem);
                    populateTreeViewWithSubgroups_S1(major, listItem);
                }

                nodeListItem.Controls.Add(nestedUL);
            }            
        }

        private void populateTreeViewWithSubgroups_S1(Major major, HtmlGenericControl nodeListItem)
        {
            if (major.CompositeSubgroupsList.Count > 0)
            {
                HtmlGenericControl nestedUL = createNestedUL();

                foreach (dynamic composite in major.CompositeSubgroupsList)
                {
                    HtmlGenericControl listItem = createListTextItem(composite.Subgroup.NAME, null);
                    nestedUL.Controls.Add(listItem);
                    populateTreeViewWithSubgroups_S2(composite, listItem);
                    populateTreeViewWithGroups(composite, listItem);
                }

                nodeListItem.Controls.Add(nestedUL);
            }
        }

        private void populateTreeViewWithSubgroups_S2(dynamic parentComposite, HtmlGenericControl nodeListItem)
        {
            if (parentComposite.CompositeSubgroupsList.Count > 0)
            {
                HtmlGenericControl nestedUL = createNestedUL();

                foreach (dynamic composite in parentComposite.CompositeSubgroupsList)
                {
                    HtmlGenericControl listItem = createListTextItem(composite.Subgroup.NAME, null);
                    nestedUL.Controls.Add(listItem);
                    populateTreeViewWithSubgroups_S2(composite, listItem);
                    populateTreeViewWithGroups(composite, listItem);
                }

                nodeListItem.Controls.Add(nestedUL);
            }
        }

        private void populateTreeViewWithGroups(dynamic composite, HtmlGenericControl nodeListItem)
        {
            if (composite.Groups.Count > 0)
            {
                HtmlGenericControl nestedUL = createNestedUL();

                foreach (Group group in composite.Groups)
                {
                    HtmlGenericControl listItemGroup = createListTextItem(group.NAME, "?group=" + group.ID);
                    nestedUL.Controls.Add(listItemGroup);
                }

                nodeListItem.Controls.Add(nestedUL);
            }
        }

        protected void buttonPdf_Click(object sender, EventArgs e)
        {
            int groupID = (int)choosenGroupID;
            int weekID = Int32.Parse(DropDownList1.SelectedValue);            

            Group gg = groupBehavior.GetGroupById(groupID);

            CurrentSchedule currentSchedule = currentScheduleBehavior.GetCurrentSchedule(groupID, weekID);

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=\"" + gg.NAME + " " + DropDownList1.SelectedItem.Text + ".pdf\"");
            Response.AddHeader("content-length", currentSchedule.SCHEDULE_PDF.Length.ToString());
            Response.BinaryWrite(currentSchedule.SCHEDULE_PDF);
        }

        protected void buttonPng_Click(object sender, EventArgs e)
        {
            int groupID = (int)choosenGroupID;
            int weekID = Int32.Parse(DropDownList1.SelectedValue);

            Group gg = groupBehavior.GetGroupById(groupID);

            CurrentSchedule currentSchedule = currentScheduleBehavior.GetCurrentSchedule(groupID, weekID);

            Response.ContentType = "application/png";
            Response.AppendHeader("Content-Disposition", "inline; filename=\"" + gg.NAME + " " + DropDownList1.SelectedItem.Text + ".png\"");
            Response.AddHeader("content-length", currentSchedule.SCHEDULE_PNG.Length.ToString());
            Response.BinaryWrite(currentSchedule.SCHEDULE_PNG);
        }
    }
}