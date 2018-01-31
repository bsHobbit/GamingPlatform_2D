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
            this.collectionDisplayTextures = new Editor.CollectionDisplay();
            this.SuspendLayout();
            // 
            // collectionDisplayTextures
            // 
            this.collectionDisplayTextures.Filter = null;
            this.collectionDisplayTextures.Location = new System.Drawing.Point(300, 138);
            this.collectionDisplayTextures.Name = "collectionDisplayTextures";
            this.collectionDisplayTextures.Size = new System.Drawing.Size(616, 419);
            this.collectionDisplayTextures.TabIndex = 0;
            // 
            // ContentBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 744);
            this.Controls.Add(this.collectionDisplayTextures);
            this.Name = "ContentBrowser";
            this.Text = "Content Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private CollectionDisplay collectionDisplayTextures;
    }
}