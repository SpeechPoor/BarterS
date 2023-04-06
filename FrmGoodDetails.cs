using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using BLL;

namespace Barter
{
    public partial class FrmGoodDetails : Form
    {

        List<GoodsInfo> goods = GoodsInfoManager.GetGoodsInfoGids(Convert.ToInt32(FrmMain.Gid));

        public static string  ID { get; set; }

        public static string GPrice { get; set; } 

        public string Gdescr;

        public FrmGoodDetails()
        {
            InitializeComponent();
        }

        private void FrmGoodDetails_Load(object sender, EventArgs e)
        {
            foreach (var i in goods)
            {
                picGoodTop.ImageLocation = i.Gpicture;
                //  pictureBox1.ImageLocation = i.Gpicture;
                lblMoney.Text = i.Gprice.ToString();
                Gdescr = i.Gdescription.ToString();

                GPrice= i.Gprice.ToString();
                // txtUid.Text = i.Gid.ToString();
                picGoodTop.Tag = i.Gid;


            }

            Label lbl = new Label();
            lbl.Size = new Size(360, 240);
            lbl.Location = new Point(30, 380);
            lbl.Name = "lbl ";//控件Name border-radius
           // lbl.Text = "动漫神奇宝贝宠物小精灵皮卡丘手办公仔口袋妖怪玩具模型摆件 41款皮卡丘+展示盒+灯 实物拍摄";
            lbl.Text = Gdescr;
            lbl.Font = new Font("宋体", 20);
            pnlien.Controls.Add(lbl);

            Button btn = new Button();
            btn.Size = new Size(40, 40);
            btn.BackColor = Color.Red;
            btn.Location = new Point(350, 270);

            btn.Name = "btn ";//控件Name border-radius
            pnlien.Controls.Add(btn);

        }

        private void btnShopping_Click(object sender, EventArgs e)
        {
            shoppingInfo sh = new shoppingInfo();

            foreach (var j in goods)
            {            
                sh.ProId = j.Gid;
                sh.ProPicture = j.Gpicture;
                sh.ProName = j.Gname;
                sh.TypeId = j.Gtype;
                sh.ProPrice = j.Gprice;
            }

            //MessageBox.Show(sh.ProPicture);
            MessageBox.Show("加入购物车成功！");
            shoppingManager.shoppingAdd(sh);

        }

        private void btnpurchase_Click(object sender, EventArgs e)
        {
            Frmcode frm=new Frmcode();
            frm.ShowDialog();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
