using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeNode = AlgoVisualizer.UI.Tree.TreeNode<AlgoVisualizer.UI.Tree.CircleNode>;

namespace AlgoVisualizer.UI.Tree
{
    public class TreeVisualization : PictureBox
    {
        private TreeNode<CircleNode> tree;

        public TreeVisualization(TreeNode<CircleNode> tree) : base()
        {
            this.tree = tree;
        }

        public void ChangeTree(TreeNode<CircleNode> tree)
        {
            this.tree = tree;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            tree.DrawTree(pe.Graphics);
        }

        public void ArrangeTree()
        {
            using (Graphics gr = this.CreateGraphics())
            {
                if (tree.Orientation == TreeNode<CircleNode>.Orientations.Horizontal)
                {
                    float xmin = 0, ymin = 0;
                    tree.Arrange(gr, ref xmin, ref ymin);

                    xmin = (this.ClientSize.Width - xmin) / 2;
                    ymin = 10;
                    tree.Arrange(gr, ref xmin, ref ymin);
                }
                else
                {
                    float xmin = tree.Indent;
                    float ymin = xmin;
                    tree.Arrange(gr, ref xmin, ref ymin);
                }
            }

            this.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ArrangeTree();
        }
    }
}
