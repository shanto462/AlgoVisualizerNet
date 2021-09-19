using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer
{
    public class SmoothAutoscrollPanel : Panel

    {

        protected override void OnScroll(ScrollEventArgs se)

        {

            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)

            {

                this.VerticalScroll.Value = se.NewValue;

            }

            else if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)

            {

                this.HorizontalScroll.Value = se.NewValue;

            }

        }

    }
}
