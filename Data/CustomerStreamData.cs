using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    public class CustomerStreamData
    {

        public static void Load(Connection aConnection, UserKey aUserKey, KeyValueCollection aKeyValueCollection)
        {
            if (aKeyValueCollection == null)
            {
                throw new ArgumentNullException(nameof(aKeyValueCollection));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("select CusKey, CusName");
                vStringBuilder.AppendLine("from Customer");
                vStringBuilder.AppendLine("where EntKey = @EntKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                if (!String.IsNullOrWhiteSpace(aKeyValueCollection.Filter))
                {
                    vStringBuilder.AppendLine("and  CusName like @Filter");
                    vSqlCommand.Parameters.AddWithValue("@Filter", $"%{aKeyValueCollection.Filter}%");
                }
                vStringBuilder.AppendLine("order by CusName");
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var vKeyValue = new KeyValue
                        {
                            Key = Convert.ToInt32(vSqlDataReader["CusKey"]),
                            Value = Convert.ToString(vSqlDataReader["CusName"])
                        };
                        aKeyValueCollection.List.Add(vKeyValue);
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
    }
}


