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
    public partial class QuickSort : MaterialForm
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

        public QuickSort()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            container = new BarArray(20, 101, 500);

            res = TreeHelper.InsertLevelOrder(container.Array, res, 0, visualizationCache);

            sortControl = new BaseSortMat(container) { Dock = DockStyle.Fill };
            this.splitContainerMain.Panel1.Controls.Add(sortControl);

            sortControl.rtbCode.SelectAll();
            sortControl.rtbCode.SelectedRtf = Properties.Resources.bubble;

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
            sortControl.rtbCode.SelectedRtf = Properties.Resources.bubble;

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
                            else if (state[i].Item2 == 2)
                            {
                                sortControl.RunOnUIThread(() =>
                                {
                                    container.Array[i].PMark();
                                    visualizationCache[i].pMarkNode = true;
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

                            if (state[i].Item2 == 1 || state[i].Item2 == 2)
                            {
                                sortControl.RunOnUIThread(() =>
                                {
                                    container.Array[i].UnMark();
                                    visualizationCache[i].markNode = false;
                                    visualizationCache[i].pMarkNode = false;
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

        int partition(BarArray container,
                         int low, int high)
        {
            int temp;
            int pivot = container.Array[high].Value;

            int i = (low - 1);
            for (int j = low; j <= high - 1; j++)
            {
                if (container.Array[j].Value <= pivot)
                {
                    i++;

                    temp = container.Array[i].Value;
                    container.Array[i].Value = container.Array[j].Value;
                    container.Array[j].Value = temp;

                    replay.AddState(container.GetState().ToArray(), i, j, high);
                }
            }

            temp = container.Array[i + 1].Value;
            container.Array[i + 1].Value = container.Array[high].Value;
            container.Array[high].Value = temp;

            replay.AddState(container.GetState().ToArray(), i + 1, high);

            return i + 1;
        }

        void qSort(BarArray container, int low,
                         int high)
        {
            if (low < high)
            {
                int pi = partition(container, low, high);
                qSort(container, low, pi - 1);
                qSort(container, pi + 1, high);
            }
        }

        private void Sort(BarArray container)
        {
            qSort(container, 0, container.Array.Length - 1);
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
