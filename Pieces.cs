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

        public bool Equals (Pieces other) {
            return _currentRow == other.CurrentRow && _currentCol == other.CurrentCol;
        }

        protected abstract List<(int i, int j)> PossibleMoves(Board board);

        public bool IsWithinBoard (int row, int col) {
            if (row >= 0 && row < 8 && col >= 0 && col < 8) {
                return true;
            }
            return false;
        }

        public List<(int i, int j)> Moves(Board board, string turn, bool all) {
            List<(int i, int j)> allMoves = PossibleMoves(board);
            if (all)
                return allMoves;

            int[] xmoves = { 1, 1,  1, 0, -1,  0, -1, -1, 1, 2,  2,  1, -1, -2, -2, -1 };
            int[] ymoves = { 1, 0, -1, 1,  1, -1, -1,  0, 2, 1, -1, -2,  2,  1, -1, -2 };
            bool[] noCheck = new bool[xmoves.Length];
            List<(int i, int j)> sievedMoves = new List<(int i, int j)>();
            string oppPlayer = turn == "White" ? "Black" : "White";
            int origX = this.CurrentRow;
            int origY = this.CurrentCol;
            int countNone = 0;
            Pieces tmp;
            bool sieve;
            int x, y;

            board.SetPieceAt(this.CurrentRow, this.CurrentCol, null);
            foreach ((int i, int j) in allMoves) {
                if (countNone == 16) {
                    board.SetPieceAt(origX, origY, this);
                    return allMoves;
                }

                tmp = board.PieceAt(i, j);
                if (tmp.Color == "null") 
                    tmp = null;

                sieve = false;
                board.SetPieceAt(i, j, this);
                (int i, int j) kingPos = board.KingPosition(turn);

                for (int k = 0; k < xmoves.Length; k++) {
                    if (noCheck[k]) continue;

                    x = kingPos.i;
                    y = kingPos.j;
                    do {
                        x += xmoves[k];
                        y += ymoves[k];
                    }
                    while (x >= 0 && x < 8 && y >= 0 && y < 8 && board.PieceAt(x, y).Color == "null" && k<8);
                    if (x < 0 || x > 7 || y < 0 || y > 7) {
                        noCheck[k] = true;
                        countNone += 1;
                        continue;
                    }

                    Pieces foundPiece = board.PieceAt(x, y);

                    List<String> names;
                    if (k >= 8) names = new List<String> { "Knight" };
                    else if (k % 2 == 0) names = new List<String> { "Queen", "Bishop" };
                    else names = new List<String> { "Queen", "Rook" };

                    if (foundPiece.Color == oppPlayer && names.Contains(foundPiece.Name))
                        sieve = true;
                    else if (!foundPiece.Equals(this) && this.Name != "King") {
                        noCheck[k] = true;
                        countNone += 1;
                    }
                }
                board.SetPieceAt(i, j, tmp);
                if (!sieve) sievedMoves.Add((i, j));
            }
            board.SetPieceAt(origX, origY, this);
            return sievedMoves;
        }
    }
}
