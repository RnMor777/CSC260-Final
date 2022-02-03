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
        public GameScreenForm() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.TransparencyKey = Color.Empty;
        }
    }
}
