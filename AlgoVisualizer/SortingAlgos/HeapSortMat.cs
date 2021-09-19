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
    public partial class HeapSortMat : MaterialForm
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

        public HeapSortMat()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            container = new BarArray(20, 101, 500);

            res = TreeHelper.InsertLevelOrder(container.Array, res, 0, visualizationCache);

            sortControl = new BaseSortMat(container) { Dock = DockStyle.Fill };
            this.splitContainerMain.Panel1.Controls.Add(sortControl);

            sortControl.rtbCode.SelectAll();
            sortControl.rtbCode.SelectedRtf = Properties.Resources.heap;

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
            sortControl.rtbCode.SelectedRtf = Properties.Resources.heap;

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

        private void Sort(BarArray container)
        {
            int n = container.Array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(container, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                var temp = container.Array[0].Value;
                container.Array[0].Value = container.Array[i].Value;
                container.Array[i].Value = temp;

                replay.AddState(container.GetState().ToArray(), 0, i);

                Heapify(container, i, 0);
            }
        }


        private void Heapify(BarArray container, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && container.Array[l].Value > container.Array[largest].Value)
                largest = l;

            if (r < n && container.Array[r].Value > container.Array[largest].Value)
                largest = r;

            if (largest != i)
            {
                var swap = container.Array[i].Value;
                container.Array[i].Value = container.Array[largest].Value;
                container.Array[largest].Value = swap;

                replay.AddState(container.GetState().ToArray(), i, largest);

                Heapify(container, n, largest);
            }
        }

        private void HeapSortMat_FormClosing(object sender, FormClosingEventArgs e)
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
