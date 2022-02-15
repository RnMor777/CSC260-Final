using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Board {

        private Pieces[,] _pieces;
        private int _wKingRow;
        private int _wKingCol;
        private int _bKingRow;
        private int _bKingCol;

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
            _wKingRow = 7;
            _wKingCol = 4;
            _bKingRow = 0;
            _bKingCol = 4;
        }

        public Board (Board board) {
            _pieces = new Pieces[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    Pieces tmp = board.PieceAt(i, j);
                    if (tmp.Color != "null") {
                        _pieces[i, j] = board.PieceAt(i, j);
                        if (tmp.Name == "King") {
                            if (tmp.Color == "White") {
                                _wKingRow = i;
                                _wKingCol = j;
                            }
                            else {
                                _bKingRow = i;
                                _bKingCol = j;
                            }
                        }
                    }
                    else
                        _pieces[i, j] = null;
                }
            }
        }

        public Board (string fen) {

        }

        public void Render () {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (_pieces[i,j] != null) {
                        GameScreenForm.BtnArr[i, j].Image = _pieces[i, j].Image; 
                    }
                    else {
                        GameScreenForm.BtnArr[i, j].Image = null;
                    }

                    if ((i+j)%2 == 0) {
                        GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.FromArgb(237, 209, 167);
                    }
                    else {
                        GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.FromArgb(191, 135, 73);
                    }
                }
            }
        }

        public Pieces PieceAt (int row, int col) {
            return _pieces[row, col] ?? new Pawn("null", -1, -1);
        }

        public void SetPieceAt (int row, int col, Pieces piece) {
            _pieces[row, col] = piece;
            if (piece != null && piece.Name == "King") {
                if (piece.Color == "White") {
                    _wKingRow = row;
                    _wKingCol = col;
                }
                else {
                    _bKingRow = row;
                    _bKingCol = col;
                }
            }
        }

        public bool WillCheck (int[,] moves, string color) {
            if (color == "White")
                return moves[_wKingRow, _wKingCol] == 1;
            else
                return moves[_bKingRow, _bKingCol] == 1;
        }
    }
}
