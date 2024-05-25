using MVCTreeSP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTreeSP.Models;

namespace MVCTreeSP.Controllers
{
    public class TreeCrudController : Controller
    {
        ITree obj=new TreeRepo();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ShowTree()
        {
            Node ts = new Node();
            List<Node> lst = obj.Get_Tree("-1");
            return View(lst);
        }

        [HttpPost]
        public ActionResult ShowTree(Node t)
        {
            obj.Ins_Tree(t);
            List<Node> lst = obj.Get_Tree("-1");
            return View(lst);
        }

        //update
        public ActionResult EditTree(String NodeId)
        {
            Node e = obj.Get_SingleTree(NodeId);
            return View(e);
        }

        [HttpPost]
        public ActionResult EditTree(Node e)
        {
            obj.Up_Tree(e);
            return RedirectToAction("ShowTree");
        }

        public ActionResult DeleteTree(string NodeId)
        {
            obj.Del_Tree(NodeId);
            return RedirectToAction("ShowTree");
        }

        //[HttpGet]
        //public ActionResult TreeView()
        //{
        //    List<TreeStruct> all = new List<TreeStruct>();
        //    using(NodesEntities1 d=new NodesEntities1())
        //    {
        //        all=d.TreeStructs.OrderBy(a=>a.PNId).ToList();  
        //    }
        //    return View(all);
        //}
    }
}