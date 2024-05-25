using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace MVCTreeSP.Services
{
    public class TreeDAL
    {
        private SqlConnection con = new SqlConnection();

        private void connection()
        {
            string s = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\vs\\MVCTreeSP\\MVCTreeSP\\App_Data\\Database2.mdf;Integrated Security=True;Application Name=EntityFramework";
            con = new SqlConnection(s);
            con.Open();
        }
        private void close()
        {

            con.Close();
        }
        public int execute(string spname, SortedList s)
        {
            connection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = spname;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < s.Count; i++)
            {
                cmd.Parameters.AddWithValue((string)s.GetKey(i), s.GetByIndex(i));
            }
            return cmd.ExecuteNonQuery();

        }

        public DataSet Fill(string spname, SortedList s)
        {
            connection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandText = spname;
            cmd.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < s.Count; i++)
            {
                cmd.Parameters.AddWithValue((string)s.GetKey(i), s.GetByIndex(i));
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);

            return ds;
        }

    }
}