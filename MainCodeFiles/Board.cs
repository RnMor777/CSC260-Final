using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Board {

        private List<Pieces> _piecesList;
        private Dictionary<string, bool> _inCheck;
        private (int i, int j) _enPassantSquare;

        protected List<Pieces> AllPieces {
            get { return _piecesList; }
        }

        public (int i, int j) EnPassant {
            get { return _enPassantSquare; }
            set { _enPassantSquare = value; }
        }

        public Board () {
            _piecesList = new List<Pieces> ();
            _piecesList.Add(new Rook("Black", 0, 0));
            _piecesList.Add(new Knight("Black", 0, 1));
            _piecesList.Add(new Bishop("Black", 0, 2));
            _piecesList.Add(new Queen("Black", 0, 3));
            _piecesList.Add(new King("Black", 0, 4, true, true));
            _piecesList.Add(new Bishop("Black", 0, 5));
            _piecesList.Add(new Knight("Black", 0, 6));
            _piecesList.Add(new Rook("Black", 0, 7));
            _piecesList.Add(new Rook("White", 7, 0));
            _piecesList.Add(new Knight("White", 7, 1));
            _piecesList.Add(new Bishop("White", 7, 2));
            _piecesList.Add(new Queen("White", 7, 3));
            _piecesList.Add(new King("White", 7, 4, true, true));
            _piecesList.Add(new Bishop("White", 7, 5));
            _piecesList.Add(new Knight("White", 7, 6));
            _piecesList.Add(new Rook("White", 7, 7));
            for (int i=0; i<8; i++) {
                _piecesList.Add(new Pawn("Black", 1, i));
                _piecesList.Add(new Pawn("White", 6, i));
            }
            _inCheck = new Dictionary<string, bool> { { "White", false}, { "Black", false} };
        }

        public Board (Board board) {
            _piecesList = new List<Pieces>();
            foreach (Pieces x in board.AllPieces) {
                _piecesList.Add(x.Copy());
            }
        }

        public Board (string fen) {
            _piecesList = new List<Pieces>();
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
                    _piecesList.Add(created);
                    col++;
                }
                index++;
            }
        }

        public bool InCheck (string color) {
            return _inCheck[color];
        }

        public Pieces PieceAt ((int i, int j) pos) {
            return _piecesList.Where(p => p.Position == pos).DefaultIfEmpty(new EmptyPiece(pos)).First();
        }

        public void SetPieceAt (Pieces piece) {
            SetPieceAt(piece.Position, piece);
        }

        public void SetPieceAt ((int i, int j) pos, Pieces piece) {
            _piecesList.RemoveAll(p => p.Position == pos);
            if (piece != null && piece.Color != "null") {
                piece.Position = pos;
                _piecesList.Add(piece);
            }
        }

        public (int i, int j) KingPosition (string color) {
            return _piecesList.Where(p => p.Color == color && p.Name == "King").First().Position;
        }

        private void CalcCheck(string color) {
            (int i, int j) kingPos = KingPosition(color);
            string opColor = Game.FlipColor(color);
            _inCheck[color] = _piecesList.Where(p => p.Color == opColor).Any(p => p.Moves(this, color, true).Contains(kingPos));
        }

        private bool CheckMate (string color) {
            return !_piecesList.Where(p => p.Color == color).ToList().Any(p => p.Moves(this, color, false).Count > 0);
        }

        public Dictionary<string, bool> MovePiece (Pieces start, Pieces destination) {
            Dictionary<string, bool> flags = new Dictionary<string, bool>();
            string opColor = Game.FlipColor(start.Color);
            int x = start.CurrentRow;
            int y = start.CurrentCol;
            SetPieceAt(destination.Position, start);
            SetPieceAt((x, y), null);
            CalcCheck(opColor);

            flags["In Check"] = InCheck(opColor);
            flags["Checkmate"] = false; 
            flags["Captured"] = destination.Color != "null";
            flags["EnPassant"] = false;
            flags["Castle"] = false;

            if (destination.Position == _enPassantSquare) { 
                SetPieceAt((x + (destination.CurrentRow - x) / 2, destination.CurrentCol), null);
                flags["Captured"] = true;
                flags["EnPassant"] = true;
            }

            if (start.Name == "King" && Math.Abs(y-start.CurrentCol) == 2) {
                if (start.CurrentCol < y) {
                    SetPieceAt((start.CurrentRow, start.CurrentCol + 1), PieceAt((start.CurrentRow, 0)));
                    SetPieceAt((start.CurrentRow, 0), null);
                }
                else {
                    SetPieceAt((start.CurrentRow, start.CurrentCol - 1), PieceAt((start.CurrentRow, 7)));
                    SetPieceAt((start.CurrentRow, 7), null);
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

            if (flags["In Check"]) 
                flags["Checkmate"] = CheckMate(opColor);

            return flags;
        }
    }
}
