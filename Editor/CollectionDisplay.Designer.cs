namespace Editor
{
    partial class CollectionDisplay
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.RenderTarget = new WinFormRenderer.cRenderTarget();
            this.SuspendLayout();
            // 
            // RenderTarget
            // 
            this.RenderTarget.Camera = null;
            this.RenderTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderTarget.Location = new System.Drawing.Point(0, 0);
            this.RenderTarget.Name = "RenderTarget";
            this.RenderTarget.Size = new System.Drawing.Size(987, 626);
            this.RenderTarget.TabIndex = 0;
            // 
            // CollectionDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RenderTarget);
            this.Name = "CollectionDisplay";
            this.Size = new System.Drawing.Size(987, 626);
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormRenderer.cRenderTarget RenderTarget;
    }
}
