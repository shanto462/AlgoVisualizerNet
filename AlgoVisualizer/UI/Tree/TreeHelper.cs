using AlgoVisualizer.SortingAlgos.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisualizer.UI.Tree
{
    public static class TreeHelper
    {
        public static TreeNode<CircleNode> InsertLevelOrder(BarButton[] arr, TreeNode<CircleNode> root, int i, Dictionary<int, TreeNode<CircleNode>> visualizationCache)
        {
            if (i < arr.Length)
            {
                TreeNode<CircleNode> temp = new TreeNode<CircleNode>(new CircleNode(arr[i].Value.ToString()));
                visualizationCache.Add(i, temp);

                root = temp;

                var left = new TreeNode<CircleNode>(new CircleNode(""));
                root.AddChild(InsertLevelOrder(arr, left, 2 * i + 1, visualizationCache));

                var right = new TreeNode<CircleNode>(new CircleNode(""));
                root.AddChild(InsertLevelOrder(arr, right, 2 * i + 2, visualizationCache));
            }
            return root;
        }
    }
}
