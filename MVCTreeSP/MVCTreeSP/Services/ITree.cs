using MVCTreeSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTreeSP.Services
{
    public interface ITree
    {
        List<Node> Get_Tree(string id);
        int Ins_Tree(Node t);
        int Up_Tree(Node t);
        Node Get_SingleTree(string id);

        int Del_Tree(string Id);
    }
}
