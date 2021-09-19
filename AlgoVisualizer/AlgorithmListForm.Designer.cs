
namespace AlgoVisualizer
{
    partial class AlgorithmListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MaterialSkin.MaterialListBoxItem materialListBoxItem1 = new MaterialSkin.MaterialListBoxItem();
            MaterialSkin.MaterialListBoxItem materialListBoxItem2 = new MaterialSkin.MaterialListBoxItem();
            MaterialSkin.MaterialListBoxItem materialListBoxItem3 = new MaterialSkin.MaterialListBoxItem();
            MaterialSkin.MaterialListBoxItem materialListBoxItem4 = new MaterialSkin.MaterialListBoxItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.materialButtonViewSort = new MaterialSkin.Controls.MaterialButton();
            this.materialListBoxSort = new MaterialSkin.Controls.MaterialListBox();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1136, 571);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Number Theory";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1136, 571);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "String";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1136, 571);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DP";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1136, 571);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graph";
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialTabSelector1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTabSelector1.Location = new System.Drawing.Point(3, 64);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(1144, 48);
            this.materialTabSelector1.TabIndex = 20;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tabPage3);
            this.materialTabControl1.Controls.Add(this.tabPage4);
            this.materialTabControl1.Controls.Add(this.tabPage5);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 112);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1144, 597);
            this.materialTabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.materialButtonViewSort);
            this.tabPage1.Controls.Add(this.materialListBoxSort);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1136, 571);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sorting";
            // 
            // materialButtonViewSort
            // 
            this.materialButtonViewSort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButtonViewSort.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButtonViewSort.Depth = 0;
            this.materialButtonViewSort.HighEmphasis = true;
            this.materialButtonViewSort.Icon = null;
            this.materialButtonViewSort.Location = new System.Drawing.Point(484, 34);
            this.materialButtonViewSort.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButtonViewSort.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButtonViewSort.Name = "materialButtonViewSort";
            this.materialButtonViewSort.Size = new System.Drawing.Size(64, 36);
            this.materialButtonViewSort.TabIndex = 1;
            this.materialButtonViewSort.Text = "View";
            this.materialButtonViewSort.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButtonViewSort.UseAccentColor = false;
            this.materialButtonViewSort.UseVisualStyleBackColor = true;
            this.materialButtonViewSort.Click += new System.EventHandler(this.materialButtonViewSort_Click);
            // 
            // materialListBoxSort
            // 
            this.materialListBoxSort.BackColor = System.Drawing.Color.White;
            this.materialListBoxSort.BorderColor = System.Drawing.Color.LightGray;
            this.materialListBoxSort.Depth = 0;
            this.materialListBoxSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            materialListBoxItem1.SecondaryText = "";
            materialListBoxItem1.Tag = null;
            materialListBoxItem1.Text = "Bubble Sort";
            materialListBoxItem2.SecondaryText = "";
            materialListBoxItem2.Tag = null;
            materialListBoxItem2.Text = "Heap Sort";
            materialListBoxItem3.SecondaryText = "";
            materialListBoxItem3.Tag = null;
            materialListBoxItem3.Text = "Merge Sort";
            materialListBoxItem4.SecondaryText = "";
            materialListBoxItem4.Tag = null;
            materialListBoxItem4.Text = "Quick Sort";
            this.materialListBoxSort.Items.Add(materialListBoxItem1);
            this.materialListBoxSort.Items.Add(materialListBoxItem2);
            this.materialListBoxSort.Items.Add(materialListBoxItem3);
            this.materialListBoxSort.Items.Add(materialListBoxItem4);
            this.materialListBoxSort.Location = new System.Drawing.Point(50, 34);
            this.materialListBoxSort.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialListBoxSort.Name = "materialListBoxSort";
            this.materialListBoxSort.SelectedIndex = -1;
            this.materialListBoxSort.SelectedItem = null;
            this.materialListBoxSort.Size = new System.Drawing.Size(343, 489);
            this.materialListBoxSort.TabIndex = 0;
            // 
            // AlgorithmListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 712);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.Name = "AlgorithmListForm";
            this.Text = "Algorithm Visualization";
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialListBox materialListBoxSort;
        private MaterialSkin.Controls.MaterialButton materialButtonViewSort;
    }
}