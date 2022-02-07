using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Pawn : Pieces {

        public Pawn (string color, int row, int col) {
            this.Name = "Pawn";
            this.PieceWorth = (float)1.0;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
        }

        public override int[,] PossibleMoves () {
            int[,] markArr = new int[8,8];

            if (Color == "White") {
                if (CurrentRow - 1 >= 0)
                    markArr[CurrentRow - 1, CurrentCol] = 1;
                if (CurrentRow == 6)
                    markArr[CurrentRow - 2, CurrentCol] = 1;
                if (CurrentRow - 1 >= 0 && CurrentRow - 1 >= 0)
                    markArr[CurrentRow - 1, CurrentCol - 1] = 1;
                if (CurrentRow - 1 >= 0 && CurrentRow + 1 < 8)
                    markArr[CurrentRow - 1, CurrentCol + 1] = 1;
            }
            else {
                if (CurrentRow + 1 < 8)
                    markArr[CurrentRow + 1, CurrentCol] = 1;
                if (CurrentRow == 1)
                    markArr[CurrentRow + 2, CurrentCol] = 1;
                if (CurrentRow + 1 < 8 && CurrentRow - 1 >= 0)
                    markArr[CurrentRow + 1, CurrentCol - 1] = 1;
                if (CurrentRow + 1 < 8 && CurrentRow + 1 < 8)
                    markArr[CurrentRow + 1, CurrentCol + 1] = 1;
            }

            return markArr;
        }
    }
}
