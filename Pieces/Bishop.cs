using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Bishop : Pieces {

        public Bishop (string color, int row, int col) {
            this.Name = "Bishop";
            this.PieceWorth = (float)3.0;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WBishop:Properties.Resources.BBishop));
        }

        public override Pieces Copy () {
            return new Bishop(this.Color, this.CurrentRow, this.CurrentCol);
        }

        protected override List<(int i, int j)> PossibleMoves (Board board) {
            List<(int i, int j)> retList = new List<(int, int)>();
            int[] offsetI = { 1, 1, -1, -1 };
            int[] offsetJ = { 1, -1, 1, -1 };
            int i, j, k;

            for (k = 0; k < 4; k++) {
                i = CurrentRow + offsetI[k];
                j = CurrentCol + offsetJ[k];
                while (IsWithinBoard(i, j)) {
                    if (board.PieceAt((i, j)).Color == "null") {
                        retList.Add((i, j));
                    }
                    else if (board.PieceAt((i, j)).Color == Color) {
                        break;
                    }
                    else {
                        retList.Add((i, j));
                        break;
                    }
                    i += offsetI[k];
                    j += offsetJ[k];
                }
            }
            return retList;
        }
    }
}
