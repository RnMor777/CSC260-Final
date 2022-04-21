using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace CSC260_Final {
    public partial class GameScreenForm : Form {

        private DataStore _store;
        //public static SemaphoreSlim WorkSignal;

        public GameScreenForm() {
            InitializeComponent();
        }

        private void GameScreenForm_Load(object sender, EventArgs e) {
            this.TransparencyKey = Color.Empty;

            _store = new DataStore(this);

            Button[,] arr = new Button[,] {
                { A8, B8, C8, D8, E8, F8, G8, H8},
                { A7, B7, C7, D7, E7, F7, G7, H7},
                { A6, B6, C6, D6, E6, F6, G6, H6},
                { A5, B5, C5, D5, E5, F5, G5, H5},
                { A4, B4, C4, D4, E4, F4, G4, H4},
                { A3, B3, C3, D3, E3, F3, G3, H3},
                { A2, B2, C2, D2, E2, F2, G2, H2},
                { A1, B1, C1, D1, E1, F1, G1, H1},
            };
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    AssignEvent(arr[i, j], i, j);
                }
            }

            _store.Renderer.ButtonArr = arr;
            _store.Renderer.BlackCaps = blackCap;
            _store.Renderer.WhiteCaps = whiteCap;
            _store.Renderer.CheckLabel = labelCheck;
            _store.Renderer.LetterArr = new Label[] { labelA, labelB, labelC, labelD, labelE, labelF, labelG, labelH };
            _store.Renderer.NumbArr = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8 };

            _store.TableHandler.AddTable(MoveTable);

            _store.Renderer.Render();
        }

        public void Start() {
            //WorkSignal = new SemaphoreSlim(0, 2);
            //Show();
            //Thread thr = new Thread(new ThreadStart(_store.Game.Run));
            //thr.Start();
            //Hide();
            ShowDialog();
            //thr.Abort();
        }

        private void AssignEvent (Button btn, int i, int j) {
            btn.MouseUp += (s, args) => {
                if (args.Button == MouseButtons.Right) {
                    _store.Game.EscapeMove();
                }
                else if (args.Button == MouseButtons.Left) {
                    if (!_store.Flip)
                        _store.Game.TakeInput(i, j);
                    else
                        _store.Game.TakeInput(7 - i, 7 - j);
                }
            };
        }

        private void undoBtn_Click(object sender, EventArgs e) {
            _store.Game.TakeUndo();
        }

        private void flipBtn_Click(object sender, EventArgs e) {
            _store.Game.FlipBoard();
        }

        private void RewindAll_Click(object sender, EventArgs e) {
            _store.TableHandler.GoBack(true);
        }

        private void ForwardAll_Click(object sender, EventArgs e) {
            _store.TableHandler.GoForward(true);
        }

        private void Rewind_Click(object sender, EventArgs e) {
            _store.TableHandler.GoBack(false);
        }

        private void Forward_Click(object sender, EventArgs e) {
            _store.TableHandler.GoForward(false);
        }
    }
}
