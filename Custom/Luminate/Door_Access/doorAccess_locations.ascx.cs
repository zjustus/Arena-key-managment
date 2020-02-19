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

    public partial class Locations : PortalControl
    {
        #region Module Settings
        [NumericSetting("Person Page", "The person page id", false)]
        public string PersonPageSetting { get { return Setting("PersonPage", "7", false); } }
        #endregion

        //DoorAccess.locationData locationDataLayer;

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
            //dgLocations.Columns[4].Visible = CategoryEnable;

            //get the DataSource
            //DataSource = new ListData().GetPublicReports(ArenaContext.Current.Organization.OrganizationID, listcategoryid, CurrentPerson.PersonID);
            if(String.IsNullOrEmpty(Request["parentID"])){
                DataSource = new locationData().GetLocationsByParent();
            }
            else{
                DataSource = new locationData().GetLocationsByParent(Int32.Parse(Request["parentID"]));
            }

            dgLocations.Columns[5].Visible = true;

            dgLocations.Visible = true;
            dgLocations.ItemType = "List";
            dgLocations.ItemBgColor = CurrentPortalPage.Setting("ItemBgColor", string.Empty, false);
            dgLocations.ItemAltBgColor = CurrentPortalPage.Setting("ItemAltBgColor", string.Empty, false);
            dgLocations.ItemMouseOverColor = CurrentPortalPage.Setting("ItemMouseOverColor", string.Empty, false);
            dgLocations.AddEnabled = EditEnabled;
            dgLocations.AddImageUrl = "~/images/add_list.gif";
            dgLocations.MoveEnabled = false;
            dgLocations.DeleteEnabled = EditEnabled;
            dgLocations.EditEnabled = EditEnabled;
            dgLocations.MergeEnabled = false;
            dgLocations.MailEnabled = false;
            dgLocations.ExportEnabled = false;
            dgLocations.AllowSorting = true;
            dgLocations.DataSource = DataSource;
            dgLocations.DataBind();

            dgLocations.Visible = true;
            lbAdd.Visible = true;
        }
        //list support functions
        private void dgLocations_DeleteCommand(object sender, DataGridCommandEventArgs e)
        {
            //ListReport.Delete(Int32.Parse(e.Item.Cells[0].Text));
            ShowList();
        }

        private void dgLocations_Rebind(object source, System.EventArgs e)
        {
            ShowList();
        }
        private void dgLocations_AddItem(object sender, System.EventArgs e)
        {


            /*
            if (BuildReportOptionSetting == "0")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", ListReportWizardPageIDSetting));
            else if (BuildReportOptionSetting == "1")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", AdvanceListWizardPageIDSetting));
            */
        }

        private void dgLocations_EditCommand(object source, DataGridCommandEventArgs e)
        {
            /*
            ListReport rpt = new ListReport(Int32.Parse(e.Item.Cells[0].Text));

            Response.Redirect(string.Format("default.aspx?page={0}&reportid={1}",
                rpt.NewFormat ? AdvanceListWizardPageIDSetting : ListReportWizardPageIDSetting,
                e.Item.Cells[0].Text));
            */
        }
        private void dgLocations_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			TextBox tbName = (TextBox)e.Item.FindControl("tbName");
            int location_id = Int32.Parse(e.Item.Cells[0].Text);
            int theReturn = new locationData().UpdateLocation(location_id, tbName.Text);

            dgLocations.ItemUpdated(location_id);
            dgLocations.EditItemIndex = -1;
			ShowList();
		}
        protected void imgBtnAddRpt_Click(object sender, EventArgs e)
        {
            int newLocation = new locationData().AddBlankLocation();
            ShowList();
            /*
            if (ddlSelectReport.SelectedValue.ToString() == "2")
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", AdvanceListWizardPageIDSetting));
            else
                Response.Redirect(string.Format("default.aspx?page={0}&reportid=-1", ListReportWizardPageIDSetting));
            */
        }

        private void dgLocations_ItemCreated(object sender, DataGridItemEventArgs e)
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
            this.dgLocations.AddItem += new AddItemEventHandler(dgLocations_AddItem);
            this.dgLocations.DeleteCommand += new DataGridCommandEventHandler(dgLocations_DeleteCommand);
            this.dgLocations.EditCommand += new DataGridCommandEventHandler(dgLocations_EditCommand);
            this.dgLocations.ItemCommand += new DataGridCommandEventHandler(dgLocations_EditCommand);
            this.dgLocations.ReBind += new DataGridReBindEventHandler(this.dgLocations_Rebind);
            this.dgLocations.ItemDataBound += new DataGridItemEventHandler(dgLocations_ItemDataBound);
            this.dgLocations.ItemCreated += new DataGridItemEventHandler(dgLocations_ItemCreated);
            this.dgLocations.UpdateCommand += new DataGridCommandEventHandler(dgLocations_UpdateCommand);
            //this.ddlReportCategory.SelectedIndexChanged += new EventHandler(ddlReportCategory_SelectedIndexChanged);


            imgBtnAddRpt.Click += new EventHandler(imgBtnAddRpt_Click);
        }

        protected void dgLocations_ItemDataBound(object sender, DataGridItemEventArgs e)
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
                aName.InnerText = row["location_name"].ToString();
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
    public partial class locationData : SqlData{
        //ExecuteSqlDataReader
        public DataTable GetLocationsByParent(int parent = 0){
            ArrayList paramList = new ArrayList();
            if(parent != 0) paramList.Add((object) new SqlParameter("@parent_id", (object) parent));
            try
            {
                return this.ExecuteDataTable("cust_luminate_key_sp_get_locations", paramList);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public int AddBlankLocation(int parent = 0){
            ArrayList paramList = new ArrayList();
            if(parent != 0) paramList.Add((object) new SqlParameter("@parent_id", (object) parent));

            paramList.Add(new SqlParameter("@location_name", "New Location"));

            SqlParameter paramOut = new SqlParameter(); //output paramaters
            paramOut.ParameterName = "@location_id";
            paramOut.Direction = ParameterDirection.Output;
            paramOut.SqlDbType = SqlDbType.Int;
            paramList.Add(paramOut);

            try
            {
                this.ExecuteNonQuery("cust_luminate_key_sp_add_location", paramList);
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

        //cust_luminate_key_sp_update_location
        public int UpdateLocation(int location_id, string location_name){
            ArrayList paramList = new ArrayList();

            paramList.Add(new SqlParameter("@location_id", location_id));
            paramList.Add(new SqlParameter("@location_name", location_name));

            try
            {
                this.ExecuteNonQuery("cust_luminate_key_sp_update_location", paramList);
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
