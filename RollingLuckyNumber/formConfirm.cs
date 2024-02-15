using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using static Org.BouncyCastle.Math.EC.ECCurve;
namespace RollingLuckyNumber
{
    public partial class formConfirm : Form
    {
        private SoundPlayer sp;
        private bool soundCheck = frmPlay.soundKQ;
        public formConfirm()
        {
            InitializeComponent();
        }

        private void formConfirm_Load(object sender, EventArgs e)
        {
            if (soundCheck)
            {
                using (sp = new SoundPlayer(Path.Combine(formConfig.soundDirect, "fs.wav")))
                {
                    sp.PlayLooping();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    if (soundCheck)
                    {
                        sp.Stop();
                        sp.Dispose();
                    }
                }
                else if (keyData == Keys.Escape)
                {
                    if (soundCheck)
                    {
                        sp.Stop();
                        sp.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (soundCheck)
            {
                sp.Stop();
                sp.Dispose();
            }
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (soundCheck)
            {
                sp.Stop();
                sp.Dispose();
            }
            this.Close();
        }

        private void formConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (soundCheck)
            {
                sp.Stop();
                sp.Dispose();
            }
        }
    }
}
