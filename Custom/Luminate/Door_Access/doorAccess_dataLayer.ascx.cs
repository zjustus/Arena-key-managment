using Arena.Core;
using Arena.DataLib;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ArenaWeb.UserControls.custom.Luminate.DoorAccess
{
    public class doorData : sqlData{

        public SqlDataReader GetLocationsByParent(int parent = null)
        {
            ArrayList paramList = new ArrayList();
            paramList.Add((object) new SqlParameter("@parent_id", (object) parent));
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
