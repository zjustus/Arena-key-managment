namespace ArenaWeb.UserControls.custom.Luminate.DoorAccess{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Web.Security;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using AjaxControlToolkit;

    using Arena.Core;
    using Arena.Core.Communications;
    using Arena.Content;
	using Arena.Exceptions;
	using Arena.Portal;
	using Arena.Security;
	using Arena.Utility;
    using Arena.Organization; //?
    using Arena.Enums;
    using Arena.Exceptions;
    using Arena.List;
    using Arena.Portal.UI;
    using Arena.DataLib;
    using Arena.DataLayer;
    using Arena.DataLayer.Security;
    using Arena.DataLayer.List;


    /// <summary>
	///		This module manages the locations for the doors
	/// </summary>
	//public abstract class HtmlText : System.Web.UI.UserControl

    public partial class Doors : PortalControl
    {
        #region Module Settings
        [NumericSetting("Person Page", "The person page id", false)]
        public string PersonPageSetting { get { return Setting("PersonPage", "7", false); } }
        #endregion

        //DoorAccess.doorData doorDataLayer;

        //page load
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowList();
            }

			//pnlAdvanceReport.Visible = (BuildReportOptionSetting == "2"); //dont know yet
            pnlAdvanceReport.Visible = true;
        }

        private void ShowList()
        {
            DataTable DataSource = null;
            //CategoryEnable = (ddlReportCategory.Items.Count > 1);
            //CategoryEnable = false;
            //int listcategoryid = (CategoryEnable ? Int32.Parse(ddlReportCategory.SelectedValue.ToString()) : -1);
            //EditEnabled = CurrentModule.Permissions.Allowed(OperationType.Edit, CurrentUser);
            bool EditEnabled = true;
            //dgDoors.Columns[4].Visible = CategoryEnable;

            //get the DataSource
            //DataSource = new ListData().GetPublicReports(ArenaContext.Current.Organization.OrganizationID, listcategoryid, CurrentPerson.PersonID);
            DataSource = new doorData().GetDoorByLocation(Int32.Parse(Request["LocationID"]));

            dgDoors.Columns[5].Visible = true;

            dgDoors.Visible = true;
            dgDoors.ItemType = "List";
            dgDoors.ItemBgColor = CurrentPortalPage.Setting("ItemBgColor", string.Empty, false);
            dgDoors.ItemAltBgColor = CurrentPortalPage.Setting("ItemAltBgColor", string.Empty, false);
            dgDoors.ItemMouseOverColor = CurrentPortalPage.Setting("ItemMouseOverColor", string.Empty, false);
            dgDoors.AddEnabled = false;
            dgDoors.AddImageUrl = "~/images/add_list.gif";
            dgDoors.MoveEnabled = false;
            dgDoors.DeleteEnabled = EditEnabled;
            dgDoors.EditEnabled = EditEnabled;
            dgDoors.MergeEnabled = false;
            dgDoors.MailEnabled = false;
            dgDoors.ExportEnabled = false;
            dgDoors.AllowSorting = true;
            dgDoors.DataSource = DataSource;
            dgDoors.DataBind();

            dgDoors.Visible = true;
            //lbAdd.Visible = true;
        }
        //list support functions
        private void dgDoors_DeleteCommand(object sender, DataGridCommandEventArgs e)
        {
            //ListReport.Delete(Int32.Parse(e.Item.Cells[0].Text));
            //DelLocation
            int door_id = Int32.Parse(e.Item.Cells[0].Text);
            int theReturn = new doorData().DelDoor(door_id);
            dgDoors.ItemUpdated(door_id);
            dgDoors.EditItemIndex = -1;
            ShowList();
        }

        private void dgDoors_Rebind(object source, System.EventArgs e)
        {
            ShowList();
        }
        private void dgDoors_AddItem(object sender, System.EventArgs e)
        {


            /*
            if (BuildReportOptionSetting == "0")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", ListReportWizardPageIDSetting));
            else if (BuildReportOptionSetting == "1")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", AdvanceListWizardPageIDSetting));
            */
        }

        private void dgDoors_EditCommand(object source, DataGridCommandEventArgs e)
        {
            /*
            ListReport rpt = new ListReport(Int32.Parse(e.Item.Cells[0].Text));

            Response.Redirect(string.Format("default.aspx?page={0}&reportid={1}",
                rpt.NewFormat ? AdvanceListWizardPageIDSetting : ListReportWizardPageIDSetting,
                e.Item.Cells[0].Text));
            */
        }
        private void dgDoors_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			TextBox tbName = (TextBox)e.Item.FindControl("tbName");
            int door_id = Int32.Parse(e.Item.Cells[0].Text);
            int theReturn = new doorData().UpdateDoor(door_id, tbName.Text);

            dgDoors.ItemUpdated(door_id);
            dgDoors.EditItemIndex = -1;
			ShowList();
		}
        protected void imgBtnAddRpt_Click(object sender, EventArgs e)
        {
            int newDoor = new doorData().AddBlankDoor(Int32.Parse(Request["LocationID"]));
            ShowList();
            /*
            if (ddlSelectReport.SelectedValue.ToString() == "2")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", AdvanceListWizardPageIDSetting));
            else
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", ListReportWizardPageIDSetting));
            */
        }

        private void dgDoors_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            /*
            ListItemType li = (ListItemType)e.Item.ItemType;
            if (li == ListItemType.Item || li == ListItemType.AlternatingItem)
            {
                CheckBox cbSubscribed = (CheckBox)e.Item.FindControl("cbSubscribed");
                cbSubscribed.CheckedChanged += new EventHandler(cbSubscribed_CheckedChanged);
            }
            */
        }

        protected string GetFormattedDate(object dateCol)
        {
            if (dateCol.GetType() != Type.GetType("System.DBNull") && DateTime.Parse(dateCol.ToString()) != new DateTime(1900, 1, 1))
            {
                DateTime dd = (DateTime)dateCol;
                return dd.ToString("MM/dd/yy hh:mm tt");
            }
            else
                return string.Empty;
        }
        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.dgDoors.AddItem += new AddItemEventHandler(dgDoors_AddItem);
            this.dgDoors.DeleteCommand += new DataGridCommandEventHandler(dgDoors_DeleteCommand);
            this.dgDoors.EditCommand += new DataGridCommandEventHandler(dgDoors_EditCommand);
            this.dgDoors.ItemCommand += new DataGridCommandEventHandler(dgDoors_EditCommand);
            this.dgDoors.ReBind += new DataGridReBindEventHandler(this.dgDoors_Rebind);
            this.dgDoors.ItemDataBound += new DataGridItemEventHandler(dgDoors_ItemDataBound);
            this.dgDoors.ItemCreated += new DataGridItemEventHandler(dgDoors_ItemCreated);
            this.dgDoors.UpdateCommand += new DataGridCommandEventHandler(dgDoors_UpdateCommand);
            //this.ddlReportCategory.SelectedIndexChanged += new EventHandler(ddlReportCategory_SelectedIndexChanged);


            imgBtnAddRpt.Click += new EventHandler(imgBtnAddRpt_Click);
        }

        protected void dgDoors_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ListItemType li = (ListItemType)e.Item.ItemType;
                DataRowView row = (DataRowView)e.Item.DataItem;

                HtmlAnchor aName = new HtmlAnchor();
                /*
                aName.HRef = string.Format("~/default.aspx?page={0}&lookuptypeid={1}",
                    Int32.Parse(DetailPageIDSetting), row["lookup_type_id"].ToString());
                */
                aName.HRef = "~/default.aspx?";
                aName.InnerText = row["door_name"].ToString();
                /*
                aName.InnerText = string.IsNullOrWhiteSpace(row["location_name"].ToString()) ? "[ Unnamed Lookup Type ]" : row["location_id"].ToString();
                */
                PlaceHolder phName = (PlaceHolder)e.Item.FindControl("phName");
                phName.Controls.Add(aName);

                TextBox tbName = e.Item.FindControl("tbName") as TextBox;
				if (tbName != null)
				{
					ScriptManager.GetCurrent(this.Page).SetFocus(tbName);
				}


                /*
                if (bool.Parse(row["IsNew"].ToString()))
                {
                    Image img = new Image();
                    img.ImageUrl = "~/images/newreport.png";
                    img.ImageAlign = ImageAlign.Bottom;
                    e.Item.Cells[2].Controls.Add(img);
                }
                */
            }
        }

        #endregion
    }


////// DATA LAYER
    public partial class doorData : SqlData{
        //ExecuteSqlDataReader
        public DataTable GetDoorByLocation(int location_id){
            ArrayList paramList = new ArrayList();
            paramList.Add((object) new SqlParameter("@location_id", (object) location_id));
            try
            {
                return this.ExecuteDataTable("cust_luminate_key_sp_get_doors", paramList);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public int AddBlankDoor(int location_id){
            ArrayList paramList = new ArrayList();
            paramList.Add((object) new SqlParameter("@location_id", (object) location_id));

            paramList.Add(new SqlParameter("@door_name", "New Door"));

            SqlParameter paramOut = new SqlParameter(); //output paramaters
            paramOut.ParameterName = "@door_id";
            paramOut.Direction = ParameterDirection.Output;
            paramOut.SqlDbType = SqlDbType.Int;
            paramList.Add(paramOut);

            try
            {
                this.ExecuteNonQuery("cust_luminate_key_sp_add_door", paramList);
                return (int)((SqlParameter)(paramList[paramList.Count - 1])).Value;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        //need to make sproc
        public int UpdateDoor(int door_id, string door_name){
            ArrayList paramList = new ArrayList();

            paramList.Add(new SqlParameter("@door_id", door_id));
            paramList.Add(new SqlParameter("@door_name", door_name));

            try
            {
                this.ExecuteNonQuery("cust_luminate_key_sp_update_door", paramList);
                //return (int)((SqlParameter)(paramList[paramList.Count - 1])).Value;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
            return 1;
        }
        public int DelDoor(int door_id){
            ArrayList paramList = new ArrayList();
            paramList.Add(new SqlParameter("@door_id", door_id));

            try
            {
                this.ExecuteNonQuery("cust_luminate_key_sp_del_door", paramList);
                //return (int)((SqlParameter)(paramList[paramList.Count - 1])).Value;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
            return 1;
        }
    }
}
