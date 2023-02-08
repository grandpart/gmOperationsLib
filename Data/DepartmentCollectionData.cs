using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class DepartmentCollectionData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select EntKey, DepKey, DepName");
            vStringBuilder.AppendLine("from   Department");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject
        public static void DataToObject(Department aDepartment, SqlDataReader aSqlDataReader)
        {
            aDepartment.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aDepartment.DepKey = Convert.ToInt32(aSqlDataReader["DepKey"]);
            aDepartment.DepName = Convert.ToString(aSqlDataReader["DepName"]);
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, DepartmentCollection aDepartmenCollection)
        {
            if (aDepartmenCollection == null)
            {
                throw new ArgumentNullException("aDepartmentCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aDepartmenCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, DepartmentCollection aDepartmentCollection)
        {
            if (aDepartmentCollection == null)
            {
                throw new ArgumentNullException("aDepartmentCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aDepartmentCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, DepartmentCollection aDepartmentCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where EntKey = @EntKey");
            vStringBuilder.AppendLine("order by DepKey");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vDepartment = new Department();
                    DataToObject(vDepartment, vSqlDataReader);
                    aDepartmentCollection.DepartmentList.Add(vDepartment);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
