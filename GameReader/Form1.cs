using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using Tesseract;
using System.IO;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using System.Text.RegularExpressions;


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace GameReader
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        private Int32 hotMod = 0x0001 | 0x4000;
        private Int32 hotKey = 0x63;
        private SpeechSynthesizer reader = new SpeechSynthesizer();
        private Regex filter = new Regex(@"\s +| _ *");

        public Form1()
        {
            InitializeComponent();
            Boolean success = Form1.RegisterHotKey(this.Handle, this.GetType().GetHashCode(), hotMod, hotKey);//Set hotkey as alt+num3
        }
        
        protected override void WndProc(ref Message m)
        {
            string text = "";
            if (m.Msg == 0x0312)
            {
                foundText.Text = "";
                Bitmap image = ScreenCapture.CaptureActiveWindow();
                Speak("Processing");
                text = ReadImage(image);
                text = filter.Replace(text,"");
                Speak(text);

            }
            base.WndProc(ref m);
        }

        private void Speak(string passage)
        {
            reader.Volume =  (int)(decimal)volumeNum.Value;
            reader.Rate = (int)(decimal)rateNum.Value;

            reader.SpeakAsync(passage);
        }

        private String ReadImage(Bitmap image)
        {
            var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            string text = engine.Process(image).GetText();
            foundText.Text = text;
            return text;
        }

        private void Crop(object sender, EventArgs e)
        {
        }
    }

    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
    }
}
