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

        public override int[,] PossibleMoves () {
            int[,] markArr = new int[8,8];

            for (int i = 0; i < 8; i++) {
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
                markArr[CurrentRow, i] = 1;
                markArr[i, CurrentCol] = 1;
            }
            markArr[CurrentRow, CurrentCol] = 0;

            return markArr;
        }
    }
}
