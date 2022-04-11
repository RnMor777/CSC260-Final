using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class King : Pieces {

        private bool _castleLeft;
        private bool _castleRight;

        public bool CastleLeft {
            get { return _castleLeft; }
            set { _castleLeft = value; }
        }

        public bool CastleRight {
            get { return _castleRight; }
            set { _castleRight = value; }
        }

        public King (string color, int row, int col, bool cl, bool cr) {
            this.Name = "King";
            this.PieceWorth = float.MaxValue;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WKing:Properties.Resources.BKing));
            _castleLeft = cl;
            _castleRight = cr;
        }

        public override Pieces Copy () {
            return new King(this.Color, this.CurrentRow, this.CurrentCol, _castleLeft, _castleRight);
        }

        protected override List<(int i, int j)> PossibleMoves (Board board) {
            List<(int i, int j)> retList = new List<(int, int)>();

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (IsWithinBoard(CurrentRow + i, CurrentCol + j) && board.PieceAt(CurrentRow + i, CurrentCol + j).Color != Color ) {
                        retList.Add((CurrentRow + i, CurrentCol + j));
                    }
                }
            }

            if (_castleLeft && !board.InCheck(Color)) {
                bool fail = false;
                for (int i = 3; i > 0; i--) {
                    if (!board.PieceAt(CurrentRow, i).Color.Equals("null")) {
                        fail = true;
                        break;
                    }
                }
                if (!board.PieceAt(CurrentRow, 0).Equals(new Rook(Color, CurrentRow, 0))) 
                    fail = true;
                if (!fail) 
                    retList.Add((CurrentRow, 2));
            }

            if (_castleRight && !board.InCheck(Color)) {
                bool fail = false;
                for (int i = 5; i < 7; i++) {
                    if (!board.PieceAt(CurrentRow, i).Color.Equals("null")) {
                        fail = true;
                        break;
                    }
                }
                if (!board.PieceAt(CurrentRow, 7).Equals(new Rook(Color, CurrentRow, 7))) 
                    fail = true;
                if (!fail) 
                    retList.Add((CurrentRow, 6));
            }

            return retList;
        }
    }
}
