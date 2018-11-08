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
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotkey(IntPtr hWnd,  int id);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        private Int32 hotMod = 0x0001 | 0x4000;//alt no repete
        private Int32 captureKey = 0x08;// backspace
        private Int32 areaTopKey = 0xDB;//[
        private Int32 areaBottomKey = 0xDD;//]
        private const Int32 hotCaptureID = 0;
        private const Int32 hotTopID = 1;
        private const Int32 hotBotID = 2;
        private SpeechSynthesizer reader = new SpeechSynthesizer();
        private Regex filter = new Regex(@"\s +| _ *");
        private POINT topPoint = new POINT(-5000,-5000);
        private POINT botPoint = new POINT(-5000, -5000);
        private const int statusPause = 2;
        private const int statusSpeaking = 1;
        private const int statusReady = 0;
        private int readerStatus = statusReady;

        public Form1()
        {
            InitializeComponent();
            var test = this.GetType().GetHashCode();
            var test2 = this.Handle;
            Boolean success = Form1.RegisterHotKey(this.Handle, hotCaptureID, hotMod, captureKey);
            success = Form1.RegisterHotKey(this.Handle, hotTopID, hotMod, areaTopKey);
            success = Form1.RegisterHotKey(this.Handle, hotBotID, hotMod, areaBottomKey);
            reader.SpeakStarted += SpeakStart;
            reader.SpeakCompleted += SpeakFinish;
            statusLbl.Text = "Ready";
        }

        private void SpeakStart(object sender, SpeakStartedEventArgs e)
        {
            statusLbl.Text = "Speaking";
            readerStatus = statusSpeaking;
            stopVoiceBtn.Enabled = true;
            pausePlayBtn.Enabled = true;
            rateNum.Enabled = false;
            volumeNum.Enabled = false;
        }

        private void SpeakFinish(object sender, SpeakCompletedEventArgs e)
        {
            statusLbl.Text = "Ready";
            readerStatus = statusReady;
            stopVoiceBtn.Enabled = false;
            pausePlayBtn.Enabled = false;
            rateNum.Enabled = true;
            volumeNum.Enabled = true;
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            Form1.UnregisterHotkey(this.Handle,hotCaptureID);
            Form1.UnregisterHotkey(this.Handle, hotTopID);
            Form1.UnregisterHotkey(this.Handle, hotBotID);
        }

        protected override void WndProc(ref Message m)
        {
            string text = "";
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == hotCaptureID)
                {
                    Bitmap image;
                    foundText.Text = "";
                    Speak("Processing");
                    if (topPoint.X <= -5000 || topPoint.Y <= -5000 || botPoint.X <= -5000 || botPoint.Y <= -5000)
                    {
                        
                        image = ScreenCapture.CaptureActiveWindow();
                        
                        
                    }
                    else
                    {
                        image = ScreenCapture.CaptureArea(topPoint.X, topPoint.Y, botPoint.X, botPoint.Y);
                    }
                    //pictureBox1.Image = image;
                    text = ReadImage(image);
                    text = filter.Replace(text, "");
                    Speak(text);

                }
                else if (id == hotTopID)
                {
                    GetCursorPos(out topPoint);
                    Speak("Top Left Captured");
                    if (botPoint.X < topPoint.X && botPoint.Y < topPoint.Y)
                    {
                        botPoint.X = topPoint.X + 1;
                        botPoint.Y = topPoint.Y + 1;
                        Speak("Your bottom right point has been reset");
                    }
                }
                else if (id == hotBotID)
                {
                    POINT tempPoint;

                    GetCursorPos(out tempPoint);
                    if(tempPoint.X > topPoint.X && tempPoint.Y > topPoint.Y)
                    {
                        botPoint = tempPoint;
                        Speak("Bottom Right Captured");
                    }
                    else
                    {
                        Speak("Your second point is above or left of your first point");
                    }
                }
                

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

        private void pausePlayBtn_Click(object sender, EventArgs e)
        {
            if (readerStatus == statusSpeaking)
            {
                reader.Pause();
                statusLbl.Text = "Paused";
                readerStatus = statusPause;
                pausePlayBtn.Text = "Resume";
            }
            else if(readerStatus == statusPause)
            {
                reader.Resume();
                statusLbl.Text = "Speaking";
                readerStatus = statusSpeaking;
                pausePlayBtn.Text = "Pause";
            }
        }

        private void stopVoiceBtn_Click(object sender, EventArgs e)
        {
            reader.SpeakAsyncCancelAll();
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

        public static Bitmap CaptureArea(int left, int top, int right, int bottom)
        {
            IntPtr handle = GetForegroundWindow();
            var bounds = new Rectangle(left, top, right - left, bottom - top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
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
