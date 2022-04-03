using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Board {

        private Pieces[,] _pieces;
        private Dictionary<string, bool> _inCheck;

        public Pieces[,] AllPieces { 
            get { return _pieces; } 
        }

        public Board () {
            _pieces = new Pieces[8,8];
            _pieces[0, 0] = new Rook("Black", 0, 0);
            _pieces[0, 1] = new Knight("Black", 0, 1);
            _pieces[0, 2] = new Bishop("Black", 0, 2);
            _pieces[0, 3] = new Queen("Black", 0, 3);
            _pieces[0, 4] = new King("Black", 0, 4);
            _pieces[0, 5] = new Bishop("Black", 0, 5);
            _pieces[0, 6] = new Knight("Black", 0, 6);
            _pieces[0, 7] = new Rook("Black", 0, 7);
            _pieces[7, 0] = new Rook("White", 7, 0);
            _pieces[7, 1] = new Knight("White", 7, 1);
            _pieces[7, 2] = new Bishop("White", 7, 2);
            _pieces[7, 3] = new Queen("White", 7, 3);
            _pieces[7, 4] = new King("White", 7, 4);
            _pieces[7, 5] = new Bishop("White", 7, 5);
            _pieces[7, 6] = new Knight("White", 7, 6);
            _pieces[7, 7] = new Rook("White", 7, 7);
            for (int i=0; i<8; i++) {
                _pieces[1, i] = new Pawn("Black", 1, i);
                _pieces[6, i] = new Pawn("White", 6, i);
            }
            _inCheck = new Dictionary<string, bool> { { "White", false}, { "Black", false} };
        }

        public Board (Board board) {
            _pieces = new Pieces[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    Pieces tmp = board.PieceAt(i, j);
                    _pieces[i, j] = tmp.Color == "null" ? null : tmp;
                }
            }
        }

        public Board (string fen) {

        }

        public bool InCheck (string color) {
            return _inCheck[color];
        }

        public Pieces PieceAt (int row, int col) {
            return _pieces[row, col] ?? new Pawn("null", row, col);
        }

        public void SetPieceAt (int row, int col, Pieces piece) {
            _pieces[row, col] = piece;
            if (piece != null) {
                piece.CurrentRow = row;
                piece.CurrentCol = col;
            }
        }

        public (int i, int j) KingPosition (string color) {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (this.PieceAt(i, j).Color == color && this.PieceAt(i,j).Name == "King") {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        private void CalcCheck(string color) {
            (int i, int j) kingPos = KingPosition(color);
            string opColor = color == "White" ? "Black" : "White";

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (PieceAt(i,j).Color == opColor && PieceAt(i, j).Moves(this, color, true).Contains(kingPos)) {
                        _inCheck[color] = true;
                        return;
                    }
                }
            }
            _inCheck[color] = false;
        }

        public Dictionary<string, bool> MovePiece (Pieces start, Pieces destination) {
            Dictionary<string, bool> flags = new Dictionary<string, bool>();
            string opColor = start.Color == "White" ? "Black" : "White";
            int x = start.CurrentRow;
            int y = start.CurrentCol;
            SetPieceAt(destination.CurrentRow, destination.CurrentCol, start);
            SetPieceAt(x, y, null);
            CalcCheck(opColor);

            flags["In Check"] = InCheck(opColor);
            flags["Checkmate"] = false; //to implement
            flags["Captured"] = destination.Color != "null";

            return flags;
        }
    }
}
