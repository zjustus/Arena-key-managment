namespace ArenaWeb.UserControls.custom.Luminate.DoorAccess{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Arena.Core;
    using Arena.Content;
	using Arena.Core.Communications;
	using Arena.Exceptions;
	using Arena.Portal;
	using Arena.Security;
	using Arena.Utility;
    using Arena.Organization; //?

    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    using Arena.DataLib;
    using Arena.DataLayer;
    using Arena.DataLayer.Security;


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

        DoorAccess.locationData locationDataLayer;
    }


////// DATA LAYER
    public partial class locationData : SqlData{
        public SqlDataReader GetLocationsByParent(int parent = 0)
        {
            ArrayList paramList = new ArrayList();
            if(parent != 0) paramList.Add((object) new SqlParameter("@parent_id", (object) parent));
            try
            {
                return this.ExecuteSqlDataReader("cust_luminate_key_sp_get_locations", paramList);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

    }
}
