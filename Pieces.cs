using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal abstract class Pieces {

        private string _name;
        private string _color;
        private float _pieceWorth;
        private int _currentRow;
        private int _currentCol;
        private System.Drawing.Image _image;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        public string Color {
            get { return _color; }
            set { _color = value; } 
        }
        public float PieceWorth {
            get { return _pieceWorth; }
            set { _pieceWorth = value; }
        }
        public int CurrentRow {
            get { return _currentRow; }
            set { _currentRow = value; }
        }
        public int CurrentCol {
            get { return _currentCol; }
            set { _currentCol = value; }
        }
        public System.Drawing.Image Image {
            get { return _image; }
            set { _image = value; }
        }

        public abstract List<(int i, int j)> PossibleMoves(Board board);

        public bool IsWithinBoard (int row, int col) {
            if (row >= 0 && row < 8 && col >= 0 && col < 8) {
                return true;
            }
            return false;
        }
    }
}
