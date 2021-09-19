using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisualizer.UI.Tree
{
    public class TreeNode<T> where T : IDrawable
    {
        public T Data;

        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        public float HOffset = 5;
        public float VOffset = 10;

        public float Indent = 20;
        public float SpotRadius = 5;

        public enum Orientations
        {
            Horizontal,
            Vertical
        }
        public Orientations Orientation = Orientations.Horizontal;

        private PointF DataCenter;

        private PointF SpotCenter;

        public Font MyFont = null;
        public Pen MyPen = Pens.Black;
        public Brush FontBrush = Brushes.Black;
        public Brush BgBrush = Brushes.White;
        public Brush BgBrushMarked = Brushes.Yellow;
        public Brush PBgBrushMarked = Brushes.Red;

        public bool markNode = false;
        public bool pMarkNode = false;

        public void SetTreeDrawingParameters(float h_offset, float v_offset, float indent, float node_radius, Orientations orientation)
        {
            HOffset = h_offset;
            VOffset = v_offset;
            Indent = indent;
            SpotRadius = node_radius;
            Orientation = orientation;

            foreach (TreeNode<T> child in Children)
                child.SetTreeDrawingParameters(h_offset, v_offset,
                    indent, node_radius, orientation);
        }

        public TreeNode(T new_data)
            : this(new_data, new Font("Times New Roman", 12))
        {
            Data = new_data;
        }
        public TreeNode(T new_data, Font fg_font)
        {
            Data = new_data;
            MyFont = fg_font;
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        public void Arrange(Graphics gr, ref float xmin, ref float ymin)
        {
            if (Orientation == TreeNode<T>.Orientations.Horizontal)
            {
                ArrangeHorizontally(gr, ref xmin, ref ymin);
            }
            else
            {
                ArrangeVertically(gr, xmin, ref ymin);
            }
        }

        public void ArrangeHorizontally(Graphics gr, ref float xmin, ref float ymin)
        {
            SizeF my_size = Data.GetSize(gr, MyFont);

            float x = xmin;
            float biggest_ymin = ymin + my_size.Height;
            float subtree_ymin = ymin + my_size.Height + VOffset;
            foreach (TreeNode<T> child in Children)
            {
                float child_ymin = subtree_ymin;
                child.Arrange(gr, ref x, ref child_ymin);

                if (biggest_ymin < child_ymin) biggest_ymin = child_ymin;

                x += HOffset;
            }

            if (Children.Count > 0) x -= HOffset;
            float subtree_width = x - xmin;
            if (my_size.Width > subtree_width)
            {
                x = xmin + (my_size.Width - subtree_width) / 2;
                foreach (TreeNode<T> child in Children)
                {
                    child.Arrange(gr, ref x, ref subtree_ymin);
                    x += HOffset;
                }
                subtree_width = my_size.Width;
            }

            DataCenter = new PointF(
                xmin + subtree_width / 2,
                ymin + my_size.Height / 2);

            xmin += subtree_width;

            ymin = biggest_ymin;
        }

        public void ArrangeVertically(Graphics gr, float xmin, ref float ymin)
        {
            SizeF my_size = Data.GetSize(gr, MyFont);
            my_size.Width += 3 * SpotRadius;

            SpotCenter = new PointF(
                xmin + SpotRadius,
                ymin + (my_size.Height - 2 * SpotRadius) / 2);

            DataCenter = new PointF(
                SpotCenter.X + SpotRadius + my_size.Width / 2,
                SpotCenter.Y);

            ymin += my_size.Height + VOffset;

            foreach (TreeNode<T> child in Children)
            {
                child.ArrangeVertically(gr, xmin + Indent, ref ymin);
            }
        }

        public void DrawTree(Graphics gr, ref float x, float y)
        {
            Arrange(gr, ref x, ref y);
            DrawTree(gr);
        }

        public void DrawTree(Graphics gr)
        {
            if (Children.Count <= 0) return;
            DrawSubtreeLinks(gr);
            DrawSubtreeNodes(gr);
        }

        private void DrawSubtreeLinks(Graphics gr)
        {
            if (Orientation == TreeNode<T>.Orientations.Horizontal)
            {
                DrawSubtreeLinksHorizontal(gr);
            }
            else
            {
                DrawSubtreeLinksVertical(gr);
            }
        }

        private void DrawSubtreeLinksHorizontal(Graphics gr)
        {
            if (Children.Count <= 0) return;
            foreach (TreeNode<T> child in Children)
            {
                if (child.Children.Count <= 0) return;
                gr.DrawLine(MyPen, DataCenter, child.DataCenter);
                child.DrawSubtreeLinksHorizontal(gr);
            }
        }

        private void DrawSubtreeLinksVertical(Graphics gr)
        {
            if (Children.Count <= 0) return;
            foreach (TreeNode<T> child in Children)
            {
                if (child.Children.Count <= 0) return;
                gr.DrawLine(MyPen, SpotCenter.X, SpotCenter.Y, SpotCenter.X, child.SpotCenter.Y);
                gr.DrawLine(MyPen, SpotCenter.X, child.SpotCenter.Y, child.SpotCenter.X, child.SpotCenter.Y);
                child.DrawSubtreeLinksVertical(gr);
            }
        }

        private void DrawSubtreeNodes(Graphics gr)
        {
            if (Children.Count <= 0) return;

            if (pMarkNode)
                Data.Draw(DataCenter.X, DataCenter.Y, gr, MyPen, PBgBrushMarked, FontBrush, MyFont);
            else if (markNode)
                Data.Draw(DataCenter.X, DataCenter.Y, gr, MyPen, BgBrushMarked, FontBrush, MyFont);
            else
                Data.Draw(DataCenter.X, DataCenter.Y, gr, MyPen, BgBrush, FontBrush, MyFont);

            if (Orientation == TreeNode<T>.Orientations.Vertical)
            {
                RectangleF rect = new RectangleF(
                    SpotCenter.X - SpotRadius, SpotCenter.Y - SpotRadius,
                    2 * SpotRadius, 2 * SpotRadius);
                if (Children.Count > 0)
                {
                    gr.FillEllipse(Brushes.LightBlue, rect);
                }
                else
                {
                    gr.FillEllipse(Brushes.Orange, rect);
                }
                gr.DrawEllipse(MyPen, rect);
            }
            foreach (TreeNode<T> child in Children)
            {
                child.DrawSubtreeNodes(gr);
            }
        }

        public TreeNode<T> NodeAtPoint(Graphics gr, PointF target_pt)
        {
            if (Data.IsAtPoint(gr, MyFont, DataCenter, target_pt)) return this;

            foreach (TreeNode<T> child in Children)
            {
                TreeNode<T> hit_node = child.NodeAtPoint(gr, target_pt);
                if (hit_node != null) return hit_node;
            }

            return null;
        }

        public bool DeleteNode(TreeNode<T> target)
        {
            foreach (TreeNode<T> child in Children)
            {
                if (child == target)
                {
                    Children.Remove(child);
                    return true;
                }
                if (child.DeleteNode(target)) return true;
            }
            return false;
        }
    }
}
