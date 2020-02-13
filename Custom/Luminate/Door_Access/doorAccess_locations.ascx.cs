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
    }
}
