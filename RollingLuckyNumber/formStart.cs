using Newtonsoft.Json;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static RollingLuckyNumber.formStart;
using System.Drawing;
using Newtonsoft.Json.Schema;
using NPOI.DDF;
using Guna.UI2.WinForms;
using System.Media;

namespace RollingLuckyNumber
{
    public partial class formStart : Form
    {
        public static string PathAnotherConfig = "aNotherConfig.json";
        public static string excelFilePath;
        public static string saveAsPath;
        public static commonFormat tAConfig;
        bool varPlay = false;
        private SoundPlayer sp;
        public class DuongDan
        {
            public int ID { get; set; }
            public string Path { get; set; }
        }
        //public static short windowFlag;
        public class Sound
        {
            public int ID { get; set; }
            public int State { get; set; }
        }

        public class ImageEle
        {
            public int ID { get; set; }
            public int State { get; set; }
        }
        public class ColorEle
        {
            public string Color { get; set; }
            public string ColorText { get; set; }
            public int ID { get; set; }
        }
        public class commonFormat
        {
            public int AmountPlay { get; set; }
            public int TimeRoll { get; set; }
            public List<DuongDan> lstDuongDan { get; set; }
            public List<ColorEle> lstColorEle { get; set; }
            public List<ImageEle> lstImage { get; set; }
            public List<Sound> lstSound { get; set; }
        }

        public static List<Giai> lstGiai;
        public class Giai
        {
            public int IDGiai { get; set; }
            public string TenGiai { get; set; }
            public string PhanThuong { get; set; }
            public int SoLuong { get; set; }
        }

        public static void LoadAnotherConfig()
        {
            try
            {
                tAConfig = JsonConvert.DeserializeObject<commonFormat>(File.ReadAllText(PathAnotherConfig));

                Guna2Button btnBatDau = new Guna2Button();
                Guna2Button btnCaiDat = new Guna2Button();
                Guna2Button btnKetQua = new Guna2Button();
                btnBatDau.FillColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 1).Color.ToString());
                btnBatDau.ForeColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 1).ColorText.ToString());

                btnCaiDat.FillColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 2).Color.ToString());
                btnCaiDat.ForeColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 2).ColorText.ToString());

                btnKetQua.FillColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 3).Color.ToString());
                btnKetQua.ForeColor = ConvertToColor(tAConfig.lstColorEle.FirstOrDefault(c => c.ID == 3).ColorText.ToString());

                excelFilePath = Path.Combine(tAConfig.lstDuongDan.First(c => c.ID == 1).Path);
                saveAsPath = Path.Combine(tAConfig.lstDuongDan.First(c => c.ID == 2).Path);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        public formStart()
        {
            InitializeComponent();
            //panel1.BackgroundImage =
        }

        public void setControlBackgroundImage(string imgName, string extend)
        {
            //string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"img\";
            string imgpath = Path.Combine(formConfig.imgDirect, imgName + extend);

            string[] allowedExtension = { ".jpg" };
            //string fileExtend = Path.GetExtension(imgpath);

            if (allowedExtension.Contains(extend, StringComparer.OrdinalIgnoreCase))
            {
                if (File.Exists(imgpath))
                {
                    panel1.BackgroundImage = Image.FromFile(imgpath);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy hình ảnh: {imgName} \n {imgpath}", "Lỗi", MessageBoxButtons.OK);
                }
            }

        }

        public static void changeBackgoundImage()
        {
            if(panel1.BackgroundImage != null)
            {
                panel1.BackgroundImage.Dispose();
            }
        }
        //public bool checkMdiChildren(string frmName)
        //{
        //    if (this.MdiChildren.Length > 0)
        //    {
        //        Form[] frm = this.MdiChildren;
        //        for (int i = 0; i < this.MdiChildren.Length; i++)
        //        {
        //            if (frm[i].Name == frmName)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            if (varPlay == true)
            {
                sp.Stop();
                sp.Dispose();
                varPlay = false;
            }

            frmPlay f = new frmPlay();
            this.Hide();
            f.Show();
        }
        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            if (varPlay == true)
            {
                sp.Stop();
                sp.Dispose();
                varPlay = false;
            }
            formConfig f = new formConfig();
            f.ShowDialog();
        }

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            if (varPlay == true)
            {
                sp.Stop();
                sp.Dispose();
                varPlay = false;
            }
            formKQ f = new formKQ();
            f.ShowDialog();
        }

        private void playSound()
        {
            if (tAConfig.lstSound[0].State == 0)
            {
                using (sp = new SoundPlayer(Path.Combine(formConfig.soundDirect + "\\fs.wav")))
                {
                    sp.PlayLooping();
                    varPlay = true;
                }
            }
        }
        private void formStart_Load(object sender, EventArgs e)
        {
            LoadAnotherConfig();
            playSound();

        }
        private void formStart_Activated(object sender, EventArgs e)
        {
            playSound();
            LoadAnotherConfig();
            if (tAConfig.lstImage[0].State == 0)
            {
                setControlBackgroundImage("imgFormStart", ".jpg");
            }
            else
            {
                panel1.BackgroundImage = null;
            }
        }
        public static System.Drawing.Color ConvertToColor(string strRGBColor)
        {
            string[] rgbValues = strRGBColor.Split(", ");

            int red = int.Parse(rgbValues[0]);
            int green = int.Parse(rgbValues[1]);
            int blue = int.Parse(rgbValues[2]);

            System.Drawing.Color cl = System.Drawing.Color.FromArgb(red, green, blue);
            return cl;
        }


        private void formStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (varPlay == true)
            {
                sp.Stop();
                sp.Dispose();
                varPlay = false;
            }
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            if (keyData == Keys.Enter)
            {
                btnBatDau_Click(null, EventArgs.Empty);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static void panel1_BackgroundImageChanged(object sender, EventArgs e)
        {
        }
    }
}
