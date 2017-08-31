namespace TestClient
{
	partial class Form1
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
			this.tbAddr = new System.Windows.Forms.TextBox();
			this.tbNumber = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.tbMsgLen = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbAddr
			// 
			this.tbAddr.Location = new System.Drawing.Point(631, 12);
			this.tbAddr.Name = "tbAddr";
			this.tbAddr.Size = new System.Drawing.Size(263, 22);
			this.tbAddr.TabIndex = 0;
			// 
			// tbNumber
			// 
			this.tbNumber.Location = new System.Drawing.Point(155, 76);
			this.tbNumber.Name = "tbNumber";
			this.tbNumber.Size = new System.Drawing.Size(100, 22);
			this.tbNumber.TabIndex = 1;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(346, 74);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(100, 22);
			this.textBox3.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(36, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(177, 56);
			this.button1.TabIndex = 3;
			this.button1.Text = "Max Connections";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(309, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(177, 56);
			this.button2.TabIndex = 4;
			this.button2.Text = "Max File Size";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 79);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "Connection Number:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(36, 165);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(177, 56);
			this.button3.TabIndex = 7;
			this.button3.Text = "Max MSG Length";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// tbMsgLen
			// 
			this.tbMsgLen.Location = new System.Drawing.Point(132, 227);
			this.tbMsgLen.Name = "tbMsgLen";
			this.tbMsgLen.Size = new System.Drawing.Size(100, 22);
			this.tbMsgLen.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 232);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 17);
			this.label2.TabIndex = 8;
			this.label2.Text = "Msg length:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(906, 393);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.tbMsgLen);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.tbNumber);
			this.Controls.Add(this.tbAddr);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbAddr;
		private System.Windows.Forms.TextBox tbNumber;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox tbMsgLen;
		private System.Windows.Forms.Label label2;
	}
}

