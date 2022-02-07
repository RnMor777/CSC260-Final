using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Knight : Pieces {

        public Knight (string color, int row, int col) {
            this.Name = "Knight";
            this.PieceWorth = (float)3.0;
            this.Color = color;
            this.CurrentRow = row;
            this.CurrentCol = col;
        }

        public override int[,] PossibleMoves () {
            int[,] markArr = new int[8,8];
            
            if (CurrentRow - 2 >= 0 && CurrentCol - 1 >= 0) 
                markArr[CurrentRow - 2, CurrentCol - 1] = 1;
            if (CurrentRow - 1 >= 0 && CurrentCol - 2 >= 0)
                markArr[CurrentRow - 1, CurrentCol - 2] = 1;
            if (CurrentRow - 1 >= 0 && CurrentCol + 2 < 8)
                markArr[CurrentRow - 1, CurrentCol + 2] = 1;
            if (CurrentRow - 2 >= 0 && CurrentCol + 1 < 8)
                markArr[CurrentRow - 2, CurrentCol + 1] = 1;

            if (CurrentRow + 2 < 8 && CurrentCol - 1 >= 0) 
                markArr[CurrentRow + 2, CurrentCol - 1] = 1;
            if (CurrentRow + 1 < 8 && CurrentCol - 2 >= 0)
                markArr[CurrentRow + 1, CurrentCol - 2] = 1;
            if (CurrentRow + 1 < 8 && CurrentCol + 2 < 8)
                markArr[CurrentRow + 1, CurrentCol + 2] = 1;
            if (CurrentRow + 2 < 8 && CurrentCol + 1 < 8)
                markArr[CurrentRow + 2, CurrentCol + 1] = 1;

            return markArr;
        }
    }
}
