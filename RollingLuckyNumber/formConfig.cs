//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.IO;
//using System.Security.AccessControl;
//using static Org.BouncyCastle.Math.EC.ECCurve;
//using System.Drawing.Text;
//using static RollingLuckyNumber.formConfig;
//using NPOI.SS.UserModel;
//using NPOI.SS.Formula.Functions;
//using NPOI.SS.Util;
using Newtonsoft.Json;
using Guna.UI2.WinForms;
using System.Media;
using NAudio.Wave;
using System.Diagnostics;


namespace RollingLuckyNumber
{
    public partial class formConfig : Form
    {
        //đọc file ở configPath
        private string configPath = "config.json";
        private int amountEleLstSound = formStart.tAConfig.lstSound.Count();
        private static List<Guna2CheckBox> lstCheckBoxSound = new List<Guna2CheckBox>();
        private List<Giai> lstGiai;
        public static string imgDirect = Path.Combine("img");
        public static string soundDirect = Path.Combine(Application.StartupPath, "..", "..", "..", "..", "sound");

        private bool checkConfigImg = false, checkConfigImg2 = false, disposed = false;
        private int curIDGiai = 0;

        private Stopwatch stopwatch = new Stopwatch();
        private static short playing = -1;//0: khong phat , 1:dang phat, -1: landautien
        private Guna2Button curAudioBtn;

        private SoundPlayer sp = new SoundPlayer();
        private List<string> lstPathSound = new List<string>();
        private double audioTime;
        public class Giai
        {
            public int IDGiai { get; set; }
            public string TenGiai { get; set; }
            public string PhanThuong { get; set; }
            public int SoLuong { get; set; }
        }

        public formConfig()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void lnkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void formConfig_Load(object sender, EventArgs e)
        {
            lstGiai = JsonConvert.DeserializeObject<List<Giai>>(File.ReadAllText(configPath));
            //set Format cho datagridview
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "IDGiai";
            idColumn.HeaderText = "ID Giai";
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn tenColumn = new DataGridViewTextBoxColumn();
            tenColumn.DataPropertyName = "TenGiai";
            tenColumn.HeaderText = "Ten Giai";
            dataGridView1.Columns.Add(tenColumn);

            DataGridViewTextBoxColumn phanThuongColumn = new DataGridViewTextBoxColumn();
            phanThuongColumn.DataPropertyName = "PhanThuong";
            phanThuongColumn.HeaderText = "Phan Thuong";
            dataGridView1.Columns.Add(phanThuongColumn);

            DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
            soLuongColumn.DataPropertyName = "SoLuong";
            soLuongColumn.HeaderText = "So Luong";
            dataGridView1.Columns.Add(soLuongColumn);
            dataGridView1.DataSource = lstGiai;
            timer1.Interval = 1000;
            foreach (string file in Directory.GetFiles(imgDirect))
            {
                string filename = Path.GetFileNameWithoutExtension(file);

                if (filename == "imgFormPlay")
                {
                    pbGunaFormPlay.BackgroundImage = Image.FromFile(file);
                }
                else if (filename == "imgFormStart")
                {
                    pbGunaFormStart.BackgroundImage = Image.FromFile(file);
                }
            }
            foreach (var item in formStart.tAConfig.lstImage)
            {
                if (item.ID == 1)
                {
                    chbGunaImgFrmStart.Checked = (item.State != 1) ? false : true;
                }
                if (item.ID == 2)
                {
                    chbGunaImgFrmPlay.Checked = (item.State != 1) ? false : true;
                }
            }
            lstCheckBoxSound.AddRange([chbGunaSoundFs, chbGunaSoundQt, chbGunaSoundKq, chbGunaSound1g, chbGunaSoundTc]);
            for (short i = 0; i < amountEleLstSound; i++)
            {
                lstCheckBoxSound[i].Checked = (formStart.tAConfig.lstSound[i].State == 1) ? true : false;
            }

            lstPathSound.AddRange(new string[5] { soundDirect + @"\fs.wav", soundDirect + @"\qt.wav", soundDirect + @"\kq.wav", soundDirect + @"\1g.wav", soundDirect + @"\tc.wav" });

            FormConfig_Load_Reload();
        }
        private void cbOptConf_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        //Tab Config Giai
        private void tabPage1_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
        }
        private void FormConfig_Load_Reload()
        {
            formStart.LoadAnotherConfig();
            loadConfig();
            foreach (var item in formStart.tAConfig.lstColorEle)
            {
                if (item.ID == 1)
                {
                    btnGunaPreviewBD.FillColor = formStart.ConvertToColor(item.Color);
                    btnGunaPreviewBD.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 2)
                {
                    btnGunaPreviewCD.FillColor = formStart.ConvertToColor(item.Color);
                    btnGunaPreviewCD.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 3)
                {
                    btnGunaPreviewKQ.FillColor = formStart.ConvertToColor(item.Color);
                    btnGunaPreviewKQ.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 4)
                {
                    btnGunaPreviewQ.FillColor = formStart.ConvertToColor(item.Color);
                    btnGunaPreviewQ.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 5)
                {
                    guna2Button3.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 6)
                {
                    guna2Button4.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 7)
                {
                    guna2Button8.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 8)
                {
                    guna2Button11.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
                else if (item.ID == 9)
                {
                    guna2Button18.ForeColor = formStart.ConvertToColor(item.ColorText);
                }
            }
            foreach (var item in formStart.tAConfig.lstDuongDan)
            {
                if (item.ID == 1)
                {
                    txtGunaResultPath.Text = item.Path;
                }
                else if (item.ID == 2)
                {
                    txtGunaSourcePath.Text = item.Path;
                }
            }
            txtGunaTimeroll.Text = formStart.tAConfig.TimeRoll.ToString();
            //pbGunaFormPlay.BackgroundImage = Image.FromFile("");
        }
        public List<Giai> loadConfig()
        {
            try
            {
                //dung refresh để báo cho datagridv biết là dữ liệu trong các ô đã bị thay đổi(Nếu gọi khi xóa 1 thành phần thì sẽ lỗi)
                dataGridView1.Refresh();
                btnXoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }
            return lstGiai;
        }
        private void saveConfig(List<Giai> lGiai)
        {
            try
            {
                string JsonGiai = JsonConvert.SerializeObject(lGiai, Formatting.Indented);
                File.WriteAllText(configPath, JsonGiai);
                loadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi Save cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int idGiai = getLastIdGiai(lstGiai) + 1;
            try
            {
                string tenGiai = txtTenGiai.Text;
                string phanThuong = txtPhanThuong.Text;
                int soLuong = int.Parse(txtSoLuong.Text);

                if (soLuong != 0)
                {
                    curIDGiai = idGiai;
                    lstGiai.Add(new Giai
                    {
                        IDGiai = idGiai,
                        TenGiai = tenGiai,
                        PhanThuong = phanThuong,
                        SoLuong = soLuong
                    });
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = lstGiai;
                    txtTenGiai.Clear();
                    txtSoLuong.Clear();
                    txtPhanThuong.Clear();

                }
                else
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }

        }
        private int getLastIdGiai(List<Giai> lstGiai)
        {
            if (lstGiai != null && lstGiai.Count > 0)
            {
                return lstGiai.Last().IDGiai;
            }
            return 0;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curIDGiai = int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            txtTenGiai.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            txtSoLuong.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            txtPhanThuong.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            btnXoa.Enabled = true;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (curIDGiai != 0)
            {
                try
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa Giải có ID là '{curIDGiai}' không?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < lstGiai.Count(); i++)
                        {
                            if (lstGiai[i].IDGiai == curIDGiai)
                            {
                                lstGiai.RemoveAt(i);
                            }
                        }
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = lstGiai;
                        txtTenGiai.Clear();
                        txtSoLuong.Clear();
                        txtPhanThuong.Clear();
                        btnXoa.Enabled = false;
                        curIDGiai = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn Giải muốn xóa!", "Thông báo", MessageBoxButtons.OK);
            }
        }
        private bool checkInputNumber(string ip)
        {
            int result;
            return int.TryParse(ip, out result);
        }
        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            if (!checkInputNumber(txtSoLuong.Text))
            {
                MessageBox.Show("Giá trị không hợp lệ!!! (không phải số)");
                txtSoLuong.Focus();
            }
        }
        private void formConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (playing != -1 && playing != 0)
            {
                sp.Stop();
                sp.Dispose();
                stopwatch.Reset();
                timer1.Stop();
            }
            lstCheckBoxSound.Clear();
        }

        private void btnGunaKQPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                txtGunaResultPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        //Tab Control Load+Reset
        private void TabControl_Load_Reset()
        {

        }

        private void disposedSP(SoundPlayer sp1)
        {
            if (!disposed)
            {
                sp1.Dispose();
                disposed = true;
            }
        }
        private void SaveAnotherConfig(formStart.commonFormat CM, SoundPlayer sp1)
        {
            try
            {
                //save config duongdan in class taconfig
                foreach (var item in CM.lstDuongDan)
                {
                    if (item.ID == 1)
                    {
                        item.Path = txtGunaResultPath.Text;
                    }
                    else if (item.ID == 2)
                    {
                        item.Path = txtGunaSourcePath.Text;
                    }
                }

                //saveTimerolling in taconfig
                CM.TimeRoll = int.Parse(txtGunaTimeroll.Text);

                //save ImgConfig
                if (checkConfigImg) //formstart
                {
                    //Lấy những file ở path (trong folder img)
                    string[] files = Directory.GetFiles(imgDirect);
                    for(int i = 0; i < files.Count(); i++)
                    {
                        string filename = Path.GetFileNameWithoutExtension(files[i]);
                        if (filename == "imgFormStart")
                        {
                            formStart.changeBackgoundImage();
                            File.Delete(files[i]);
                        }
                    }
                    pbGunaFormStart.BackgroundImage.Dispose();
                    File.Move(Path.Combine(imgDirect, "imgFormStart_Tempo.jpg"), Path.Combine(imgDirect, "imgFormStart.jpg"));
                    pbGunaFormStart.BackgroundImage = Image.FromFile(Path.Combine(imgDirect, "imgFormStart.jpg"));
                    checkConfigImg = false;
                }
                if (checkConfigImg2) // formconfig
                {
                    //Lấy những file ở path (trong folder img)
                    string[] files = Directory.GetFiles(imgDirect);
                    foreach (string oSeek in files)
                    {
                        string filename = Path.GetFileNameWithoutExtension(oSeek);

                        if (filename == "imgFormPlay")
                        {
                            frmPlay.changeBackgoundImage();
                            File.Delete(oSeek);
                        }                       
                    }
                    pbGunaFormPlay.BackgroundImage.Dispose();
                    File.Move(Path.Combine(imgDirect, "imgFormPlay_Tempo.jpg"), Path.Combine(imgDirect, "imgFormPlay.jpg"));
                    pbGunaFormStart.BackgroundImage = Image.FromFile(Path.Combine(imgDirect, "imgFormPlay.jpg"));
                    checkConfigImg = false;
                }
                foreach (var item in CM.lstImage)
                {
                    if (item.ID == 1)
                    {
                        item.State = (chbGunaImgFrmStart.Checked == false) ? 0 : 1;
                    }
                    if (item.ID == 2)
                    {
                        item.State = (chbGunaImgFrmPlay.Checked == false) ? 0 : 1;
                    }
                }
                for (short i = 0; i < amountEleLstSound; i++)
                {
                    CM.lstSound[i].State = (lstCheckBoxSound[i].Checked == false) ? 0 : 1;
                }

                if (playing != -1)
                {
                    playing = 0;
                    sp1.Stop();
                    stopwatch.Reset();
                    timer1.Stop();
                }
                disposedSP(sp);
                if (disposed)
                {
                    for (short i = 0; i < lstPathSound.Count; i++)
                    {
                        if (lstPathSound[i] == Path.Combine(soundDirect, "fs_temp.wav"))
                        {
                            lstPathSound[0] = soundDirect + @"\fs.wav";
                            File.Delete(Path.Combine(soundDirect, "fs.wav"));
                            File.Move(Path.Combine(soundDirect, "fs_temp.wav"), Path.Combine(soundDirect, "fs.wav"));
                        }
                        else if (lstPathSound[i] == Path.Combine(soundDirect, "qt_temp.wav"))
                        {
                            lstPathSound[1] = soundDirect + @"\qt.wav";
                            File.Delete(Path.Combine(soundDirect, "qt.wav"));
                            File.Move(Path.Combine(soundDirect, "qt_temp.wav"), Path.Combine(soundDirect, "qt.wav"));
                        }
                        else if (lstPathSound[i] == Path.Combine(soundDirect, "kq_temp.wav"))
                        {
                            lstPathSound[2] = soundDirect + @"\kq.wav";
                            File.Delete(Path.Combine(soundDirect, "kq.wav"));
                            File.Move(Path.Combine(soundDirect, "kq_temp.wav"), Path.Combine(soundDirect, "kq.wav"));
                        }
                        else if (lstPathSound[i] == Path.Combine(soundDirect, "1g_temp.wav"))
                        {
                            lstPathSound[3] = soundDirect + @"\1g.wav";
                            File.Delete(Path.Combine(soundDirect, "1g.wav"));
                            File.Move(Path.Combine(soundDirect, "1g_temp.wav"), Path.Combine(soundDirect, "1g.wav"));
                        }
                        else if (lstPathSound[i] == Path.Combine(soundDirect, "tc_temp.wav"))
                        {
                            lstPathSound[4] = soundDirect + @"\tc.wav";

                            File.Delete(Path.Combine(soundDirect, "tc.wav"));
                            File.Move(Path.Combine(soundDirect, "tc_temp.wav"), Path.Combine(soundDirect, "tc.wav"));
                        }
                    }
                }

                string jsonString = JsonConvert.SerializeObject(CM, Formatting.Indented);
                File.WriteAllText(formStart.PathAnotherConfig, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi Lưu cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void Sp_SoundLocationChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //change Background color (btn)
        private void ChangeBgColorBtn(int id, Guna2Button btn)
        {
            DialogResult rs = colorDialog1.ShowDialog();
            if (rs == DialogResult.OK && !string.IsNullOrWhiteSpace(colorDialog1.Color.ToString()))
            {
                Color selectedColor = colorDialog1.Color;
                foreach (var i in formStart.tAConfig.lstColorEle)
                {
                    if (i.ID == id)
                    {
                        i.Color = string.Format("{0}, {1}, {2}", selectedColor.R, selectedColor.G, selectedColor.B);
                        btn.FillColor = selectedColor;
                    }
                }
            }
        }
        private void ChangeTextColorBtn(int id, Control btn)
        {
            DialogResult rs = colorDialog1.ShowDialog();
            if (rs == DialogResult.OK && !string.IsNullOrWhiteSpace(colorDialog1.Color.ToString()))
            {
                Color selectedColor = colorDialog1.Color;
                foreach (var i in formStart.tAConfig.lstColorEle)
                {
                    if (i.ID == id)
                    {
                        i.ColorText = string.Format("{0}, {1}, {2}", selectedColor.R, selectedColor.B, selectedColor.B);
                        btn.ForeColor = colorDialog1.Color;
                    }
                }
            }
        }
        private void btnGunaColorBD_Click(object sender, EventArgs e)
        {
            ChangeBgColorBtn(1, btnGunaPreviewBD);
        }

        private void btnGunaColorCD_Click(object sender, EventArgs e)
        {
            ChangeBgColorBtn(2, btnGunaPreviewCD);
        }

        private void btnGunaColorKQ_Click(object sender, EventArgs e)
        {
            ChangeBgColorBtn(3, btnGunaPreviewKQ);
        }

        private void btnGunaColorQuay_Click(object sender, EventArgs e)
        {
            ChangeBgColorBtn(4, btnGunaPreviewQ);
        }

        private void btngunaTextColorBD_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(1, btnGunaPreviewBD);
        }

        private void btngunaTextColorCD_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(2, btnGunaPreviewCD);
        }

        private void btngunaTextColorKQ_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(3, btnGunaPreviewKQ);
        }

        private void btngunaTextColorQuay_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(4, btnGunaPreviewQ);
        }

        private void btnGunaColorSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu lại thay đổi?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //Sua~ Giaiconfig
                    if (!string.IsNullOrEmpty(txtPhanThuong.Text) && !string.IsNullOrEmpty(txtSoLuong.Text) && !string.IsNullOrEmpty(txtTenGiai.Text))
                    {
                        foreach (var i in lstGiai)
                        {
                            if (i.IDGiai == curIDGiai)
                            {
                                i.PhanThuong = txtPhanThuong.Text;
                                i.SoLuong = int.Parse(txtSoLuong.Text);
                                i.TenGiai = txtTenGiai.Text;
                            }
                        }
                        loadConfig();
                    }
                    //List<Giai> listGiai = JsonConvert.DeserializeObject<List<Giai>>(File.ReadAllText(configPath));
                    saveConfig(lstGiai);
                    SaveAnotherConfig(formStart.tAConfig, sp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void Copy_Move_RenameImg(string imgName, Guna2PictureBox gn2)
        {
            try
            {
                openFileDialog1.Filter = "JPG Files (*.jpg)|*.jpg";

                DialogResult rs = openFileDialog1.ShowDialog();
                if (rs == DialogResult.OK && openFileDialog1.FileName != "")
                {
                    //fileName là cả đường dẫn tới file đã chọn
                    string selectedFile = openFileDialog1.FileName;
                    string newFileName = Path.Combine(imgDirect, imgName + ".jpg");
                    gn2.BackgroundImage.Dispose();

                    if (File.Exists(newFileName))
                    {
                        File.Delete(newFileName);
                    }
                    File.Copy(selectedFile, newFileName);
                    gn2.BackgroundImage = Image.FromFile(newFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGunaImgFormStart_Click(object sender, EventArgs e)
        {
            Copy_Move_RenameImg("imgFormStart_Tempo", pbGunaFormStart);
            checkConfigImg = true;
        }

        private void btnGunaImgFormPlay_Click(object sender, EventArgs e)
        {
            Copy_Move_RenameImg("imgFormPlay_Tempo", pbGunaFormPlay);
            checkConfigImg2 = true;
        }

        private SoundPlayer SoundPlay(string path, Guna2Button gunaBtn, SoundPlayer sp1)
        {
            sp1.SoundLocation = path;
            using (AudioFileReader audioT = new AudioFileReader(path))
            {
                audioTime = audioT.TotalTime.TotalSeconds;
                if (playing == 0 || playing == -1)
                {
                    try
                    {
                        playing = 1;
                        sp1.Play();
                        stopwatch.Start();
                        timer1.Start();
                        curAudioBtn = gunaBtn;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi phát âm thanh: {ex.Message} \n {soundDirect}");
                    }
                }
                else
                {
                    //xu ly dang phat && chua phat xong
                    try
                    {
                        if (curAudioBtn != gunaBtn)//chua phat xong
                        {
                            playing = 1;
                            sp1.Play();
                            stopwatch.Start();
                            timer1.Start();
                            curAudioBtn = gunaBtn;
                        }
                        else //dang phat
                        {
                            sp1.Stop();
                            playing = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi phát âm thanh: {ex.Message} \n {soundDirect}");
                    }
                }
            }

            return sp1;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (stopwatch.Elapsed.TotalSeconds > audioTime)
            {
                playing = 0;
                stopwatch.Reset();
                timer1.Stop();
            }
        }
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            SoundPlay(lstPathSound[0], guna2Button7, sp);
        }
        private void guna2Button14_Click(object sender, EventArgs e)
        {
            SoundPlay(lstPathSound[1], guna2Button14, sp);
        }
        private void guna2Button15_Click(object sender, EventArgs e)
        {
            SoundPlay(lstPathSound[2], guna2Button15, sp);
        }
        private void guna2Button16_Click(object sender, EventArgs e)
        {
            SoundPlay(lstPathSound[3], guna2Button16, sp);
        }
        private void guna2Button17_Click(object sender, EventArgs e)
        {
            SoundPlay(lstPathSound[4], guna2Button17, sp);
        }
        private void chbGunaSoundFs_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnGunaSourcePath_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel File (*.xlsx)| *.xlsx";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                txtGunaSourcePath.Text = openFileDialog1.FileName;
            }
        }

        private void changeSoundPath(int index, string newSoundPath)
        {
            try
            {
                using (SoundPlayer sp1 = new SoundPlayer(newSoundPath))
                {
                    //sp.Dispose();
                    openFileDialog1.Filter = "WAV Files (*.WAV)|*.wav";
                    DialogResult result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK && openFileDialog1.FileName != "")
                    {
                        if (playing != -1 && playing != 0)
                        {
                            playing = 0;
                            sp1.Stop();
                            stopwatch.Reset();
                            timer1.Stop();
                            sp1.Dispose();
                        }
                        //fileName là cả đường dẫn tới file đã chọn
                        string selectedFile = openFileDialog1.FileName;

                        if (File.Exists(newSoundPath))
                        {
                            File.Delete(newSoundPath);
                        }
                        File.Copy(selectedFile, newSoundPath);
                        openFileDialog1.Dispose();
                        lstPathSound[index] = newSoundPath;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGunaSoundStart_Click(object sender, EventArgs e)
        {
            changeSoundPath(0, Path.Combine(soundDirect, "fs_temp.wav"));
        }

        private void btnGunaRollingArchieve_Click(object sender, EventArgs e)
        {
            changeSoundPath(1, Path.Combine(soundDirect, "qt_temp.wav"));
        }

        private void btnGunaAfterArchieve_Click(object sender, EventArgs e)
        {
            changeSoundPath(2, Path.Combine(soundDirect, "kq_temp.wav"));
        }

        private void btnGunaFinishedGiai_Click(object sender, EventArgs e)
        {
            changeSoundPath(3, Path.Combine(soundDirect, "1g_temp.wav"));
        }

        private void btnGunaFinished_Click(object sender, EventArgs e)
        {
            changeSoundPath(4, Path.Combine(soundDirect, "tc_temp.wav"));
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void btnGunaPreviewQ_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(5, guna2Button3);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(6, guna2Button4);
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(7, guna2Button8);
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(8, guna2Button11);
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            ChangeTextColorBtn(9, guna2Button18);
        }
    }
}
