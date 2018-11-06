using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyViewer {
    public partial class Form1 : Form, KeyEventListener {
        private KeyHooker keyHooker = null;

        public Form1(string[] args) {
            InitializeComponent();
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "/t") {
                    SetOnTop(true);
                } else {
                    if (args[i] == "/o") {
                        if (args.Length > i) {
                            SetOpacity(int.Parse(args[i + 1]));
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            keyHooker = new KeyHooker();
            keyHooker.SetKeyEventListener(this);
            keyHooker.SetHook();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            keyHooker.UnHook();
        }

        public void OnKeyDown(int keyCode) {
            //Console.WriteLine("Key Down : " + keyCode);

            SetKeyColor(keyCode, true);
        }

        public void OnKeyUp(int keyCode) {
            //Console.WriteLine("Key Up : " + keyCode);

            SetKeyColor(keyCode, false);
        }

        private void SetKeyColor(int keyCode, bool down) {
            Color color = down ? Color.FromArgb(100, 100, 100) : Color.FromArgb(240, 240, 240);

            switch (keyCode) {
                // 1p

                case 192:
                    key1.BackColor = color;
                    break;
                case 49:
                    key2.BackColor = color;
                    break;
                case 50:
                    key3.BackColor = color;
                    break;
                case 51:
                    key4.BackColor = color;
                    break;
                case 52:
                    key5.BackColor = color;
                    break;
                case 53:
                    key6.BackColor = color;
                    break;
                case 54:
                    key7.BackColor = color;
                    break;

                case 9:
                    key8.BackColor = color;
                    break;
                case 81:
                    key9.BackColor = color;
                    break;
                case 87:
                    key10.BackColor = color;
                    break;
                case 69:
                    key11.BackColor = color;
                    break;
                case 82:
                    key12.BackColor = color;
                    break;
                case 84:
                    key13.BackColor = color;
                    break;

                case 20:
                    key14.BackColor = color;
                    break;
                case 65:
                    key15.BackColor = color;
                    break;
                case 83:
                    key16.BackColor = color;
                    break;
                case 68:
                    key17.BackColor = color;
                    break;
                case 70:
                    key18.BackColor = color;
                    break;
                case 71:
                    key19.BackColor = color;
                    break;

                case 160:
                    key20.BackColor = color;
                    break;
                case 90:
                    key21.BackColor = color;
                    break;
                case 88:
                    key22.BackColor = color;
                    break;
                case 67:
                    key23.BackColor = color;
                    break;
                case 86:
                    key24.BackColor = color;
                    break;

                case 162:
                    key25.BackColor = color;
                    break;
                case 91:
                    key26.BackColor = color;
                    break;
                case 164:
                    key27.BackColor = color;
                    break;
                case 32:
                    key28.BackColor = color;
                    break;

                // 2p

                case 8:
                    key29.BackColor = color;
                    break;
                case 144:
                    key30.BackColor = color;
                    break;
                case 111:
                    key31.BackColor = color;
                    break;
                case 106:
                    key32.BackColor = color;
                    break;
                case 109:
                    key33.BackColor = color;
                    break;

                case 220:
                    key34.BackColor = color;
                    break;
                case 103:
                    key35.BackColor = color;
                    break;
                case 104:
                    key36.BackColor = color;
                    break;
                case 105:
                    key37.BackColor = color;
                    break;
                case 107:
                    key38.BackColor = color;
                    break;

                case 13:
                    key39.BackColor = color;
                    break;
                case 100:
                    key40.BackColor = color;
                    break;
                case 101:
                    key41.BackColor = color;
                    break;
                case 102:
                    key42.BackColor = color;
                    break;

                case 161:
                    key43.BackColor = color;
                    break;
                case 38:
                    key44.BackColor = color;
                    break;
                case 97:
                    key45.BackColor = color;
                    break;
                case 98:
                    key46.BackColor = color;
                    break;
                case 99:
                    key47.BackColor = color;
                    break;
                case 10013:
                    key48.BackColor = color;
                    break;

                case 25:
                    key49.BackColor = color;
                    break;
                case 37:
                    key50.BackColor = color;
                    break;
                case 40:
                    key51.BackColor = color;
                    break;
                case 39:
                    key52.BackColor = color;
                    break;
                case 96:
                    key53.BackColor = color;
                    break;
                case 110:
                    key54.BackColor = color;
                    break;
            }
        }

        private void opacityBar_Scroll(object sender, EventArgs e) {
            if (opacityBar.Value < 20) {
                opacityBar.Value = 20;
            }

            this.Opacity = ((double) opacityBar.Value) / 100.0;

        }

        private void alwaysTopChk_CheckedChanged(object sender, EventArgs e) {
            this.TopMost = alwaysTopChk.Checked;
        }

        private void SetOpacity(int value) {
            opacityBar.Value = value;
            this.Opacity = ((double) opacityBar.Value) / 100.0;
        }

        private void SetOnTop(bool top) {
            alwaysTopChk.Checked = top;
            this.TopMost = alwaysTopChk.Checked;
        }
    }
}
