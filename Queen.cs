using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Queen : Pieces {

        public Queen (string color, int row, int col) {
            this.Name = "Queen";
            this.PieceWorth = (float)9.0;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WQueen:Properties.Resources.BQueen));
        }

        public override List<(int i, int j)> PossibleMoves (Board board) {
            List<(int i, int j)> retList = new List<(int, int)>();
            int[] offsetI = { 1, 1, -1, -1, 1, -1, 0, 0};
            int[] offsetJ = { 1, -1, 1, -1, 0, 0, 1, -1};
            int i, j, k;

            for (k = 0; k < 8; k++) {
                i = CurrentRow + offsetI[k];
                j = CurrentCol + offsetJ[k];
                while (IsWithinBoard(i, j)) {
                    if (board.PieceAt(i, j).Color == "null") {
                        retList.Add((i, j));
                    }
                    else if (board.PieceAt(i, j).Color == Color) {
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
