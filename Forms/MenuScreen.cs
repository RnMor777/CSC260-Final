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
    public partial class Start : Form {
        public Start() {
            InitializeComponent();
        }

        private void MenuScreen_Load(object sender, EventArgs e) {
        }

        private void Play_Click(object sender, EventArgs e) {
            GameScreenForm f = new GameScreenForm();
            Hide();
            f.ShowDialog();
            Close();
        }

        private void Quit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
