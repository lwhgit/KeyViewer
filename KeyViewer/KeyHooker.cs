using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;

using System.Runtime.InteropServices;


namespace KeyViewer {
    public class KeyHooker {

        private static KeyEventListener keyEventListener = null;

        public KeyHooker() {
            
        }

        public void SetKeyEventListener(KeyEventListener listener) {
            keyEventListener = listener;
        }


        // ... { GLOBAL HOOK }
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13; // Номер глобального LowLevel-хука на клавиатуру
        const int WM_KEYDOWN = 0x100; // Сообщения нажатия клавиши
        const int WM_KEYUP = 0x101;

        private LowLevelKeyboardProc _proc = hookProc;

        private static IntPtr hhook = IntPtr.Zero;

        public void SetHook() {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public void UnHook() {
            UnhookWindowsHookEx(hhook);
        }

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam) {
            int vkCode = Marshal.ReadInt32(lParam);
            int tag = Marshal.ReadInt32(lParam, 8);

            if (vkCode == 46) {
                vkCode = 110;
            } else if (vkCode == 13) {
                if (tag == 1 || tag == 129) {
                    vkCode += 10000;
                }
            }

            if (code >= 0) {
                //////ОБРАБОТКА НАЖАТИЯ
                //if (vkCode.ToString() == "162") {
                //    MessageBox.Show("You pressed a CTR");
                //}
                if (wParam == (IntPtr) WM_KEYDOWN) {
                    keyEventListener.OnKeyDown(vkCode);
                } else if (wParam == (IntPtr) WM_KEYUP || wParam == (IntPtr) 261) {
                    keyEventListener.OnKeyUp(vkCode);
                }
                if (wParam == (IntPtr) 260) {
                    keyEventListener.OnKeyDown(vkCode);
                } else if (wParam == (IntPtr) 257) {
                    keyEventListener.OnKeyUp(vkCode);
                }
                
            }

            //Console.WriteLine("Code: " + code + "\t\tKey : " + vkCode + "\t\twParam: " + wParam + "\t\ta: " + tag);

            return CallNextHookEx(hhook, code, (int) wParam, lParam);
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            // убираем хук
            UnHook();

        }

        private void Form1_Load(object sender, EventArgs e) {
            // Устанавливаем хук
            SetHook();
        }
    }

    public interface KeyEventListener {
        void OnKeyDown(int keyCode);
        void OnKeyUp(int keyCode);
    }
}