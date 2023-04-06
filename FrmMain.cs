using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Models;


namespace Barter
{
    public partial class FrmMain : Form
    {
        public static  int TotalPrice = 0;
        int sumber = 0;
        int n;
        int i = 10;
        string[] Goods = null;

        public static string Gid { get; set; }

        List<GoodsInfo> Used_Goods = GoodsInfoManager.GetGoodsInfos();

        TcpListener server = null;
        TcpClient client = null;
        NetworkStream ns = null;
        //存放临时接收数据的字节数组
        byte[] data = new byte[1024 * 1024];

        public FrmMain()
        {
            server = new TcpListener(IPAddress.Parse("192.168.51.143"), 6005);
            Control.CheckForIllegalCrossThreadCalls = false;//取消窗体控件的依附性
            InitializeComponent();
        }

        #region 窗体加载事件方法
        /// <summary>
        /// 窗体加载事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            List<GoodType> types = GoodTypeManager.GetGoodType();
            foreach (GoodType type in types)
            {
                cboType.Items.Add(type.ID);
            }

            txtSeller.Text=FrmLogin.UserID.ToString();

            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
            receive();
        }
        #endregion

        #region 首页动态加载Panel控件
        /// <summary>
        /// 首页动态加载Panel控件
        /// </summary>
        private void Panels()
        {
            pnlinear.Controls.Clear();//清空所有控件
            //控件大小
            int x = 150;
            int y = 200;
            //计数
            GoodsPrice(Used_Goods);

            for (int i = 0; i < 4; i++)//列数
            {
                for (int k = 0; k < n / 4; k++)//行数
                {
                    //创建控件
                    Panel pnl = new Panel();
                    if (i == 0)
                    {
                        if (k == 0)
                        {
                            pnl.Location = new Point(20 * i, 20 * i);
                        }
                        else
                        {
                            pnl.Location = new Point((x + 20) * i, (y + 20) * k);
                        }
                    }
                    else
                    {
                        if (k == 0)
                        {
                            pnl.Location = new Point(20 * i + x * i, 0);
                        }
                        else
                        {
                            pnl.Location = new Point((x + 20) * i, (y + 20) * k);
                        }
                    }
                    pnl.Size = new Size(x, y);
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                    pnl.Name = "pnl ";//控件Name border-radius
                    pnlinear.Controls.Add(pnl);
                }
            }

            for (int j = 0; j < n % 4; j++)
            {
                Panel pnl = new Panel();
                if (j == 0)
                {
                    pnl.Location = new Point(0, n / 4 * 220);
                }
                else
                {
                    pnl.Location = new Point(x + 20 * j, n / 4 * 220);
                }


                pnl.Size = new Size(x, y);
                pnl.BorderStyle = BorderStyle.FixedSingle;
                pnl.Name = "pnl ";//控件Name
                pnlinear.Controls.Add(pnl);
            }

        }
        #endregion

        #region 首页向Panel里面动态添加PictureBox
        /// <summary>
        /// 首页向Panel里面动态添加PictureBox
        /// </summary>
        private void PictureBoxs()
        {
            int j = 0;
            Goodsinfo(Used_Goods);
            foreach (Control c in pnlinear.Controls)
            {
                PictureBox pic = new PictureBox();

                if (c is Panel c1)
                {
                    pic.Size = new Size(130, 120);
                    pic.Location = new Point(10, 10);
                    pic.ImageLocation = Goods[j];
                    pic.Tag = Used_Goods[j].Gid;
                    j++;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    c1.Controls.Add(pic);                 
                    pic.Click += new EventHandler(Pic_Click);//添加点击事件
                }
            }
        }
        #endregion

        #region 给每一个PictureBox控件添加点击事件
        /// <summary>
        /// 给每一个PictureBox控件添加点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.7;//主窗体添加透明度
            PictureBox p = sender as PictureBox;
            Gid = p.Tag.ToString();
            FrmGoodDetails frm = new FrmGoodDetails();      
            frm.ShowDialog();
            this.Opacity = 1;
        }
        #endregion

        #region 首页商品价格控件
        /// <summary>
        /// 首页商品价格控件
        /// </summary>
        private void LabelMoney()
        {
            int j = 0;
            GoodsPrice(Used_Goods);
            foreach (Control c in pnlinear.Controls)
            {

                Label lbl = new Label();
                if (c is Panel c1)
                {
                    lbl.Size = new Size(140, 30);
                    lbl.Location = new Point(10, 130);
                    lbl.Name = "lbl " + j;//控件Name
                    lbl.Text = "￥" + Goods[j] + ".00";
                    j++;
                    lbl.Font = new Font("楷体", 18);
                    lbl.AutoSize = false;
                    lbl.Parent = c1;
                    lbl.BackColor = Color.Transparent;
                    c1.Controls.Add(lbl);
                }
            }
        }
        #endregion

        #region 首页商品描述控件
        /// <summary>
        /// 首页商品描述控件
        /// </summary>
        private void LabelDescription()
        {
            string s = null;
            int length = 0;
            GoodsGdescription(Used_Goods);
            int j = 0;
            foreach (Control c in pnlinear.Controls)
            {

                Label lbl = new Label();
                if (c is Panel c1)
                {
                    lbl.Size = new Size(140, 40);
                    lbl.Location = new Point(5, 160);
                    lbl.Name = "lbl " + j;//控件Name
                    length = lbl.Width;
                    lbl.Text = Goods[j];
                    j++;
                    lbl.Font = new Font("楷体", 10);
                    c1.Controls.Add(lbl);
                }
                s = lbl.Text;
                if (s.Length >= 20)
                {
                    lbl.AutoSize = false;
                    lbl.Text = s.Length > 16 ? s.Substring(0, 16) + "..." : s;
                    // l.Text = s.Substring(0, length) + "···";
                }
                else
                {
                    //l.Text = "哈哈哈哈";
                }
                lbl.AutoSize = false;
            }
        }
        #endregion

        #region 首页获取每个商品的图片
        /// <summary>
        /// 首页获取每个商品的图片
        /// </summary>
        /// <returns></returns>
        public string[] Goodsinfo(List<GoodsInfo> goods)
        {
            Used_Goods = goods;
            Goods = new string[Used_Goods.Count];
            int i = 0;
            foreach (var u in Used_Goods)
            {
                Goods[i] = u.Gpicture;
                i++;
            }
            return Goods;
        }
        #endregion

        #region 首页获取每个商品的价格
        /// <summary>
        /// 首页获取每个商品的价格
        /// </summary>
        /// <returns></returns>
        public string[] GoodsPrice(List<GoodsInfo> goods)
        {
            Used_Goods = goods;
            Goods = new string[Used_Goods.Count];
            int i = 0;
            foreach (var u in Used_Goods)
            {
                Goods[i] = u.Gprice.ToString();
                i++;
            }
            n = i;
            return Goods;
        }
        #endregion

        #region 首页获取每个商品商品描述
        /// <summary>
        /// 首页获取每个商品商品描述
        /// </summary>
        /// <returns></returns>
        public string[] GoodsGdescription(List<GoodsInfo> goods)
        {
            List<GoodsInfo> Used_Goods = goods;
            Goods = new string[Used_Goods.Count];
            int i = 0;
            foreach (var u in Used_Goods)
            {
                Goods[i] = u.Gdescription.ToString();
                i++;
            }
            return Goods;
        }
        #endregion

        #region 我的私信发送事件方法
        /// <summary>
        /// 我的私信发送事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsend_Click(object sender, EventArgs e)
        {
            string s = rtbsend.Text;
            byte[] info=Encoding.UTF8.GetBytes(s);
            if (ns==null)
            {
                ns = client.GetStream();
            }
            ns.Write(info,0,info.Length);
            PictureBox pic = new PictureBox();//创建头像框图片框对象
            pic.Size = new Size(30, 30); //设置大小
            pic.Location = new Point(170, i - 5); //定位
            pic.BackColor = Color.Red;



            Label l = new Label();//创建label 聊天气泡框对象

            l.Location = new Point(160 - s.Length * 17, i - 5);//定位
            l.BackColor = Color.FromArgb(18, 183, 245);
            l.TextAlign = ContentAlignment.MiddleLeft;
            l.Height = 35;
            l.Width = s.Length * 17;

            if (l.Width >= 170)
            {
                l.Location = new Point(0, i - 5);//定位
                l.Width = 160;
                l.Height = 50;

            }
            else if (l.Width >= 335)
            {
                l.Width = 160;
                l.Height = 80;
            }
         
            l.Text = s;
            l.Font = new Font("楷体", 10);
            i += l.Height + 5;
            PnB.Controls.Add(pic);
            PnB.Controls.Add(l);

            PictureBox Tu = new PictureBox();
            Tu.Location = new Point(160 - s.Length * 17, i - 5);//定位
            Tu.BackColor = Color.FromArgb(18, 183, 245);
            //Tu.TextAlign = ContentAlignment.MiddleLeft;
            Tu.Height = 35;
            Tu.Width = s.Length * 17;

            if (Tu.Width >= 170)
            {
                Tu.Location = new Point(0, i - 5);//定位
                Tu.Width = 160;
                Tu.Height = 50;

            }
            else if (Tu.Width >= 335)
            {
                Tu.Width = 160;
                Tu.Height = 80;
            }

            Tu.Text = s;
            Tu.Font = new Font("楷体", 10);
            i += Tu.Height + 5;
            PnB.Controls.Add(pic);
            PnB.Controls.Add(Tu);


            if (i >= 240)
            {
                pnlS.Height += l.Height + 5;
                PnA.Height += l.Height + 5;
                PnB.Height += l.Height + 5;
                pnReceive.VerticalScroll.Value = this.pnReceive.VerticalScroll.Maximum;
                pnReceive.VerticalScroll.Value = this.pnReceive.VerticalScroll.Maximum;

            }
        }
        #endregion

        #region 我的私信关闭事件方法
        /// <summary>
        /// 我的私信关闭事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 我的私信接收信息事件方法
        /// <summary>
        /// 我的私信接收信息事件方法
        /// </summary>
        public void receive()
        {
            if (server == null)
            {
                server = new TcpListener(IPAddress.Parse("192.168.51.143"), 6005);
            }
            server.Start(10);//启动服务并且监听    

            Task.Factory.StartNew(() =>
            {
                client = server.AcceptTcpClient();//接受客户端连接
                //rtbMsg.AppendText("\r\n 客户端连接成功！");
                ns = client.GetStream();
                while (true)
                {
                    try
                    {

                        if (!ns.CanRead)//判断是否有数据接收
                            continue;
                        int buffer = ns.Read(data, 0, data.Length);
                        string s = Encoding.UTF8.GetString(data, 0, buffer);

                        JS(s);
                        SoundPlayer player = new SoundPlayer("msg.wav");
                        player.Play();

                }
                catch (Exception ex)
                {
                        //rtbMsg.AppendText("\r\n 错误：" + ex.Message);
                        Console.WriteLine(ex.Message);
                }
            }
            });
        }
        #endregion

        #region 首页居家日用事件方法
        /// <summary>
        /// 首页居家日用事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomedaily_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1000);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();

        }
        #endregion

        #region 我的私信接收信息事件方法
        /// <summary>
        /// 我的私信接收信息事件方法
        /// </summary>
        /// <param name="s"></param>
        public void JS(string s )
        {
            Action action = () =>
             {

                 PictureBox pic = new PictureBox();//创建头像框图片框对象
                pic.Size = new Size(30, 30); //设置大小
                pic.Location = new Point(0, i - 5); //定位
                pic.BackColor = Color.Red;

                 Label l = new Label();//创建label 聊天气泡框对象

                l.Location = new Point(40, i - 5);//定位
                l.BackColor = Color.FromArgb(18, 183, 245);
                 l.TextAlign = ContentAlignment.MiddleLeft;
                 l.Height = 35;
                 l.Width = s.Length * 17;

                 if (l.Width >= 170)
                 {
                     l.Location = new Point(40, i - 5);//定位
                    l.Width = 140;
                     l.Height = 50;

                 }
                 else if (l.Width >= 335)
                 {
                     l.Width = 200;
                     l.Height = 80;
                 }
                 l.Text = s;

                 l.Font = new Font("楷体", 10);
                 i += l.Height + 5;

                 PnA.Controls.Add(pic);
                 PnA.Controls.Add(l);


                 if (i >= 240)
                 {
                     pnlS.Height += l.Height + 5;
                     PnA.Height += l.Height + 5;
                     PnB.Height += l.Height + 5;
                     panel3.VerticalScroll.Value = this.panel3.VerticalScroll.Maximum;
                     panel3.VerticalScroll.Value = this.panel3.VerticalScroll.Maximum;
                 }
             };
            this.Invoke(action);
        }

        #endregion

        #region 首页运动户外事件方法
        /// <summary>
        /// 首页运动户外事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExercise_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1003);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }
        #endregion

        #region 首页动漫周边事件方法
        /// <summary>
        /// 首页动漫周边事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCartoon_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1005);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }
        #endregion

        #region 首页玩具乐器事件方法
        /// <summary>
        /// 首页玩具乐器事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToy_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1006);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }
        #endregion

        #region 首页电子商品事件方法
        /// <summary>
        /// 首页电子商品事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBike_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1001);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }
        #endregion

        #region 首页箱包事件方法
        /// <summary>
        /// 首页箱包事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCadastral_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1004);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }

        #endregion

        #region 首页闲置书籍事件方法
        /// <summary>
        /// 首页闲置事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdlebooks_Click(object sender, EventArgs e)
        {
            Used_Goods = GoodsInfoManager.ClassifyGoodsInfo(1002);
            Panels();
            PictureBoxs();
            LabelMoney();
            LabelDescription();
        }

        #endregion

        private void tabToolbar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabToolbar.SelectedIndex ==0)
            {
               // MessageBox.Show("首页");
            }
            if (tabToolbar.SelectedIndex == 1)
            {
              //  MessageBox.Show("发布闲置");
            }
            if (tabToolbar.SelectedIndex == 3)
            {

                List<shoppingInfo> list = shoppingManager.GetShoppingInfos();
                loadShoppingCart(list.Count, list);
            }
        }

        #region 打开文件夹事件方法
        /// <summary>
        /// 打开文件夹事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSendFile_Click(object sender, EventArgs e)
        {
            //选择显示图片操作

            OpenFileDialog openFi = new OpenFileDialog();
            openFi.Filter = "图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)"
                + "|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)"
                + "| *.png |所有文件(*.*)|*.*";
            if (openFi.ShowDialog() == DialogResult.OK)
            {


                Image img = Image.FromFile(openFi.FileName);
                     
                Clipboard.SetDataObject(img);          
                rtbsend.Paste(DataFormats.GetFormat(DataFormats.Bitmap));

                //FileStream fs = new FileStream(openFi.FileName, FileMode.Open);

                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6006); 
                //socket.Connect(ipep);
                //long contentLength = fs.Length;
                ////第一次发送数据包的大小
                //socket.Send(BitConverter.GetBytes(contentLength));
                //while (true)
                //{
                //    //每次发送128字节
                //    byte[] bits = new byte[128];
                //    int r = fs.Read(bits, 0, bits.Length);
                //    if (r <= 0) break;
                //    socket.Send(bits, r, SocketFlags.None);
                //}
                //socket.Close();
                //fs.Position = 0;
            }
        }
        #endregion

       

        #region 发送图片事件
        /// <summary>
        /// 发送图片事件
        /// </summary>
        private void SendImage()
        {
            //实例化socket    
           
        }
        #endregion

        #region 发布闲置
        /// <summary>
        /// 发布闲置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, EventArgs e)
        {
            GoodsInfo goods = new GoodsInfo
            {
                Gname = txtName.Text,
                Gtype = Convert.ToInt32(cboType.SelectedItem),
                Gprice = Convert.ToDouble(txtPrice.Text),
                Gseller = Convert.ToInt32(txtSeller.Text),
                Gstatus = Convert.ToBoolean(0),
                Gdescription = txtDescrip.Text,
                GDateTime = dtpDate.Value,
                Gpicture = picPicture.ImageLocation,
            };
            MessageBox.Show(txtDescrip.Text);
            GoodsInfoManager.GoodsAdd(goods);


        }

        #endregion

        #region 打开文件夹选择图片
        /// <summary>
        /// 打开文件夹选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPicture_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdImage.ShowDialog();
            if (DialogResult.Cancel != dr)
            {
                picPicture.ImageLocation = ofdImage.FileName;
            }
        }
        #endregion


        public void loadShoppingCart(int s, List<shoppingInfo> list)
        {
            panel2.Controls.Clear();

            for (int i = 0; i < s; i++)
            {
                int num = 0;
                UclCars use = new UclCars();
                use.Size = new Size(608, 148);

                use.BackColor = Color.WhiteSmoke;
                use.BorderStyle = BorderStyle.FixedSingle;
                use.Location = new Point(0, i * 148);
                panel2.Controls.Add(use);
                PictureBox p = (PictureBox)use.Controls["panel1"].Controls["picCars"];
                //p.BackColor = Color.Blue;
                p.Image = Image.FromFile(list[i].ProPicture);
                p.SizeMode = PictureBoxSizeMode.Zoom;
                Label lbl1 = (Label)use.Controls["panel1"].Controls["lblName"];
                lbl1.Text = list[i].ProName;
                Label lbl2 = (Label)use.Controls["panel1"].Controls["lblPrice"];
                lbl2.Text = list[i].ProPrice.ToString();
                Label lbl3 = (Label)use.Controls["panel1"].Controls["lblDescription"];
                lbl3.Text = "备注：物美价廉";



                //选择
                CheckBox chk = (CheckBox)use.Controls["panel1"].Controls["chkSelect"];
                chk.CheckedChanged += (sender, e) =>
                {
                    if (chk.Checked)
                    {
                        TotalPrice += Convert.ToInt32(lbl2.Text);
                        label29.Text = TotalPrice + "￥";
                        sumber++;
                        label28.Text = sumber.ToString();
                    }
                    else
                    {
                        TotalPrice -= Convert.ToInt32(lbl2.Text);
                        label29.Text = TotalPrice + "￥";
                        sumber--;
                        label28.Text = sumber.ToString();
                    }
                };

                //删除
                Button btn_Delete = (Button)use.Controls["panel1"].Controls["panel2"].Controls["btnDelete"];
                btn_Delete.Tag = list[i].shoppingId;
                btn_Delete.Click += Btn_Delete_Click;

                Label lbl4 = (Label)use.Controls["panel1"].Controls["lblNum"];
                //增加数量
                Button btn_Add = (Button)use.Controls["panel1"].Controls["btnAdd"];
                btn_Add.Click += (sender, e) => {
                    num++;
                    lbl4.Text = num.ToString();
                };
                //减少数量
                Button btn_Minus = (Button)use.Controls["panel1"].Controls["btnMinus"];
                btn_Minus.Click += (sender, e) => {
                    if (!(num == 0))
                    {
                        num--;
                        lbl4.Text = num.ToString();
                    }
                };
            }

        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            DialogResult rest = MessageBox.Show("是否删除？", "删除购物车", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rest == DialogResult.Yes)
            {
                shoppingManager.shoppingDelete((int)b.Tag);
                List<shoppingInfo> list = shoppingManager.GetShoppingInfos();
                loadShoppingCart(list.Count, list);
            }
        }


        #region 支付事件方法
        /// <summary>
        /// 支付事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FrmCodes frm=new FrmCodes();
            frm.ShowDialog();
        }
        #endregion


    }
}
