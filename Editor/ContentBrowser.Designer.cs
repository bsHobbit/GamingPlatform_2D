namespace Editor
{
    partial class ContentBrowser
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
            this.tabControlContent = new System.Windows.Forms.TabControl();
            this.tabPageTextures = new System.Windows.Forms.TabPage();
            this.tabPageAnimations = new System.Windows.Forms.TabPage();
            this.collectionDisplayTextures = new Editor.CollectionDisplay();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlContent.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlContent
            // 
            this.tabControlContent.Controls.Add(this.tabPageTextures);
            this.tabControlContent.Controls.Add(this.tabPageAnimations);
            this.tabControlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlContent.Location = new System.Drawing.Point(0, 0);
            this.tabControlContent.Name = "tabControlContent";
            this.tabControlContent.SelectedIndex = 0;
            this.tabControlContent.Size = new System.Drawing.Size(1176, 744);
            this.tabControlContent.TabIndex = 1;
            // 
            // tabPageTextures
            // 
            this.tabPageTextures.Controls.Add(this.tableLayoutPanel1);
            this.tabPageTextures.Location = new System.Drawing.Point(4, 22);
            this.tabPageTextures.Name = "tabPageTextures";
            this.tabPageTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextures.Size = new System.Drawing.Size(1168, 718);
            this.tabPageTextures.TabIndex = 0;
            this.tabPageTextures.Text = "Textures";
            this.tabPageTextures.UseVisualStyleBackColor = true;
            // 
            // tabPageAnimations
            // 
            this.tabPageAnimations.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnimations.Name = "tabPageAnimations";
            this.tabPageAnimations.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnimations.Size = new System.Drawing.Size(1162, 682);
            this.tabPageAnimations.TabIndex = 1;
            this.tabPageAnimations.Text = "Animations";
            this.tabPageAnimations.UseVisualStyleBackColor = true;
            // 
            // collectionDisplayTextures
            // 
            this.collectionDisplayTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionDisplayTextures.Filter = null;
            this.collectionDisplayTextures.Location = new System.Drawing.Point(203, 3);
            this.collectionDisplayTextures.Name = "collectionDisplayTextures";
            this.collectionDisplayTextures.Size = new System.Drawing.Size(956, 706);
            this.collectionDisplayTextures.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.collectionDisplayTextures, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1162, 712);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 706);
            this.panel1.TabIndex = 1;
            // 
            // ContentBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 744);
            this.Controls.Add(this.tabControlContent);
            this.Name = "ContentBrowser";
            this.Text = "Content Manager";
            this.tabControlContent.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CollectionDisplay collectionDisplayTextures;
        private System.Windows.Forms.TabControl tabControlContent;
        private System.Windows.Forms.TabPage tabPageTextures;
        private System.Windows.Forms.TabPage tabPageAnimations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}