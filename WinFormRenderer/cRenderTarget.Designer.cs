namespace WinFormRenderer
{
    partial class cRenderTarget
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.renderTarget = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // renderTarget
            // 
            this.renderTarget.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.renderTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderTarget.Location = new System.Drawing.Point(0, 0);
            this.renderTarget.Name = "renderTarget";
            this.renderTarget.Size = new System.Drawing.Size(284, 261);
            this.renderTarget.TabIndex = 0;
            this.renderTarget.TabStop = false;
            // 
            // cRenderTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.renderTarget);
            this.Name = "cRenderTarget";
            this.Size = new System.Drawing.Size(284, 261);
            ((System.ComponentModel.ISupportInitialize)(this.renderTarget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox renderTarget;
    }
}

