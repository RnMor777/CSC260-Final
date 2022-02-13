using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Rook : Pieces {

        public Rook (string color, int row, int col) {
            this.Name = "Rook";
            this.PieceWorth = (float)5.0;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WRook:Properties.Resources.BRook));
        }

        public override int[,] PossibleMoves () {
            int[,] markArr = new int[8,8];

            for (int i = 0; i < 8; i++) {
                markArr[CurrentRow, i] = 1;
                markArr[i, CurrentCol] = 1;
            }
            markArr[CurrentRow, CurrentCol] = 0;

            return markArr;
        }
    }
}
