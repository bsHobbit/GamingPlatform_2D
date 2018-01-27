namespace Editor
{
    partial class AnimationEditor
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelAnimationName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarFrameSelection = new System.Windows.Forms.TrackBar();
            this.renderTargetCurrentFrame = new WinFormRenderer.cRenderTarget();
            this.renderTargetAnimation = new WinFormRenderer.cRenderTarget();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownGridHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownGridWidth = new System.Windows.Forms.NumericUpDown();
            this.checkBoxReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxLoopAnimation = new System.Windows.Forms.CheckBox();
            this.labelAnimationFPS = new System.Windows.Forms.Label();
            this.numericUpDownAnimationFPS = new System.Windows.Forms.NumericUpDown();
            this.rendertargetTileset = new WinFormRenderer.cRenderTarget();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAutoFrame = new System.Windows.Forms.Button();
            this.buttonResetAnimation = new System.Windows.Forms.Button();
            this.buttonRemoveFrame = new System.Windows.Forms.Button();
            this.buttonMoveFrameBottom = new System.Windows.Forms.Button();
            this.buttonMoveFrameTop = new System.Windows.Forms.Button();
            this.buttonAddFrame = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrameSelection)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnimationFPS)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(44, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // labelAnimationName
            // 
            this.labelAnimationName.AutoSize = true;
            this.labelAnimationName.Location = new System.Drawing.Point(3, 9);
            this.labelAnimationName.Name = "labelAnimationName";
            this.labelAnimationName.Size = new System.Drawing.Size(35, 13);
            this.labelAnimationName.TabIndex = 2;
            this.labelAnimationName.Text = "Name";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rendertargetTileset, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 716);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.renderTargetAnimation, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(173, 419);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(676, 294);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.trackBarFrameSelection, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.renderTargetCurrentFrame, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(332, 288);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // trackBarFrameSelection
            // 
            this.trackBarFrameSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarFrameSelection.Location = new System.Drawing.Point(3, 3);
            this.trackBarFrameSelection.Name = "trackBarFrameSelection";
            this.trackBarFrameSelection.Size = new System.Drawing.Size(326, 24);
            this.trackBarFrameSelection.TabIndex = 3;
            this.trackBarFrameSelection.ValueChanged += new System.EventHandler(this.trackBarFrameSelection_ValueChanged);
            // 
            // renderTargetCurrentFrame
            // 
            this.renderTargetCurrentFrame.Camera = null;
            this.renderTargetCurrentFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderTargetCurrentFrame.Location = new System.Drawing.Point(3, 33);
            this.renderTargetCurrentFrame.Name = "renderTargetCurrentFrame";
            this.renderTargetCurrentFrame.Size = new System.Drawing.Size(326, 252);
            this.renderTargetCurrentFrame.TabIndex = 3;
            // 
            // renderTargetAnimation
            // 
            this.renderTargetAnimation.Camera = null;
            this.renderTargetAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderTargetAnimation.Location = new System.Drawing.Point(341, 3);
            this.renderTargetAnimation.Name = "renderTargetAnimation";
            this.renderTargetAnimation.Size = new System.Drawing.Size(332, 288);
            this.renderTargetAnimation.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxGrid);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDownGridHeight);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDownGridWidth);
            this.panel1.Controls.Add(this.checkBoxReverse);
            this.panel1.Controls.Add(this.checkBoxLoopAnimation);
            this.panel1.Controls.Add(this.labelAnimationFPS);
            this.panel1.Controls.Add(this.numericUpDownAnimationFPS);
            this.panel1.Controls.Add(this.labelAnimationName);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 410);
            this.panel1.TabIndex = 6;
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Checked = true;
            this.checkBoxGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrid.Location = new System.Drawing.Point(6, 129);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(81, 17);
            this.checkBoxGrid.TabIndex = 11;
            this.checkBoxGrid.Text = "Enable Grid";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Grid Height";
            // 
            // numericUpDownGridHeight
            // 
            this.numericUpDownGridHeight.Location = new System.Drawing.Point(78, 173);
            this.numericUpDownGridHeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownGridHeight.Name = "numericUpDownGridHeight";
            this.numericUpDownGridHeight.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownGridHeight.TabIndex = 9;
            this.numericUpDownGridHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Grid Width";
            // 
            // numericUpDownGridWidth
            // 
            this.numericUpDownGridWidth.Location = new System.Drawing.Point(78, 147);
            this.numericUpDownGridWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownGridWidth.Name = "numericUpDownGridWidth";
            this.numericUpDownGridWidth.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownGridWidth.TabIndex = 7;
            this.numericUpDownGridWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // checkBoxReverse
            // 
            this.checkBoxReverse.AutoSize = true;
            this.checkBoxReverse.Location = new System.Drawing.Point(6, 81);
            this.checkBoxReverse.Name = "checkBoxReverse";
            this.checkBoxReverse.Size = new System.Drawing.Size(93, 17);
            this.checkBoxReverse.TabIndex = 6;
            this.checkBoxReverse.Text = "Reverse Loop";
            this.checkBoxReverse.UseVisualStyleBackColor = true;
            // 
            // checkBoxLoopAnimation
            // 
            this.checkBoxLoopAnimation.AutoSize = true;
            this.checkBoxLoopAnimation.Location = new System.Drawing.Point(6, 58);
            this.checkBoxLoopAnimation.Name = "checkBoxLoopAnimation";
            this.checkBoxLoopAnimation.Size = new System.Drawing.Size(99, 17);
            this.checkBoxLoopAnimation.TabIndex = 5;
            this.checkBoxLoopAnimation.Text = "Loop Animation";
            this.checkBoxLoopAnimation.UseVisualStyleBackColor = true;
            // 
            // labelAnimationFPS
            // 
            this.labelAnimationFPS.AutoSize = true;
            this.labelAnimationFPS.Location = new System.Drawing.Point(3, 34);
            this.labelAnimationFPS.Name = "labelAnimationFPS";
            this.labelAnimationFPS.Size = new System.Drawing.Size(27, 13);
            this.labelAnimationFPS.TabIndex = 4;
            this.labelAnimationFPS.Text = "FPS";
            // 
            // numericUpDownAnimationFPS
            // 
            this.numericUpDownAnimationFPS.Location = new System.Drawing.Point(78, 32);
            this.numericUpDownAnimationFPS.Name = "numericUpDownAnimationFPS";
            this.numericUpDownAnimationFPS.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownAnimationFPS.TabIndex = 3;
            // 
            // rendertargetTileset
            // 
            this.rendertargetTileset.Camera = null;
            this.rendertargetTileset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rendertargetTileset.Location = new System.Drawing.Point(173, 3);
            this.rendertargetTileset.Name = "rendertargetTileset";
            this.rendertargetTileset.Size = new System.Drawing.Size(676, 410);
            this.rendertargetTileset.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonAutoFrame);
            this.panel2.Controls.Add(this.buttonResetAnimation);
            this.panel2.Controls.Add(this.buttonRemoveFrame);
            this.panel2.Controls.Add(this.buttonMoveFrameBottom);
            this.panel2.Controls.Add(this.buttonMoveFrameTop);
            this.panel2.Controls.Add(this.buttonAddFrame);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 419);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 294);
            this.panel2.TabIndex = 8;
            // 
            // buttonAutoFrame
            // 
            this.buttonAutoFrame.Location = new System.Drawing.Point(9, 217);
            this.buttonAutoFrame.Name = "buttonAutoFrame";
            this.buttonAutoFrame.Size = new System.Drawing.Size(144, 23);
            this.buttonAutoFrame.TabIndex = 5;
            this.buttonAutoFrame.Text = "Auto Frame Selection";
            this.buttonAutoFrame.UseVisualStyleBackColor = true;
            this.buttonAutoFrame.Click += new System.EventHandler(this.buttonAutoFrame_Click);
            // 
            // buttonResetAnimation
            // 
            this.buttonResetAnimation.Location = new System.Drawing.Point(9, 262);
            this.buttonResetAnimation.Name = "buttonResetAnimation";
            this.buttonResetAnimation.Size = new System.Drawing.Size(144, 23);
            this.buttonResetAnimation.TabIndex = 4;
            this.buttonResetAnimation.Text = "Reset Animation";
            this.buttonResetAnimation.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveFrame
            // 
            this.buttonRemoveFrame.Location = new System.Drawing.Point(9, 94);
            this.buttonRemoveFrame.Name = "buttonRemoveFrame";
            this.buttonRemoveFrame.Size = new System.Drawing.Size(144, 23);
            this.buttonRemoveFrame.TabIndex = 3;
            this.buttonRemoveFrame.Text = "Remove Frame";
            this.buttonRemoveFrame.UseVisualStyleBackColor = true;
            this.buttonRemoveFrame.Click += new System.EventHandler(this.buttonRemoveFrame_Click);
            // 
            // buttonMoveFrameBottom
            // 
            this.buttonMoveFrameBottom.Location = new System.Drawing.Point(9, 65);
            this.buttonMoveFrameBottom.Name = "buttonMoveFrameBottom";
            this.buttonMoveFrameBottom.Size = new System.Drawing.Size(144, 23);
            this.buttonMoveFrameBottom.TabIndex = 2;
            this.buttonMoveFrameBottom.Text = "Move Frame (->)";
            this.buttonMoveFrameBottom.UseVisualStyleBackColor = true;
            this.buttonMoveFrameBottom.Click += new System.EventHandler(this.buttonMoveFrameBottom_Click);
            // 
            // buttonMoveFrameTop
            // 
            this.buttonMoveFrameTop.Location = new System.Drawing.Point(9, 36);
            this.buttonMoveFrameTop.Name = "buttonMoveFrameTop";
            this.buttonMoveFrameTop.Size = new System.Drawing.Size(144, 23);
            this.buttonMoveFrameTop.TabIndex = 1;
            this.buttonMoveFrameTop.Text = "Move Frame (<-)";
            this.buttonMoveFrameTop.UseVisualStyleBackColor = true;
            this.buttonMoveFrameTop.Click += new System.EventHandler(this.buttonMoveFrameTop_Click);
            // 
            // buttonAddFrame
            // 
            this.buttonAddFrame.Location = new System.Drawing.Point(9, 7);
            this.buttonAddFrame.Name = "buttonAddFrame";
            this.buttonAddFrame.Size = new System.Drawing.Size(144, 23);
            this.buttonAddFrame.TabIndex = 0;
            this.buttonAddFrame.Text = "Add Frame";
            this.buttonAddFrame.UseVisualStyleBackColor = true;
            this.buttonAddFrame.Click += new System.EventHandler(this.buttonAddFrame_Click);
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 716);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AnimationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Animation Editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrameSelection)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAnimationFPS)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormRenderer.cRenderTarget rendertargetTileset;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelAnimationName;
        private WinFormRenderer.cRenderTarget renderTargetCurrentFrame;
        private WinFormRenderer.cRenderTarget renderTargetAnimation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar trackBarFrameSelection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxReverse;
        private System.Windows.Forms.CheckBox checkBoxLoopAnimation;
        private System.Windows.Forms.Label labelAnimationFPS;
        private System.Windows.Forms.NumericUpDown numericUpDownAnimationFPS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonResetAnimation;
        private System.Windows.Forms.Button buttonRemoveFrame;
        private System.Windows.Forms.Button buttonMoveFrameBottom;
        private System.Windows.Forms.Button buttonMoveFrameTop;
        private System.Windows.Forms.Button buttonAddFrame;
        private System.Windows.Forms.Button buttonAutoFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownGridHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownGridWidth;
        private System.Windows.Forms.CheckBox checkBoxGrid;
    }
}