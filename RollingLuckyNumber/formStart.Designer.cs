namespace RollingLuckyNumber
{
    partial class formStart
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStart));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCaiDat = new Guna.UI2.WinForms.Guna2Button();
            btnKetQua = new Guna.UI2.WinForms.Guna2Button();
            btnBatDau = new Guna.UI2.WinForms.Guna2Button();
            panel1 = new Panel();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnCaiDat, 0, 1);
            tableLayoutPanel1.Controls.Add(btnKetQua, 0, 2);
            tableLayoutPanel1.Controls.Add(btnBatDau, 0, 0);
            tableLayoutPanel1.Location = new Point(668, 369);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(168, 155);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btnCaiDat
            // 
            btnCaiDat.CustomizableEdges = customizableEdges1;
            btnCaiDat.DisabledState.BorderColor = Color.DarkGray;
            btnCaiDat.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCaiDat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCaiDat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCaiDat.Font = new Font("Segoe UI", 9F);
            btnCaiDat.ForeColor = Color.White;
            btnCaiDat.Image = (Image)resources.GetObject("btnCaiDat.Image");
            btnCaiDat.ImageOffset = new Point(-4, 2);
            btnCaiDat.Location = new Point(3, 56);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCaiDat.Size = new Size(162, 45);
            btnCaiDat.TabIndex = 5;
            btnCaiDat.Text = "Cài đặt";
            btnCaiDat.TextOffset = new Point(-2, 0);
            btnCaiDat.Click += btnCaiDat_Click;
            // 
            // btnKetQua
            // 
            btnKetQua.CustomizableEdges = customizableEdges3;
            btnKetQua.DisabledState.BorderColor = Color.DarkGray;
            btnKetQua.DisabledState.CustomBorderColor = Color.DarkGray;
            btnKetQua.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnKetQua.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnKetQua.Font = new Font("Segoe UI", 9F);
            btnKetQua.ForeColor = Color.White;
            btnKetQua.Image = (Image)resources.GetObject("btnKetQua.Image");
            btnKetQua.ImageOffset = new Point(-4, 2);
            btnKetQua.Location = new Point(3, 109);
            btnKetQua.Name = "btnKetQua";
            btnKetQua.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnKetQua.Size = new Size(162, 43);
            btnKetQua.TabIndex = 4;
            btnKetQua.Text = "Kết quả";
            btnKetQua.TextOffset = new Point(-2, 0);
            btnKetQua.Click += btnKetQua_Click;
            // 
            // btnBatDau
            // 
            btnBatDau.CustomizableEdges = customizableEdges5;
            btnBatDau.DisabledState.BorderColor = Color.DarkGray;
            btnBatDau.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBatDau.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBatDau.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBatDau.Font = new Font("Segoe UI", 9F);
            btnBatDau.ForeColor = Color.White;
            btnBatDau.Image = (Image)resources.GetObject("btnBatDau.Image");
            btnBatDau.ImageOffset = new Point(-4, 2);
            btnBatDau.Location = new Point(3, 3);
            btnBatDau.Name = "btnBatDau";
            btnBatDau.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBatDau.Size = new Size(162, 45);
            btnBatDau.TabIndex = 6;
            btnBatDau.Text = "Bắt đầu";
            btnBatDau.TextOffset = new Point(-2, 0);
            btnBatDau.Click += btnBatDau_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(901, 552);
            panel1.TabIndex = 4;
            panel1.BackgroundImageChanged += panel1_BackgroundImageChanged;
            panel1.Paint += panel1_Paint;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // formStart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 552);
            Controls.Add(panel1);
            Name = "formStart";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rolling lucky number";
            Activated += formStart_Activated;
            FormClosing += formStart_FormClosing;
            Load += formStart_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button btnKetQua;
        private Guna.UI2.WinForms.Guna2Button btnCaiDat;
        private Guna.UI2.WinForms.Guna2Button btnBatDau;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        public static Panel panel1;
    }
}
