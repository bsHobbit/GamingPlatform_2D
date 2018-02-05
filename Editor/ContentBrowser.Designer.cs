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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.collectionDisplayTextures = new Editor.CollectionDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRemoveTexture = new System.Windows.Forms.Button();
            this.tabPageTilesetAnimations = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.collectionDisplayTilesetAnimations = new Editor.CollectionDisplay();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAddTilesetAnimation = new System.Windows.Forms.Button();
            this.tabPageAnimations = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.collectionDisplayAnimations = new Editor.CollectionDisplay();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonAddAnimation = new System.Windows.Forms.Button();
            this.tabControlContent.SuspendLayout();
            this.tabPageTextures.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageTilesetAnimations.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageAnimations.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlContent
            // 
            this.tabControlContent.Controls.Add(this.tabPageTextures);
            this.tabControlContent.Controls.Add(this.tabPageTilesetAnimations);
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
            // collectionDisplayTextures
            // 
            this.collectionDisplayTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionDisplayTextures.Filter = null;
            this.collectionDisplayTextures.Location = new System.Drawing.Point(203, 3);
            this.collectionDisplayTextures.Name = "collectionDisplayTextures";
            this.collectionDisplayTextures.Size = new System.Drawing.Size(956, 706);
            this.collectionDisplayTextures.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonRemoveTexture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 706);
            this.panel1.TabIndex = 1;
            // 
            // buttonRemoveTexture
            // 
            this.buttonRemoveTexture.Location = new System.Drawing.Point(19, 20);
            this.buttonRemoveTexture.Name = "buttonRemoveTexture";
            this.buttonRemoveTexture.Size = new System.Drawing.Size(161, 23);
            this.buttonRemoveTexture.TabIndex = 0;
            this.buttonRemoveTexture.Text = "Remove";
            this.buttonRemoveTexture.UseVisualStyleBackColor = true;
            this.buttonRemoveTexture.Click += new System.EventHandler(this.buttonRemoveTexture_Click);
            // 
            // tabPageTilesetAnimations
            // 
            this.tabPageTilesetAnimations.Controls.Add(this.tableLayoutPanel2);
            this.tabPageTilesetAnimations.Location = new System.Drawing.Point(4, 22);
            this.tabPageTilesetAnimations.Name = "tabPageTilesetAnimations";
            this.tabPageTilesetAnimations.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTilesetAnimations.Size = new System.Drawing.Size(1168, 718);
            this.tabPageTilesetAnimations.TabIndex = 1;
            this.tabPageTilesetAnimations.Text = "Tileset-Animations";
            this.tabPageTilesetAnimations.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.collectionDisplayTilesetAnimations, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1162, 712);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // collectionDisplayTilesetAnimations
            // 
            this.collectionDisplayTilesetAnimations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionDisplayTilesetAnimations.Filter = null;
            this.collectionDisplayTilesetAnimations.Location = new System.Drawing.Point(203, 3);
            this.collectionDisplayTilesetAnimations.Name = "collectionDisplayTilesetAnimations";
            this.collectionDisplayTilesetAnimations.Size = new System.Drawing.Size(956, 706);
            this.collectionDisplayTilesetAnimations.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonAddTilesetAnimation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 706);
            this.panel2.TabIndex = 1;
            // 
            // buttonAddTilesetAnimation
            // 
            this.buttonAddTilesetAnimation.Location = new System.Drawing.Point(12, 15);
            this.buttonAddTilesetAnimation.Name = "buttonAddTilesetAnimation";
            this.buttonAddTilesetAnimation.Size = new System.Drawing.Size(169, 23);
            this.buttonAddTilesetAnimation.TabIndex = 0;
            this.buttonAddTilesetAnimation.Text = "Add";
            this.buttonAddTilesetAnimation.UseVisualStyleBackColor = true;
            // 
            // tabPageAnimations
            // 
            this.tabPageAnimations.Controls.Add(this.tableLayoutPanel3);
            this.tabPageAnimations.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnimations.Name = "tabPageAnimations";
            this.tabPageAnimations.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnimations.Size = new System.Drawing.Size(1168, 718);
            this.tabPageAnimations.TabIndex = 2;
            this.tabPageAnimations.Text = "Animations";
            this.tabPageAnimations.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.collectionDisplayAnimations, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1162, 712);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // collectionDisplayAnimations
            // 
            this.collectionDisplayAnimations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionDisplayAnimations.Filter = null;
            this.collectionDisplayAnimations.Location = new System.Drawing.Point(203, 3);
            this.collectionDisplayAnimations.Name = "collectionDisplayAnimations";
            this.collectionDisplayAnimations.Size = new System.Drawing.Size(956, 706);
            this.collectionDisplayAnimations.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonAddAnimation);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 706);
            this.panel3.TabIndex = 1;
            // 
            // buttonAddAnimation
            // 
            this.buttonAddAnimation.Location = new System.Drawing.Point(12, 15);
            this.buttonAddAnimation.Name = "buttonAddAnimation";
            this.buttonAddAnimation.Size = new System.Drawing.Size(169, 23);
            this.buttonAddAnimation.TabIndex = 0;
            this.buttonAddAnimation.Text = "Add";
            this.buttonAddAnimation.UseVisualStyleBackColor = true;
            // 
            // ContentBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 744);
            this.Controls.Add(this.tabControlContent);
            this.Name = "ContentBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Content Manager";
            this.tabControlContent.ResumeLayout(false);
            this.tabPageTextures.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPageTilesetAnimations.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPageAnimations.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CollectionDisplay collectionDisplayTextures;
        private System.Windows.Forms.TabControl tabControlContent;
        private System.Windows.Forms.TabPage tabPageTextures;
        private System.Windows.Forms.TabPage tabPageTilesetAnimations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CollectionDisplay collectionDisplayTilesetAnimations;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonAddTilesetAnimation;
        private System.Windows.Forms.Button buttonRemoveTexture;
        private System.Windows.Forms.TabPage tabPageAnimations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CollectionDisplay collectionDisplayAnimations;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonAddAnimation;
    }
}