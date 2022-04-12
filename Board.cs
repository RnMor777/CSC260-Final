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
        private (int i, int j) _enPassantSquare;

        public Pieces[,] AllPieces { 
            get { return _pieces; } 
        }

        public (int i, int j) EnPassant {
            get { return _enPassantSquare; }
            set { _enPassantSquare = value; }
        }

        public Board () {
            _pieces = new Pieces[8,8];
            _pieces[0, 0] = new Rook("Black", 0, 0);
            _pieces[0, 1] = new Knight("Black", 0, 1);
            _pieces[0, 2] = new Bishop("Black", 0, 2);
            _pieces[0, 3] = new Queen("Black", 0, 3);
            _pieces[0, 4] = new King("Black", 0, 4, true, true);
            _pieces[0, 5] = new Bishop("Black", 0, 5);
            _pieces[0, 6] = new Knight("Black", 0, 6);
            _pieces[0, 7] = new Rook("Black", 0, 7);
            _pieces[7, 0] = new Rook("White", 7, 0);
            _pieces[7, 1] = new Knight("White", 7, 1);
            _pieces[7, 2] = new Bishop("White", 7, 2);
            _pieces[7, 3] = new Queen("White", 7, 3);
            _pieces[7, 4] = new King("White", 7, 4, true, true);
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
            _pieces = new Pieces[8, 8];
            int row = 0;
            int col = 0;
            int index = 0;
            while (fen[index] != ' ') {
                if (fen[index] == '/') {
                    row++;
                    col = 0;
                }
                else if (fen[index] < (char)58) {
                    col += (int)(fen[index] - 48);
                }
                else {
                    Pieces created;
                    char upper = (char)fen[index];

                    switch (upper) {
                        case 'R':
                            created = new Rook("White", row, col);
                            break;
                        case 'r':
                            created = new Rook("Black", row, col);
                            break;
                        case 'N':
                            created = new Knight("White", row, col);
                            break;
                        case 'n':
                            created = new Knight("Black", row, col);
                            break;
                        case 'K':
                            created = new King("White", row, col);
                            break;
                        case 'k':
                            created = new King("Black", row, col);
                            break;
                        case 'B':
                            created = new Bishop("White", row, col);
                            break;
                        case 'b':
                            created = new Bishop("Black", row, col);
                            break;
                        case 'Q':
                            created = new Queen("White", row, col);
                            break;
                        case 'q':
                            created = new Queen("Black", row, col);
                            break;
                        case 'P':
                            created = new Pawn("White", row, col);
                            break;
                        default:
                            created = new Pawn("Black", row, col);
                            break;
                    }
                    _pieces[row, col] = created;
                    col++;
                }
                index++;
            }
        }

        public bool InCheck (string color) {
            return _inCheck[color];
        }

        public Pieces PieceAt (int row, int col) {
            return _pieces[row, col] ?? new Pawn("null", row, col);
        }

        public Pieces PieceAt ((int i, int j) pos) {
            return _pieces[pos.i, pos.j] ?? new Pawn("null", pos.i, pos.j);
        }

        public void SetPieceAt (int row, int col, Pieces piece) {
            _pieces[row, col] = piece;
            if (piece != null) {
                piece.CurrentRow = row;
                piece.CurrentCol = col;
            }
        }

        public void SetPieceAt ((int i, int j) pos, Pieces piece) {
            _pieces[pos.i, pos.j] = piece;
            if (piece != null) {
                piece.CurrentRow = pos.i;
                piece.CurrentCol = pos.j;
            }
        }

        public void SetPieceAt (Pieces piece) {
            _pieces[piece.CurrentRow, piece.CurrentCol] = piece;
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
            SetPieceAt(destination.Position, start);
            SetPieceAt(x, y, null);
            CalcCheck(opColor);

            flags["In Check"] = InCheck(opColor);
            flags["Checkmate"] = false; //to implement
            flags["Captured"] = destination.Color != "null";
            flags["EnPassant"] = false;
            flags["Castle"] = false;

            if (destination.CurrentRow == _enPassantSquare.i && destination.CurrentCol == _enPassantSquare.j) {
                SetPieceAt(x + (destination.CurrentRow - x) / 2, destination.CurrentCol, null);
                flags["Captured"] = true;
                flags["EnPassant"] = true;
            }

            if (start.Name == "King" && Math.Abs(y-start.CurrentCol) == 2) {
                if (start.CurrentCol < y) {
                    SetPieceAt(start.CurrentRow, start.CurrentCol + 1, PieceAt(start.CurrentRow, 0));
                    SetPieceAt(start.CurrentRow, 0, null);
                }
                else {
                    SetPieceAt(start.CurrentRow, start.CurrentCol - 1, PieceAt(start.CurrentRow, 7));
                    SetPieceAt(start.CurrentRow, 7, null);
                }
                ((King)start).CastleLeft = false;
                ((King)start).CastleRight = false;
                flags["Castle"] = true;
            }

            if (start.Name == "King") {
                ((King)start).CastleLeft = false;
                ((King)start).CastleRight = false;
            }

            if (start.Name == "Rook") {
                if (y == 0) {
                    ((King)PieceAt(KingPosition(start.Color))).CastleLeft = false;
                }
                else if (y == 7) {
                    ((King)PieceAt(KingPosition(start.Color))).CastleRight = false;
                }
            }

            if (start.Name.Equals("Pawn") && Math.Abs(destination.CurrentRow - x) == 2) 
                _enPassantSquare = (x + (destination.CurrentRow - x) / 2, start.CurrentCol);
            else 
                _enPassantSquare = (-1, -1);

            return flags;
        }
    }
}
