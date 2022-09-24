using Microsoft.EntityFrameworkCore;
using System.Data;

namespace VacationManagement.Data
{
    public static class GeneralDbFunctions
    {
        public static DataTable SqlDataTable(this DbContext context,string sqlQuery)
        {
            IDbConnection conn = context.Database.GetDbConnection();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType=CommandType.Text;
                cmd.CommandText=sqlQuery;

                var table = new DataTable();
                if (conn.State.Equals(ConnectionState.Closed)) { conn.Open(); }

                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
                if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
                return table;
            }
        }
    }
}
