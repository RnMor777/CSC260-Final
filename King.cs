using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class King : Pieces {

        public King (string color, int row, int col) {
            this.Name = "King";
            this.PieceWorth = float.MaxValue;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WKing:Properties.Resources.BKing));
        }

        public override int[,] PossibleMoves (Board board) {
            int[,] markArr = new int[8,8];

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (IsWithinBoard(CurrentRow+i, CurrentCol+j) && board.PieceAt(CurrentRow + i, CurrentCol + j).Color != Color ) {
                        markArr[CurrentRow + i, CurrentCol + j] = 1;
                    }
                }
            }
            markArr[CurrentRow, CurrentCol] = 0;

            return markArr;
        }
    }
}
