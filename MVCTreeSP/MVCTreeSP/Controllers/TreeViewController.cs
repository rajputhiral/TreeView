using MVCTreeSP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCTreeSP.Controllers
{

public class TreeViewController : Controller
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\vs\\MVCTreeSP\\MVCTreeSP\\App_Data\\Database2.mdf;Integrated Security=True;Application Name=EntityFramework";

        #region Index View
                public ActionResult Index()
                {
                    var hnodes = GetNodes();
                    var htree = GetTree(hnodes);            
                    return View(htree);
                }
                #endregion

        #region Code for Creating Nod
        /*
         * GetNodes
         * 
         * GetNodes method returns the nodes.
         * it fethches the data from database
         *
         * */
        private List<Node> GetNodes()
        {
            List<Node> nodes = new List<Node>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetActiveNodes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nodes.Add(new Node
                                {
                                    NodeId = reader.GetInt32(0),
                                    NodeName = reader.GetString(1),
                                    ParentNodeId = reader.GetInt32(2),
                                    StartDate = reader.GetDateTime(3),
                                    IsActive = reader.GetBoolean(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error fetching nodes: " + ex.Message);
            }

            return nodes;
        }
        /*
         * GetTree Method takes the node and 
         * arrange it recursively
         * 
         * 
         * */
        public List<Node> GetTree(List<Node> nodes)
        {
            var nodeLookup = nodes.ToLookup(node => node.ParentNodeId.GetValueOrDefault());

            List<Node> GetChildren(int parentId) 
            {
                return nodeLookup[parentId]
                    .Select(node =>
                    {
                        node.Children = GetChildren(node.NodeId); // calling recursively
                        return node;
                    })
                    .ToList();
            }

            return GetChildren(0); 
        }

        #endregion


    }

}
    
