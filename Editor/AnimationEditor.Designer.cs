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
            this.rendertargetTileset = new WinFormRenderer.cRenderTarget();
            this.SuspendLayout();
            // 
            // rendertargetTileset
            // 
            this.rendertargetTileset.Camera = null;
            this.rendertargetTileset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rendertargetTileset.Location = new System.Drawing.Point(0, 0);
            this.rendertargetTileset.Name = "rendertargetTileset";
            this.rendertargetTileset.Size = new System.Drawing.Size(882, 529);
            this.rendertargetTileset.TabIndex = 0;
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 529);
            this.Controls.Add(this.rendertargetTileset);
            this.Name = "AnimationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Animation Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormRenderer.cRenderTarget rendertargetTileset;
    }
}