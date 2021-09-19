using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.SortingAlgos.Container
{
    public class BarButton : Button
    {
        private bool m_BasePaint = false;
        public int Value { get; set; } = -1;
        public int Index { get; private set; } = -1;

        public int Max { get; set; } = 0;

        public override string Text
        {
            get
            {
                if (m_BasePaint)
                    return "";
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public BarButton(int value, int index, int max) : base()
        {
            this.Value = value;
            this.Index = index;
            this.Max = max;

            this.Text = value.ToString();

            this.Enabled = false;

            this.ForeColor = Color.White;
            this.BackColor = Color.Black;

            UpdateLocation(0);
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            m_BasePaint = true;
            base.OnPaint(pevent);
            m_BasePaint = false;

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

            TextRenderer.DrawText(pevent.Graphics,
                Text,
                base.Font,
                ClientRectangle,
                ForeColor,
                flags);
        }

        public void Mark()
        {
            RunOnUIThread(() =>
            {
                this.BackColor = Color.Yellow;
                this.ForeColor = Color.Black;
            });
        }

        public void PMark()
        {
            RunOnUIThread(() =>
            {
                this.BackColor = Color.Red;
                this.ForeColor = Color.Black;
            });
        }

        public void RunOnUIThread(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }


        public void UnMark()
        {
            RunOnUIThread(() =>
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            });
        }

        public void UpdateLocation(int padding)
        {
            this.Location = new Point(Index * (50 + 10) - padding, 0);
            this.Size = new Size(50, Value % (Max + 30) + 10);
            this.Text = Value.ToString();
        }
    }
}
