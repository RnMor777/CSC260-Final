using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    public partial class GameScreenForm : Form {

        private static Button[,] _btnArr;
        private static Label _labelCheck;
        private static Label _whiteCaps;
        private static Label _blackCaps;
        private static TableLayoutPanel _moveTable;
        private Game _game;

        public static Button[,] BtnArr {
            get { return _btnArr; }
        }

        public static Label WhiteCaps {
            get { return _whiteCaps; }
        }

        public static Label BlackCaps {
            get { return _blackCaps; }
        }

        public static TableLayoutPanel MovementTable {
            get { return _moveTable; }
        }

        public GameScreenForm() {
            InitializeComponent();
        }

        private void GameScreenForm_Load(object sender, EventArgs e) {
            //System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
            //int fontLength = Properties.Resources.FreeSerif.Length;
            //byte[] fontdata = Properties.Resources.FreeSerif;
            //System.IntPtr data = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontLength);
            //System.Runtime.InteropServices.Marshal.Copy(fontdata, 0, data, fontLength);
            //pfc.AddMemoryFont(data, fontLength);
            //blackCap.Font = new Font(pfc.Families[0], blackCap.Font.Size);
            //whiteCap.Font = new Font(pfc.Families[0], whiteCap.Font.Size);
            _blackCaps = blackCap;
            _whiteCaps = whiteCap;
            _moveTable = MoveTable;
            _moveTable.AutoScroll = false;
            _moveTable.HorizontalScroll.Enabled = false;
            _moveTable.HorizontalScroll.Visible = false;
            _moveTable.AutoScroll = true;

            //tmpbox.Text = "♜♞♝♛♚♟";

            _game = new Game();

            this.TransparencyKey = Color.Empty;
            _labelCheck = labelCheck;
            _btnArr = new Button[,] {
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
                    AssignEvent(_btnArr[i, j], i, j);
                }
            }

            _game.Run();
        }

        private void AssignEvent (Button btn, int i, int j) {
            btn.MouseUp += (s, args) => {
                if (args.Button == MouseButtons.Right) {
                    _game.EscapeMove();
                }
                else if (args.Button == MouseButtons.Left) {
                    _game.AttemptMove(i, j);
                }
            };
        }

        public static void UpdateCheckLabel (string newText) {
            _labelCheck.Text = newText;
        }

        private void undoBtn_Click(object sender, EventArgs e) {
            _game.UndoMove();
        }
    }
}
