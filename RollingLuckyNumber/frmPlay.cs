 //using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Drawing.Drawing2D;
//using System.Xml.Linq;
//using Org.BouncyCastle.Asn1.Crmf;
//using System.Drawing.Printing;
//using Org.BouncyCastle.Tls;
using System.Diagnostics;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using Guna.UI2.WinForms;
using System.Media;
using Newtonsoft.Json;
using System.Windows.Forms;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Linq;
using System.Data;
using Org.BouncyCastle.Asn1.X509;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;


namespace RollingLuckyNumber
{
    public partial class frmPlay : Form
    {
        public static List<char> currentNumber = new List<char>();
        public static bool soundQt, soundKQ, sound1G, soundTC, checkOutLstExcelData = false, soundPlaying = false;
        private static int soLuongSoQuay, curIDGiai, rowIndex, amountExcelData;
        private int tgQuay = formStart.tAConfig.TimeRoll;
        private string configPath = "config.json";
        private static bool canClickBtnQuay = true, CanStart = true;
        public static List<Winner> lstWinner = new List<Winner>();
        private static List<Giai> currGiais;
        private static List<Giai> lstGiai;
        private static List<int> arrExceptRow = new List<int>();
        private static short state; //0 : lan dau mo, 1: da hoan thanh, 2: chua hoan thanh
        private static Random random = new Random();
        private static Tuple<long, string> randomRow;
        List<Label> lstLabel = new List<Label>();
        Stopwatch stwatch = new Stopwatch();
        SoundPlayer sp;


        // Var chứa Thông tin trong file excel.xlsx
        private static List<Tuple<long, string>> excelData = new List<Tuple<long, string>>();

        //initpanel btnPlay
        Guna2Button btnCtLeft = new Guna2Button() { Width = 30, Height = 50, ImageSize = new Size(30, 50), FillColor = Color.Transparent, Image = Image.FromFile(Path.Combine(formConfig.imgDirect, "previousArrow.png")), Cursor = Cursors.Hand };
        Guna2Button btnCtRight = new Guna2Button() { Width = 30, Height = 50, ImageSize = new Size(30, 50), FillColor = Color.Transparent, Image = Image.FromFile(Path.Combine(formConfig.imgDirect, "nextArrow.png")), Cursor = Cursors.Hand };
        Guna2Button btnQuay = new Guna2Button() { Font = new Font("Tahoma", 13, FontStyle.Bold), Text = "Quay", Width = 200, Height = 80, FillColor = (formStart.ConvertToColor(formStart.tAConfig.lstColorEle[3].Color)), ForeColor = (formStart.ConvertToColor(formStart.tAConfig.lstColorEle[3].ColorText)), BorderThickness = 1, BorderColor = Color.Black, Cursor = Cursors.Hand };

        Guna2Panel pn = new Guna2Panel() { BorderRadius = 5, BorderThickness = 3, BorderColor = formStart.ConvertToColor("245, 219, 121"), FillColor = formStart.ConvertToColor("12, 94, 150") };
        Panel panelControl = new Panel() { BackColor = Color.Transparent };

        Label lblTenGiai = new Label() { TextAlign = ContentAlignment.MiddleCenter, ForeColor = formStart.ConvertToColor(formStart.tAConfig.lstColorEle[4].ColorText)};
        Label lblPhanThuong = new Label() { TextAlign = ContentAlignment.MiddleCenter, ForeColor = formStart.ConvertToColor(formStart.tAConfig.lstColorEle[5].ColorText)};

        public class Winner
        {
            //ID giai thang
            public short IDGiai { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }

        }
        public class Giai
        {
            public int IDGiai { get; set; }
            public string TenGiai { get; set; }
            public string PhanThuong { get; set; }
            public int SoLuong { get; set; }
        }
        public frmPlay()
        {
            InitializeComponent();
            timer1.Interval = 50;
            //code xiong  xoa
            this.AutoScroll = true;
        }

        public static void changeBackgoundImage()
        {
            frmPlay f = new frmPlay();
            if(f.BackgroundImage != null)
            {
                f.BackgroundImage.Dispose();
            }
        }
        private void LoadConfig()
        {
            lstGiai = JsonConvert.DeserializeObject<List<Giai>>(File.ReadAllText(configPath));
            currGiais = lstGiai;
        }
        private void frmPlay_Load(object sender, EventArgs e)
        {
            if (formStart.tAConfig.lstImage[1].State == 0)
            {
                string imgFormStartFilePath = null;
                string imagePath = Path.Combine(formConfig.imgDirect, "imgFormPlay.jpg");
                if (File.Exists(imagePath))
                {
                    imgFormStartFilePath = imagePath;
                }
                // Kiểm tra xem tệp tin có tồn tại không
                if (!string.IsNullOrEmpty(imgFormStartFilePath))
                {
                    guna2GradientPanel1.FillColor = Color.Transparent;
                    guna2GradientPanel1.FillColor2 = Color.Transparent;
                    this.BackgroundImage = Image.FromFile(imgFormStartFilePath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            LoadConfig();
            curIDGiai = lstGiai.First().IDGiai;
            //guna2Transition1.AnimationType
            ReadExcel(formStart.excelFilePath);
            //setLicense 
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            soLuongSoQuay = excelData.Max().Item1.ToString().Count();
            initializeNumber(soLuongSoQuay);

            soundQt = (formStart.tAConfig.lstSound[1].State == 1) ? false : true;
            soundKQ = (formStart.tAConfig.lstSound[2].State == 1) ? false : true;
            sound1G = (formStart.tAConfig.lstSound[3].State == 1) ? false : true;
            soundTC = (formStart.tAConfig.lstSound[4].State == 1) ? false : true;

            loadDSTrungThuong(curIDGiai);
            amountExcelData = excelData.Count();

            //pncontrol height = 80, width = 300
            panelControl.Controls.AddRange(new Control[] { btnCtLeft, btnQuay, btnCtRight });
            btnCtLeft.Location = new Point(0, 15);
            btnQuay.Location = new Point(btnCtLeft.Location.X + 50);
            btnCtRight.Location = new Point(btnCtLeft.Location.X + 270, 15);
            panelControl.Width = 300;
            panelControl.Location = new Point(AlignControlCenter(panelControl), 850);
            btnQuay.Click += button1_Click;
            btnCtRight.Click += btnNextArrow_Click;
            btnCtLeft.Click += btnPreviousArrow_Click;

            lblTenGiai.Font = new Font("Tahoma", 80, FontStyle.Bold);
            lblPhanThuong.Font = new Font("Tahoma", 25, FontStyle.Bold);
            loadLblGiai(curIDGiai);

            this.Controls.AddRange([lblTenGiai, lblPhanThuong]);
            lblPhanThuong.BringToFront();
            lblTenGiai.BringToFront();
            pn.BringToFront();
            guna2GradientPanel1.Controls.AddRange([panelControl, lblPhanThuong, lblTenGiai]);
            lblTenGiai.BackColor = Color.Transparent;
            lblPhanThuong.BackColor = Color.Transparent;
            panelControl.BringToFront();//loi background trùng màu
        }

        //trả về giá trị của 1 hàng (A, B) ngẫu nhiên
        private Tuple<long, string> getRandomCellValue()
        {
            try
            {
                amountExcelData = excelData.Count();
                rowIndex = random.Next(0, amountExcelData);
                return randomRow = excelData[rowIndex];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hàm getRandomCellValue: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
                return randomRow = null;
            }
        }

        //n: soLuongChuSoQuay -> can viet ham de biet chu so lon nhat trong file excel
        private void initializeNumber(int n)
        {
            int lblWidth = 0;
            int YofPnnumber = 400;
            //int lblHeight = 0;
            pn.Name = "pnRollNumber";
            for (int i = 0; i < n; i++)
            {
                Label lbl = new Label() { Text = "0", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Name = "lblNum" + i, BackColor = formStart.ConvertToColor("53, 152, 219"), ForeColor = Color.White };
                lbl.Font = new Font("Tahoma", 85, FontStyle.Bold);
                if (i > 0)
                {
                    lbl.Location = new Point((lbl.Width * i) + (50 * i) + 45, 20);
                }
                else if (i == 0)
                {
                    lbl.Location = new Point(45, 20);
                    lblWidth = lbl.Width;
                    //lblHeight = lbl.Height;
                }

                lstLabel.Add(lbl);
            }

            pn.Controls.AddRange(lstLabel.ToArray());
            pn.Width = (50 * (n - 1)) + (lblWidth * n) + 120; //(120 - 30)/2
            pn.Height += 80;

            if (soLuongSoQuay > 1)
            {
                pn.Location = new Point(AlignControlCenter(pn), YofPnnumber);
            }
            else
            {
                pn.Location = new Point(AlignControlCenter(pn) - (60 * soLuongSoQuay / 2), YofPnnumber);
            }
            Controls.Add(pn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CanStart)
            {
                if (canClickBtnQuay)
                {
                    canClickBtnQuay = false;
                    if(!guna2CheckBox1.Checked)
                    {
                        ReadExcel(formStart.excelFilePath);
                    }
                    if (checkAmountGiaiRest())
                    {
                        getRandomCellValue();
                        try
                        {
                            foreach (char digit in randomRow.Item1.ToString())
                            {
                                currentNumber.Add(digit);
                            }
                            //lstWinner.Add(new Winner { IDGiai = curIDGiai, ID = 1, Number = , Name =  });

                            stwatch.Start();
                            timer1.Start();
                            if(soundQt)
                            {
                                using (sp = new SoundPlayer(formConfig.soundDirect + "\\qt.wav"))
                                {
                                    sp.Play();
                                }
                            }
                            btnQuay.Enabled = false;
                            btnCtRight.Enabled = false;
                            btnCtLeft.Enabled = false;
                            loadDSTrungThuong(curIDGiai);
                            loadLblGiai(curIDGiai);
                        }
                        catch (Exception ex)
                        {
                            canClickBtnQuay = true;
                            CanStart = false;
                            MessageBox.Show("Số lượng được cài đặt đã vượt quá sô lượng có thể roll trong Source");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng giải đã hết, vui lòng chọn giải khác!", "Thông báo");
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể bắt đầu vì đã hết giá trị để random");
            }

        }

        private void btnNextArrow_Click(object sender, EventArgs e)
        {
            btnQuay.Enabled = true;
            if (canClickBtnQuay)
            {
                if (curIDGiai != lstGiai.Last().IDGiai)
                {
                    curIDGiai = lstGiai[lstGiai.FindIndex(c => c.IDGiai == curIDGiai) + 1].IDGiai;
                }
                else
                {
                    curIDGiai = lstGiai.First().IDGiai;
                }
                if(soundPlaying)
                {
                    sp.Stop();
                }
                loadLblGiai(curIDGiai);
                loadDSTrungThuong(curIDGiai);
            }


        }

        private void btnPreviousArrow_Click(object sender, EventArgs e)
        {
            btnQuay.Enabled = true;
            if (canClickBtnQuay)
            {
                if (soundPlaying)
                {
                    sp.Stop();
                }
                if (curIDGiai != lstGiai.First().IDGiai)
                {
                    curIDGiai = lstGiai[lstGiai.FindIndex(c => c.IDGiai == curIDGiai) - 1].IDGiai;
                }
                else
                {
                    curIDGiai = lstGiai.Last().IDGiai;
                }
                loadLblGiai(curIDGiai);
                loadDSTrungThuong(curIDGiai);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //Cột A, Cột B <=> MSSV, Tên <=> long, string
        private List<Tuple<long, string>> ReadExcel(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workBook = new XSSFWorkbook(fs);
                ISheet sheet = workBook.GetSheetAt(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        long colAValue = row.GetCell(0) != null ? long.Parse(row.GetCell(0).ToString()) : 0;

                        // ?.ToString() => Trả về row.GetCell(0).ToString() nếu r.get k null còn null trả về ""
                        // ?? nêu ?.ToString == null thì trả về ""
                        string colBValue = row.GetCell(1)?.ToString() ?? "";

                        excelData.Add(new Tuple<long, string>(colAValue, colBValue));
                    }
                }
            }
            return excelData;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //rolling
            Random rd = new Random();
            foreach (var ele in lstLabel)
            {
                ele.Text = rd.Next(0, 9).ToString();
            }
            if (stwatch.Elapsed.TotalMilliseconds >= tgQuay)
            {
                //So luong ki tu ma so random hien tai chenh lech voi tong so ki tu cua so lon nhat
                int AmountCharRanNum = soLuongSoQuay - currentNumber.Count();
                int j = 0;
                for (int i = 0; i < lstLabel.Count(); i++)
                {
                    if (i >= AmountCharRanNum)
                    {
                        lstLabel[i].Text = currentNumber[j].ToString();
                        j++;
                    }
                    else
                    {
                        lstLabel[i].Text = "0";
                    }
                }
                btnQuay.Enabled = true;
                btnCtRight.Enabled = true;
                btnCtLeft.Enabled = true;

                canClickBtnQuay = true;
                if(soundPlaying)
                {
                    sp.Stop();
                    sp.Dispose();
                }
                stwatch.Stop();
                timer1.Stop();
                stwatch.Reset();

                //DialogResult result = fConf.ShowDialog();
                formConfirm fConf = new formConfirm();
                if (fConf.ShowDialog() == DialogResult.OK)
                {
                    lstWinner.Add(new Winner { IDGiai = (short)curIDGiai, Name = randomRow.Item2, Number = randomRow.Item1.ToString() });
                    if(guna2CheckBox1.Checked)
                    {
                        excelData.RemoveAt(rowIndex);
                    }
                    currGiais.First(c => c.IDGiai == curIDGiai).SoLuong -= 1;
                    loadDSTrungThuong(curIDGiai);                   
                    if(checkState())
                    {
                        if (sound1G)
                        {
                            if (checkAmountGiaiRest() == false)
                            {
                                using (sp = new SoundPlayer(Path.Combine(formConfig.soundDirect, "1g.wav")))
                                {
                                    soundPlaying = true;
                                    sp.PlayLooping();
                                }
                            }
                        }
                    }
                    canClickBtnQuay = true;

                    randomRow = null;
                    currentNumber.Clear();
                }
            }
        }
        private int AlignControlCenter(Control ct)
        {
            return (this.ClientSize.Width - ct.Width) / 2; ;
        }

        private bool checkState()
        {
            state = 2;
            foreach (var item in currGiais)
            {
                if (item.SoLuong > 0)
                {
                    state = 1;
                    break;
                }
            }
            if (state == 2)
            {
                if(soundTC)
                {
                    using (sp = new SoundPlayer(Path.Combine(formConfig.soundDirect, "tc.wav")))
                    {
                        sp.PlayLooping();
                        soundPlaying = true;
                    }
                }
                //Luu vao file 
                //----------------------------------
                string fileName = "KetQua" + DateTime.Now.ToString("dd-MM-yyyy mm-hh") + ".xlsx";
                using (var package = new ExcelPackage())
                {
                    for (int i = 0; i < currGiais.Count(); i++)
                    {
                        var workSheet = package.Workbook.Worksheets.Add(currGiais[i].TenGiai.ToString());
                        workSheet.Cells["A1"].Value = "MS";
                        workSheet.Cells["B1"].Value = "Ho Va Ten";
                        var amountWinnerByID = lstWinner.Where(c => c.IDGiai == currGiais[i].IDGiai).Count() + 2;
                        var winner = lstWinner.Where(c => c.IDGiai == currGiais[i].IDGiai).ToList();
                        for (int j = 2; j < amountWinnerByID; j++)
                        {
                            workSheet.Cells["A" + j].Value = winner[j - 2].Number;
                            workSheet.Cells["B" + j].Value = winner[j - 2].Name;
                        }
                    }
                    package.SaveAs(Path.Combine(formStart.saveAsPath, fileName));
                }
                MessageBox.Show($"Trò chơi đã hoàn thành.\nKết quả được lưu ở đường dẫn: {formStart.saveAsPath + fileName}", "Thông báo", MessageBoxButtons.OK);
                loadLblGiai(curIDGiai);
                loadDSTrungThuong(curIDGiai);
                return false;
            }
            return true;
        }
        private void frmPlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == 1) // chua hoan thanh
            {
                if (MessageBox.Show("Bạn chưa hoàn thành tất cả các giải quay, các kết quả hiện tại sẽ không được lưu lại.\nBạn có chắc chắn muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lstWinner.Clear();
                    frmPlay f = new frmPlay();
                    f.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else  //0: chua quay lan nao || da hoan thanh 
            {
                if (soundPlaying)
                {
                    sp.Stop();
                    sp.Dispose();
                }
                lstWinner.Clear();
                frmPlay f = new frmPlay();
                f.Close();
            }
        }
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked == true)
            {
                //check so luong winner con lai
                //neu so luong lon hon so dong trong file excel randomlist thi ok
            }
        }
        private void loadLblGiai(int id)
        {
            var curgiai = lstGiai.First(c => c.IDGiai == id);
            //set color in configFile
            if (curgiai.SoLuong < 10)
            {
                lblTenGiai.Text = string.Concat("0" + curgiai.SoLuong.ToString() + " " + curgiai.TenGiai.ToString());
                lblPhanThuong.Text = curgiai.PhanThuong.ToString();
            }
            else
            {
                lblTenGiai.Text = curgiai.SoLuong.ToString() + " " + curgiai.TenGiai.ToString();
                lblPhanThuong.Text = curgiai.PhanThuong.ToString();
            }
            lblTenGiai.AutoSize = true;
            lblPhanThuong.AutoSize = true;

            SizeF sizeLblTenGiai = lblTenGiai.CreateGraphics().MeasureString(lblTenGiai.Text, lblTenGiai.Font);
            SizeF sizeLblPhanThuong = lblPhanThuong.CreateGraphics().MeasureString(lblPhanThuong.Text, lblPhanThuong.Font);
            lblTenGiai.Location = new Point((this.ClientSize.Width - (int)sizeLblTenGiai.Width) / 2, 50);
            lblPhanThuong.Location = new Point((this.ClientSize.Width - (int)sizeLblPhanThuong.Width) / 2, (int)sizeLblTenGiai.Height + 50);
        }

        public void loadDSTrungThuong(int id)
        {
            var curGiaiInfor = currGiais.Where(c => c.IDGiai == id);
            lblGunaDSTenGiai.Text = curGiaiInfor.First().TenGiai;
            lblGunaDSTenGiai.ForeColor = ForeColor = formStart.ConvertToColor(formStart.tAConfig.lstColorEle[6].ColorText);
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            if (lstWinner.Where(c => c.IDGiai == id).Count() > 0)
            {
                foreach (var item in lstWinner.Where(c => c.IDGiai == curIDGiai))
                {
                    flowLayoutPanel1.Controls.Add(new Label() { Text = item.Number, AutoSize = true, Font = new Font("Arial", 13), ForeColor = formStart.ConvertToColor(formStart.tAConfig.lstColorEle[7].ColorText)});
                    flowLayoutPanel2.Controls.Add(new Label() { Text = item.Name, AutoSize = true, Font = new Font("Arial", 13), ForeColor = formStart.ConvertToColor(formStart.tAConfig.lstColorEle[8].ColorText)});
                }
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (canClickBtnQuay)
                {
                    button1_Click(null, EventArgs.Empty);
                }
            }
            if (keyData == Keys.Right)
            {
                btnNextArrow_Click(null, EventArgs.Empty);
            }
            if (keyData == Keys.Left)
            {
                btnPreviousArrow_Click(null, EventArgs.Empty);
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool checkAmountGiaiRest()
        {
            if (currGiais.First(c => c.IDGiai == curIDGiai).SoLuong > 0)
            {
                canClickBtnQuay = false;
                return true;
            }
            else//lan cuoi
            {
                canClickBtnQuay = true;
                btnQuay.Enabled = false;
                return false;
            }
        }
        private void frmPlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            formStart f = new formStart();
            f.Show();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
