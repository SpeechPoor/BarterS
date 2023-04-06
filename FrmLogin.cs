using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using Models;
using BLL;
namespace Barter
{
    public partial class FrmLogin : Form
    {
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);


        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        public static string UserName { get; set; }

        public static int UserID { get; set; }

        public static string userLogin { get; set; }
        public static string userPwd { get; set; }

        public FrmLogin()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }


        #region 登录事件方法
        /// <summary>
        /// 登录事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtLogin.Text;
            string pwd = txtPwd.Text;

            //MessageBox.Show(user);

            List<UserInfo> UsInfo = UserInfoManager.GetUserinfo(user);

            foreach (var i in UsInfo)
            {
                userLogin = i.Ulogin;
                userPwd = i.Upass;

               // MessageBox.Show(i.Uname);

                UserName = i.Uname;
                UserID = i.Uid;
                

                if (user==userLogin && pwd==userPwd)
                {
                    FrmMain frm = new FrmMain();
                    frm.ShowDialog();
                    this.Hide();
                }
            }

        }
        #endregion

        #region 退出事件方法
        /// <summary>
        /// 退出事件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                ReleaseCapture();
               // SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION);
            }
        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color FColor = Color.Blue;

            Color TColor = Color.Brown;

            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.ForwardDiagonal);

            g.FillRectangle(b, this.ClientRectangle);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void panel5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
           DialogResult dr=MessageBox.Show("您确定要退出程序吗？","系统提示",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            } 
                return;
        }
    }
}
