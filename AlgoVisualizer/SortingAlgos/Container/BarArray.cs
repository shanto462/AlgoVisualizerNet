using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.SortingAlgos.Container
{
    public class BarArray
    {
        public BarButton[] Array { get; private set; }
        private readonly Random rand = new Random(Guid.NewGuid().GetHashCode());

        public BarArray(int size, int min, int max)
        {
            Array = new BarButton[size];

            for (int i = 0; i < size; i++)
            {
                Array[i] = new BarButton(rand.Next(min, max + 1), i, max);
            }
        }

        public void UnMarkAll()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i].UnMark();
            }
        }

        public void MarkAll()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i].Mark();
            }
        }

        public IEnumerable<int> GetState()
        {
            foreach (var arr in Array)
            {
                yield return arr.Value;
            }
        }
    }
}
