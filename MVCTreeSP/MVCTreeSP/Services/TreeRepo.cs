using MVCTreeSP.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVCTreeSP.Services
{
    public class TreeRepo :ITree
    {
        TreeDAL obj = new TreeDAL();
        public DataSet FillData(string id)
        {
            SortedList s = new SortedList();
            s.Add("@NodeId", id);
            return obj.Fill("sp_ShowTree", s);
        }
        public List<Node> Get_Tree(string id)
        {
            DataSet ds = FillData(id);
            List<Node> lst = new List<Node>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Node t = new Node();
                t.NodeId = Convert.ToInt32((dr["NodeId"]));
                t.NodeName = dr["NodeName"].ToString();
                t.ParentNodeId = Convert.ToInt32((dr["ParentNodeId"]));
                t.IsActive = Convert.ToBoolean(dr["IsActive"]);
              //  t.StartDate = System.DateTime.Now;
                t.StartDate = Convert.ToDateTime(dr["StartDate"]);

                lst.Add(t);
            }
            return lst;
        }

        public int Ins_Tree(Node t)
        {
            SortedList s = new SortedList();
            s.Add("@NodeName", t.NodeName); 
            s.Add("@ParentNodeId", t.ParentNodeId); 
            s.Add("@StartDate", System.DateTime.Now); 
            return obj.execute("sp_InsertTree", s);
        }

        public Node Get_SingleTree(string id)
        {
            DataSet ds = FillData(id);
            Node t = new Node();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                t.NodeId = Convert.ToInt32((dr["NodeId"]));
                t.NodeName = dr["NodeName"].ToString();
                t.ParentNodeId = Convert.ToInt32((dr["ParentNodeId"]));
                t.IsActive = Convert.ToBoolean(dr["IsActive"]);
                t.StartDate = Convert.ToDateTime(dr["StartDate"]);

            }

            return t;
        }

        public int Up_Tree(Node t)
        {
            SortedList s = new SortedList();
            s.Add("@NodeId", t.NodeId);
            s.Add("@NodeName", t.NodeName);
            s.Add("@ParentNodeId", t.ParentNodeId);
            s.Add("@StartDate", t.StartDate);


            return obj.execute("sp_UpdateTree", s);

        }

        public int Del_Tree(string Id)
        {
            SortedList s = new SortedList();
            s.Add("@NodeId", Id);
            return obj.execute("sp_DeleteTree", s);
        }
    }
}