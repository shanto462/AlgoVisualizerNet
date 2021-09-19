using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisualizer.SortingAlgos.Container
{
    public class Replay
    {
        private List<Tuple<int, int>[]> replay = new List<Tuple<int, int>[]>();
        private int length;
        private int pointer = 0;
        private bool startReplay = false;

        public Replay(int[] arr, int length)
        {
            this.length = length;
            AddState(arr);
        }

        public void AddState(int[] arr)
        {
            Tuple<int, int>[] tuples = new Tuple<int, int>[length];
            for (int i = 0; i < length; i++)
                tuples[i] = new Tuple<int, int>(arr[i], 0);
            replay.Add(tuples);
        }

        public void StartReplay()
        {
            startReplay = true;
        }

        public void ResetReplay()
        {
            startReplay = false;
            pointer = 0;
        }

        public void Prev()
        {
            if (pointer > 0)
            {
                pointer--;
            }
        }

        public void Next()
        {
            if (pointer <= length - 2)
            {
                pointer++;
            }
        }

        public Tuple<int, int>[] GetState()
        {
            if (pointer < 0 || pointer >= replay.Count) return null;
            if (!startReplay) return null;
            return replay[pointer++];
        }

        internal void AddState(int[] arr, int u, int v, int pivot = -1)
        {
            Tuple<int, int>[] tuples = new Tuple<int, int>[length];

            for (int i = 0; i < length; i++)
            {
                tuples[i] = new Tuple<int, int>(arr[i], i == u || i == v ? 1 : 0);
                if (i == pivot)
                {
                    tuples[i] = new Tuple<int, int>(arr[i], 2);
                }
            }
            replay.Add(tuples);
        }

        internal void AddState(int[] arr, int u)
        {
            Tuple<int, int>[] tuples = new Tuple<int, int>[length];
            for (int i = 0; i < length; i++)
                tuples[i] = new Tuple<int, int>(arr[i], i == u ? 1 : 0);
            replay.Add(tuples);
        }
    }
}
