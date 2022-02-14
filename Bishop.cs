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

        public override int[,] PossibleMoves (Board board) {
            int[,] markArr = new int[8,8];
            int[] offsetI = { 1, 1, -1, -1 };
            int[] offsetJ = { 1, -1, 1, -1 };
            int i, j, k;

            for (k = 0; k < 4; k++) {
                i = CurrentRow + offsetI[k];
                j = CurrentCol + offsetJ[k];
                while (IsWithinBoard(i, j)) {
                    if (board.PieceAt(i, j).Color == "null") {
                        markArr[i, j] = 1;
                    }
                    else if (board.PieceAt(i, j).Color == Color) {
                        break;
                    }
                    else {
                        markArr[i, j] = 1;
                        break;
                    }
                    i += offsetI[k];
                    j += offsetJ[k];
                }
            }

            return markArr;
        }
    }
}
