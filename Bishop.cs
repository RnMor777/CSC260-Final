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

        public override int[,] PossibleMoves () {
            int[,] markArr = new int[8,8];

            for (int i = 1; i < 8; i++) {
                if (CurrentRow + i < 8 && CurrentCol + i < 8) {
                    markArr[CurrentRow + i, CurrentCol + i] = 1;
                }
                if (CurrentRow - i >= 0 && CurrentCol + i < 8) {
                    markArr[CurrentRow - i, CurrentCol + i] = 1;
                }
                if (CurrentRow + i < 8 && CurrentCol - i >= 0) {
                    markArr[CurrentRow + i, CurrentCol - i] = 1;
                }
                if (CurrentRow + i >= 0 && CurrentCol - i >= 0) {
                    markArr[CurrentRow - i, CurrentCol - i] = 1;
                }
            }
            markArr[CurrentRow, CurrentCol] = 0;

            return markArr;
        }
    }
}
