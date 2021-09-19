using AlgoVisualizer.SortingAlgos.Container;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.SortingAlgos
{
    public partial class BaseSortMat : UserControl
    {
        private readonly BarArray container;

        public void RunOnUIThread(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        public BaseSortMat(BarArray container)
        {
            InitializeComponent();

            this.container = container;

            for (int i = 0; i < container.Array.Length; i++)
            {
                this.panelContent.Controls.Add(container.Array[i]);
            }

            panelContent.HorizontalScroll.Enabled = true;
            panelContent.HorizontalScroll.Visible = true;
            panelContent.AutoScroll = true;

            comboBoxSpeed.SelectedIndex = 2;
        }

        public TimeSpan GetUpdateSpeed()
        {
            int index = 0;
            RunOnUIThread(() => index = comboBoxSpeed.SelectedIndex);
            switch (index)
            {
                case 0: return TimeSpan.FromMilliseconds(1000);
                case 1: return TimeSpan.FromMilliseconds(500);
                case 2: return TimeSpan.FromMilliseconds(300);
                case 3: return TimeSpan.FromMilliseconds(100);
                case 4: return TimeSpan.FromMilliseconds(10);
            }
            return TimeSpan.FromMilliseconds(200);
        }
    }
}
