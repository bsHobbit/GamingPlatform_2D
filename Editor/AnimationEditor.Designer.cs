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
            this.RenderTarget = new WinFormRenderer.cRenderTarget();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAnimationManagement = new System.Windows.Forms.TabPage();
            this.buttonAddState = new System.Windows.Forms.Button();
            this.buttonRemoveState = new System.Windows.Forms.Button();
            this.tabPageAnimationAttributes = new System.Windows.Forms.TabPage();
            this.panelAttributes = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonAddAttribute = new System.Windows.Forms.Button();
            this.groupBoxTransitionSettings = new System.Windows.Forms.GroupBox();
            this.comboBoxTransitionAttribute = new System.Windows.Forms.ComboBox();
            this.comboBoxTransitionCondition = new System.Windows.Forms.ComboBox();
            this.numericUpDownTransitionValue = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAnimationManagement.SuspendLayout();
            this.tabPageAnimationAttributes.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBoxTransitionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTransitionValue)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderTarget
            // 
            this.RenderTarget.Camera = null;
            this.RenderTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderTarget.Location = new System.Drawing.Point(203, 3);
            this.RenderTarget.Name = "RenderTarget";
            this.RenderTarget.Size = new System.Drawing.Size(601, 586);
            this.RenderTarget.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.RenderTarget, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 592);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 586);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAnimationManagement);
            this.tabControl1.Controls.Add(this.tabPageAnimationAttributes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(194, 586);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageAnimationManagement
            // 
            this.tabPageAnimationManagement.Controls.Add(this.groupBoxTransitionSettings);
            this.tabPageAnimationManagement.Controls.Add(this.buttonAddState);
            this.tabPageAnimationManagement.Controls.Add(this.buttonRemoveState);
            this.tabPageAnimationManagement.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnimationManagement.Name = "tabPageAnimationManagement";
            this.tabPageAnimationManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnimationManagement.Size = new System.Drawing.Size(186, 560);
            this.tabPageAnimationManagement.TabIndex = 0;
            this.tabPageAnimationManagement.Text = "States";
            this.tabPageAnimationManagement.UseVisualStyleBackColor = true;
            // 
            // buttonAddState
            // 
            this.buttonAddState.Location = new System.Drawing.Point(6, 123);
            this.buttonAddState.Name = "buttonAddState";
            this.buttonAddState.Size = new System.Drawing.Size(171, 23);
            this.buttonAddState.TabIndex = 0;
            this.buttonAddState.Text = "Add State";
            this.buttonAddState.UseVisualStyleBackColor = true;
            this.buttonAddState.Click += new System.EventHandler(this.buttonAddState_Click);
            // 
            // buttonRemoveState
            // 
            this.buttonRemoveState.Location = new System.Drawing.Point(6, 152);
            this.buttonRemoveState.Name = "buttonRemoveState";
            this.buttonRemoveState.Size = new System.Drawing.Size(171, 23);
            this.buttonRemoveState.TabIndex = 1;
            this.buttonRemoveState.Text = "Remove State";
            this.buttonRemoveState.UseVisualStyleBackColor = true;
            this.buttonRemoveState.Click += new System.EventHandler(this.buttonRemoveState_Click);
            // 
            // tabPageAnimationAttributes
            // 
            this.tabPageAnimationAttributes.Controls.Add(this.panelAttributes);
            this.tabPageAnimationAttributes.Controls.Add(this.panel3);
            this.tabPageAnimationAttributes.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnimationAttributes.Name = "tabPageAnimationAttributes";
            this.tabPageAnimationAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnimationAttributes.Size = new System.Drawing.Size(186, 560);
            this.tabPageAnimationAttributes.TabIndex = 1;
            this.tabPageAnimationAttributes.Text = "Attributes";
            this.tabPageAnimationAttributes.UseVisualStyleBackColor = true;
            // 
            // panelAttributes
            // 
            this.panelAttributes.AutoScroll = true;
            this.panelAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttributes.Location = new System.Drawing.Point(3, 3);
            this.panelAttributes.Name = "panelAttributes";
            this.panelAttributes.Size = new System.Drawing.Size(180, 522);
            this.panelAttributes.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonAddAttribute);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 525);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 32);
            this.panel3.TabIndex = 1;
            // 
            // buttonAddAttribute
            // 
            this.buttonAddAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddAttribute.Location = new System.Drawing.Point(0, 0);
            this.buttonAddAttribute.Name = "buttonAddAttribute";
            this.buttonAddAttribute.Size = new System.Drawing.Size(180, 32);
            this.buttonAddAttribute.TabIndex = 0;
            this.buttonAddAttribute.Text = "Add";
            this.buttonAddAttribute.UseVisualStyleBackColor = true;
            this.buttonAddAttribute.Click += new System.EventHandler(this.buttonAddAttribute_Click);
            // 
            // groupBoxTransitionSettings
            // 
            this.groupBoxTransitionSettings.Controls.Add(this.numericUpDownTransitionValue);
            this.groupBoxTransitionSettings.Controls.Add(this.comboBoxTransitionCondition);
            this.groupBoxTransitionSettings.Controls.Add(this.comboBoxTransitionAttribute);
            this.groupBoxTransitionSettings.Enabled = false;
            this.groupBoxTransitionSettings.Location = new System.Drawing.Point(6, 6);
            this.groupBoxTransitionSettings.Name = "groupBoxTransitionSettings";
            this.groupBoxTransitionSettings.Size = new System.Drawing.Size(171, 100);
            this.groupBoxTransitionSettings.TabIndex = 2;
            this.groupBoxTransitionSettings.TabStop = false;
            this.groupBoxTransitionSettings.Text = "Transition";
            // 
            // comboBoxTransitionAttribute
            // 
            this.comboBoxTransitionAttribute.FormattingEnabled = true;
            this.comboBoxTransitionAttribute.Location = new System.Drawing.Point(9, 22);
            this.comboBoxTransitionAttribute.Name = "comboBoxTransitionAttribute";
            this.comboBoxTransitionAttribute.Size = new System.Drawing.Size(156, 21);
            this.comboBoxTransitionAttribute.TabIndex = 1;
            this.comboBoxTransitionAttribute.SelectedIndexChanged += new System.EventHandler(this.comboBoxTransitionAttribute_SelectedIndexChanged);
            // 
            // comboBoxTransitionCondition
            // 
            this.comboBoxTransitionCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTransitionCondition.FormattingEnabled = true;
            this.comboBoxTransitionCondition.Location = new System.Drawing.Point(9, 63);
            this.comboBoxTransitionCondition.Name = "comboBoxTransitionCondition";
            this.comboBoxTransitionCondition.Size = new System.Drawing.Size(90, 21);
            this.comboBoxTransitionCondition.TabIndex = 2;
            this.comboBoxTransitionCondition.SelectedIndexChanged += new System.EventHandler(this.comboBoxTransitionCondition_SelectedIndexChanged);
            // 
            // numericUpDownTransitionValue
            // 
            this.numericUpDownTransitionValue.DecimalPlaces = 1;
            this.numericUpDownTransitionValue.Location = new System.Drawing.Point(105, 64);
            this.numericUpDownTransitionValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownTransitionValue.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownTransitionValue.Name = "numericUpDownTransitionValue";
            this.numericUpDownTransitionValue.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownTransitionValue.TabIndex = 3;
            this.numericUpDownTransitionValue.ValueChanged += new System.EventHandler(this.numericUpDownTransitionValue_ValueChanged);
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 592);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AnimationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnimationEditor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAnimationManagement.ResumeLayout(false);
            this.tabPageAnimationAttributes.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBoxTransitionSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTransitionValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormRenderer.cRenderTarget RenderTarget;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddState;
        private System.Windows.Forms.Button buttonRemoveState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAnimationManagement;
        private System.Windows.Forms.TabPage tabPageAnimationAttributes;
        private System.Windows.Forms.Panel panelAttributes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonAddAttribute;
        private System.Windows.Forms.GroupBox groupBoxTransitionSettings;
        private System.Windows.Forms.NumericUpDown numericUpDownTransitionValue;
        private System.Windows.Forms.ComboBox comboBoxTransitionCondition;
        private System.Windows.Forms.ComboBox comboBoxTransitionAttribute;
    }
}