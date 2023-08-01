namespace MinimumSpanningTreeApp
{
    partial class MainForm
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
            this.canvasPanel = new System.Windows.Forms.Panel();
            this.showGraphButton = new System.Windows.Forms.Button();
            this.runPrimButton = new System.Windows.Forms.Button();
            this.runKruskalButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvasPanel.Location = new System.Drawing.Point(12, 12);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(600, 400);
            this.canvasPanel.TabIndex = 0;
            // 
            // showGraphButton
            // 
            this.showGraphButton.Location = new System.Drawing.Point(630, 27);
            this.showGraphButton.Name = "showGraphButton";
            this.showGraphButton.Size = new System.Drawing.Size(150, 30);
            this.showGraphButton.TabIndex = 1;
            this.showGraphButton.Text = "Show Graph";
            this.showGraphButton.UseVisualStyleBackColor = true;
            this.showGraphButton.Click += new System.EventHandler(this.ShowGraphButton_Click);
            // 
            // runPrimButton
            // 
            this.runPrimButton.Location = new System.Drawing.Point(630, 81);
            this.runPrimButton.Name = "runPrimButton";
            this.runPrimButton.Size = new System.Drawing.Size(150, 30);
            this.runPrimButton.TabIndex = 2;
            this.runPrimButton.Text = "Run Prim Algorithm";
            this.runPrimButton.UseVisualStyleBackColor = true;
            this.runPrimButton.Click += new System.EventHandler(this.RunPrimButton_Click);
            // 
            // runKruskalButton
            // 
            this.runKruskalButton.Location = new System.Drawing.Point(630, 135);
            this.runKruskalButton.Name = "runKruskalButton";
            this.runKruskalButton.Size = new System.Drawing.Size(150, 30);
            this.runKruskalButton.TabIndex = 3;
            this.runKruskalButton.Text = "Run Kruskal Algorithm";
            this.runKruskalButton.UseVisualStyleBackColor = true;
            this.runKruskalButton.Click += new System.EventHandler(this.RunKruskalButton_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 430);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(768, 180);
            this.outputTextBox.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 622);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.runKruskalButton);
            this.Controls.Add(this.runPrimButton);
            this.Controls.Add(this.showGraphButton);
            this.Controls.Add(this.canvasPanel);
            this.Name = "MainForm";
            this.Text = "Prim and Kruskal Algorithms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvasPanel;
        private System.Windows.Forms.Button showGraphButton;
        private System.Windows.Forms.Button runPrimButton;
        private System.Windows.Forms.Button runKruskalButton;
        private System.Windows.Forms.TextBox outputTextBox;
    }
}