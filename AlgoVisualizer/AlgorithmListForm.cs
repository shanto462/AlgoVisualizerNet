using AlgoVisualizer.SortingAlgos;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer
{
    public partial class AlgorithmListForm : MaterialForm
    {
        public AlgorithmListForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
        }

        private void materialButtonViewSort_Click(object sender, EventArgs e)
        {
            var selected = materialListBoxSort.SelectedIndex;
            switch (selected)
            {
                case 0:
                    BubbleSortMat bubbleSort = new BubbleSortMat();
                    bubbleSort.Show();
                    break;
                case 1:
                    HeapSortMat heapSort = new HeapSortMat();
                    heapSort.Show();
                    break;
                case 2:
                    MergeSortMat mergeSort = new MergeSortMat();
                    mergeSort.Show();
                    break;
                case 3:
                    QuickSort quickSort = new QuickSort();
                    quickSort.Show();
                    break;
            }
        }
    }
}
