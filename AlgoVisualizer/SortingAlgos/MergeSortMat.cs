using AlgoVisualizer.SortingAlgos.Container;
using AlgoVisualizer.UI.Tree;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoVisualizer.SortingAlgos
{
    public partial class MergeSortMat : MaterialForm
    {
        private BaseSortMat sortControl;
        private BarArray container;

        private Replay replay;
        private Task OnGoingReplayTask { get; set; } = null;

        private bool Pause { get; set; } = false;

        private CancellationTokenSource tokenSource;
        private CancellationToken token;

        private TreeVisualization visualization;

        private TreeNode<CircleNode> res = new TreeNode<CircleNode>(new CircleNode(""));
        private Dictionary<int, TreeNode<CircleNode>> visualizationCache = new Dictionary<int, TreeNode<CircleNode>>();

        public MergeSortMat()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            container = new BarArray(20, 101, 500);

            res = TreeHelper.InsertLevelOrder(container.Array, res, 0, visualizationCache);

            sortControl = new BaseSortMat(container) { Dock = DockStyle.Fill };
            this.splitContainerMain.Panel1.Controls.Add(sortControl);

            sortControl.rtbCode.SelectAll();
            sortControl.rtbCode.SelectedRtf = Properties.Resources.merge;

            sortControl.buttonPlay.Click += ButtonPlay_Click;
            sortControl.buttonGenerate.Click += ButtonGenerate_Click;
            sortControl.buttonPause.Click += ButtonPause_Click;

            visualization = new TreeVisualization(res);
            this.splitContainerMain.Panel2.Controls.Add(visualization);
            visualization.Dock = DockStyle.Fill;

            visualization.ArrangeTree();
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            if (replay != null)
            {
                sortControl.buttonPlay.Enabled = true;
                sortControl.buttonPause.Enabled = false;
                Pause = true;
            }
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                ResetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            if (replay == null)
            {
                replay = new Replay(container.GetState().ToArray(), container.Array.Length);
                replay.StartReplay();
                Pause = false;

                Sort(container);

                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;

                OnGoingReplayTask = Task.Run(() =>
                {
                    ReplaySort();
                }, token);
            }
            else
            {
                Pause = false;
            }

            sortControl.buttonPause.Enabled = true;
            sortControl.buttonPlay.Enabled = false;
        }

        public void RunOnUIThread(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void ResetAll()
        {

            this.Controls.Remove(sortControl);
            sortControl.Dispose();
            container = null;
            replay = null;

            try
            {
                tokenSource?.Cancel();
            }
            catch (Exception) { }

            container = new BarArray((int)sortControl.numericUpDownCount.Value, (int)sortControl.numericUpDownMin.Value, (int)sortControl.numericUpDownMax.Value);
            sortControl = new BaseSortMat(container) { Dock = DockStyle.Fill };
            this.splitContainerMain.Panel1.Controls.Add(sortControl);

            sortControl.rtbCode.SelectAll();
            sortControl.rtbCode.SelectedRtf = Properties.Resources.merge;

            visualizationCache.Clear();

            TreeNode<CircleNode> res = new TreeNode<CircleNode>(new CircleNode(""));
            res = TreeHelper.InsertLevelOrder(container.Array, res, 0, visualizationCache);
            visualization.ChangeTree(res);
            visualization.ArrangeTree();

            sortControl.buttonPlay.Click += ButtonPlay_Click;
            sortControl.buttonGenerate.Click += ButtonGenerate_Click;
            sortControl.buttonPause.Click += ButtonPause_Click;
        }

        private void ResetAfterCompletion()
        {
            RunOnUIThread(() =>
            {
                replay = null;
                sortControl.buttonPause.Enabled = sortControl.buttonPlay.Enabled = false;
                MessageBox.Show("Finished");
            });
        }

        private void ReplaySort()
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Task.Delay(sortControl.GetUpdateSpeed()).Wait();
                    if (Pause)
                        continue;
                    else
                    {
                        var state = replay.GetState();
                        if (state == null)
                        {
                            ResetAfterCompletion();
                            break;
                        }

                        for (int i = 0; i < state.Length; i++)
                        {
                            container.Array[i].Value = state[i].Item1;

                            if (state[i].Item2 == 1)
                            {
                                sortControl.RunOnUIThread(() =>
                                {
                                    container.Array[i].Mark();
                                    visualizationCache[i].markNode = true;
                                    visualization.Refresh();
                                });
                            }
                        }

                        Task.Delay(sortControl.GetUpdateSpeed()).Wait();

                        for (int i = 0; i < state.Length; i++)
                        {
                            container.Array[i].Value = state[i].Item1;
                            visualizationCache[i].Data.Text = state[i].Item1.ToString();

                            sortControl.RunOnUIThread(() =>
                            {
                                container.Array[i].UpdateLocation(sortControl.panelContent.HorizontalScroll.Value);
                            });
                        }

                        sortControl.RunOnUIThread(() =>
                        {
                            visualization.Refresh();
                        });

                        Task.Delay(sortControl.GetUpdateSpeed()).Wait();

                        Application.DoEvents();

                        for (int i = 0; i < state.Length; i++)
                        {
                            container.Array[i].Value = state[i].Item1;

                            if (state[i].Item2 == 1)
                            {
                                sortControl.RunOnUIThread(() =>
                                {
                                    container.Array[i].UnMark();
                                    visualizationCache[i].markNode = false;
                                    visualization.Refresh();
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void mergeSort(BarArray container)
        {
            int n = container.Array.Length;
            int curr_size;
            int left_start;

            for (curr_size = 1; curr_size <= n - 1;
                          curr_size = 2 * curr_size)
            {

                for (left_start = 0; left_start < n - 1;
                            left_start += 2 * curr_size)
                {

                    int mid = left_start + curr_size - 1;

                    int right_end = Math.Min(left_start
                                 + 2 * curr_size - 1, n - 1);

                    merge(container, left_start, mid, right_end);
                }
            }
        }

        void merge(BarArray container, int l, int m, int r)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            if (l < 0 || m < 0 || r < 0) return;
            if (n1 < 0 || n2 < 0) return;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (i = 0; i < n1; i++)
                L[i] = container.Array[l + i].Value;
            for (j = 0; j < n2; j++)
                R[j] = container.Array[m + 1 + j].Value;

            i = 0;
            j = 0;
            k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    container.Array[k].Value = L[i];
                    replay.AddState(container.GetState().ToArray(), k);
                    i++;
                }
                else
                {
                    container.Array[k].Value = R[j];
                    replay.AddState(container.GetState().ToArray(), k);
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                container.Array[k].Value = L[i];
                replay.AddState(container.GetState().ToArray(), k);
                i++;
                k++;
            }

            while (j < n2)
            {
                container.Array[k].Value = R[j];
                replay.AddState(container.GetState().ToArray(), k);
                j++;
                k++;
            }
        }
        private void Sort(BarArray container)
        {
            mergeSort(container);
        }

        private void BubbleSortMat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tokenSource?.Cancel();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
