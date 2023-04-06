using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Barter
{
    public partial class FrmCodes : Form
    {
        public FrmCodes()
        {
            InitializeComponent();
        }

        private void FrmCodes_Load(object sender, EventArgs e)
        {
            GenByZXingNet("您需要支付"+FrmMain.TotalPrice+"元");
        }

        private Bitmap GenByZXingNet(string msg)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//编码问题
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            const int codeSizeInPixels = 240;   //设置图片长宽
            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 1;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap img = writer.Write(bm);
            pictureBox1.Image = img;
            return img;
        }
    }
}
