using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Moves {
        private Pieces _start;
        private Pieces _end;
        private Pieces _extra;
        private (int i, int j) _enpassantSquare;
        private (bool l, bool r) _castling;
        private StringBuilder _pgn;
        private string _fen;
        private Dictionary<String, bool> _flags;

        public String PGN {
            get { return _pgn.ToString(); }
        }

        public Pieces Start {
            get { return _start; }
        }

        public Pieces End {
            get { return _end; }
        }

        public Pieces Extra {
            get { return _extra; }
        }

        public Dictionary<String, bool> Flags {
            get { return _flags; }
        }

        public (int i, int j) EnPassantSquare {
            get { return _enpassantSquare; }
        }

        public (bool l, bool r) Castling {
            get { return _castling; }
            set { _castling = value; }
        }

        public string FEN {
            get { return _fen; }
            set { _fen = value; }
        }

        public Moves(Pieces orig, Pieces dest) {
            _flags = new Dictionary<string, bool>() { { "EnPassant", false}, { "Castle", false } };
            _start = orig.Copy();
            _end = dest.Copy();
            GeneratePGN();
        }

        public void AddEnpSquare ((int i, int j) enp) {
            _enpassantSquare = enp;
        }

        public void WasCheck () {
            _pgn = _pgn.Append("+");
        }

        public void WasMate () {
            _pgn = _pgn.Append("#");
        }

        public void WasPassant (int col) {
            _flags["EnPassant"] = true;
            _extra = new Pawn(_start.Color == "White" ? "Black" : "White", _start.CurrentRow, _end.CurrentCol);
            StringBuilder tmp = new StringBuilder();
            tmp.Append((char)(col + 97));
            tmp.Append('x');
            tmp.Append(_pgn);
            _pgn = tmp;
        }

        public void WasCastle (bool wasLeft) {
            _flags["Castle"] = true;
            if (wasLeft) {
                _extra = new Rook(_start.Color, _start.CurrentRow, 0);
                _pgn.Clear();
                _pgn.Append("O-O-O");
            }
            else {
                _extra = new Rook(_start.Color, _start.CurrentRow, 7);
                _pgn.Clear();
                _pgn.Append("O-O");
            }
        }

        private void GeneratePGN () {
            StringBuilder tmpPgn = new StringBuilder();
            switch (_start.Name) {
                case "Knight":
                    tmpPgn.Append("N");
                    break;
                case "Pawn":
                    break;
                default:
                    tmpPgn.Append(_start.Name[0]);
                    break;
            }

            //check for special cases
            //To-Do

            if (_end.Color != "null" && _end.Color != _start.Color) {
                if (_start.Name.Equals("Pawn")) 
                    tmpPgn.Append((char)(_start.CurrentCol + 97));
                tmpPgn.Append("x");
            }

            tmpPgn.Append((char)(_end.CurrentCol + 97));
            tmpPgn.Append((char)(8 - _end.CurrentRow + 48));

            _pgn = tmpPgn;

        }
    }
}
