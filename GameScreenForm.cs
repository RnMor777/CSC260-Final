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

        public static Button[,] BtnArr {
            get { return _btnArr; }
        }

        public GameScreenForm() {
            InitializeComponent();
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
        }

        private void GameScreenForm_Load(object sender, EventArgs e) {
            this.TransparencyKey = Color.Empty;
            Game game = new Game();
            game.Run();
        }

        #region Board Click Events
        private void A8_Click(object sender, EventArgs e) {

        }

        private void A7_Click(object sender, EventArgs e) {

        }

        private void A6_Click(object sender, EventArgs e) {

        }

        private void A5_Click(object sender, EventArgs e) {

        }

        private void A4_Click(object sender, EventArgs e) {

        }

        private void A3_Click(object sender, EventArgs e) {

        }

        private void A2_Click(object sender, EventArgs e) {

        }

        private void A1_Click(object sender, EventArgs e) {

        }

        private void B8_Click(object sender, EventArgs e) {

        }

        private void B7_Click(object sender, EventArgs e) {

        }

        private void B6_Click(object sender, EventArgs e) {

        }

        private void B5_Click(object sender, EventArgs e) {

        }

        private void B4_Click(object sender, EventArgs e) {

        }

        private void B3_Click(object sender, EventArgs e) {

        }

        private void B2_Click(object sender, EventArgs e) {

        }

        private void B1_Click(object sender, EventArgs e) {

        }

        private void C8_Click(object sender, EventArgs e) {

        }

        private void C7_Click(object sender, EventArgs e) {

        }

        private void C6_Click(object sender, EventArgs e) {

        }

        private void C5_Click(object sender, EventArgs e) {

        }

        private void C4_Click(object sender, EventArgs e) {

        }

        private void C3_Click(object sender, EventArgs e) {

        }

        private void C2_Click(object sender, EventArgs e) {

        }

        private void C1_Click(object sender, EventArgs e) {

        }

        private void D8_Click(object sender, EventArgs e) {

        }

        private void D7_Click(object sender, EventArgs e) {

        }

        private void D6_Click(object sender, EventArgs e) {

        }

        private void D5_Click(object sender, EventArgs e) {

        }

        private void D4_Click(object sender, EventArgs e) {

        }

        private void D3_Click(object sender, EventArgs e) {

        }

        private void D2_Click(object sender, EventArgs e) {

        }

        private void D1_Click(object sender, EventArgs e) {

        }

        private void E8_Click(object sender, EventArgs e) {

        }

        private void E7_Click(object sender, EventArgs e) {

        }

        private void E6_Click(object sender, EventArgs e) {

        }

        private void E5_Click(object sender, EventArgs e) {

        }

        private void E4_Click(object sender, EventArgs e) {

        }

        private void E3_Click(object sender, EventArgs e) {

        }

        private void E2_Click(object sender, EventArgs e) {

        }

        private void E1_Click(object sender, EventArgs e) {

        }

        private void F8_Click(object sender, EventArgs e) {

        }

        private void F7_Click(object sender, EventArgs e) {

        }

        private void F6_Click(object sender, EventArgs e) {

        }

        private void F5_Click(object sender, EventArgs e) {

        }

        private void F4_Click(object sender, EventArgs e) {

        }

        private void F3_Click(object sender, EventArgs e) {

        }

        private void F2_Click(object sender, EventArgs e) {

        }

        private void F1_Click(object sender, EventArgs e) {

        }

        private void G8_Click(object sender, EventArgs e) {

        }

        private void G7_Click(object sender, EventArgs e) {

        }

        private void G6_Click(object sender, EventArgs e) {

        }

        private void G5_Click(object sender, EventArgs e) {

        }

        private void G4_Click(object sender, EventArgs e) {

        }

        private void G3_Click(object sender, EventArgs e) {

        }

        private void G2_Click(object sender, EventArgs e) {

        }

        private void G1_Click(object sender, EventArgs e) {

        }

        private void H8_Click(object sender, EventArgs e) {

        }

        private void H7_Click(object sender, EventArgs e) {

        }

        private void H6_Click(object sender, EventArgs e) {

        }

        private void H5_Click(object sender, EventArgs e) {

        }

        private void H4_Click(object sender, EventArgs e) {

        }

        private void H3_Click(object sender, EventArgs e) {

        }

        private void H2_Click(object sender, EventArgs e) {

        }

        private void H1_Click(object sender, EventArgs e) {

        }
        #endregion
    }
}
