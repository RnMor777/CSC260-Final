using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Moves {
        private Pieces[,] _pieces;
        private StringBuilder _pgn;

        public String PGN {
            get { return _pgn.ToString(); }
        }

        public Pieces[,] OldBoard {
            get { return _pieces; }
        }

        public Moves(Pieces[,] pieces, Pieces orig, Pieces dest) {
            _pieces = new Pieces[8, 8];
            Array.Copy(pieces, _pieces, 64);
            StringBuilder tmpPgn = new StringBuilder();

            switch (orig.Name) {
                case "Knight":
                    tmpPgn.Append("N");
                    break;
                case "Pawn":
                    break;
                default:
                    tmpPgn.Append(orig.Name[0]);
                    break;
            }

            //check for special cases

            if (dest.Color != "null" && dest.Color != orig.Color) {
                if (orig.Name.Equals("Pawn")) {
                    tmpPgn.Append((char)(97 + orig.CurrentCol));
                }
                tmpPgn.Append("x");
            }

            tmpPgn.Append((char)(dest.CurrentCol + 97));
            tmpPgn.Append((char)(dest.CurrentRow + 48));

            _pgn = tmpPgn;
        }

        public void WasCheck () {
            _pgn = _pgn.Append("+");
        }

        public void WasMate () {
            _pgn = _pgn.Append("#");
        }

    }
}
