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
            this.Image = ((System.Drawing.Image)(color=="White"?Properties.Resources.WPawn:Properties.Resources.BPawn));
        }

        public override int[,] PossibleMoves (Board board) {
            int[,] markArr = new int[8,8];

            if (Color == "White") {
                if (CurrentRow == 6) {
                    if (board.PieceAt(5, CurrentCol).Color == "null") {
                        markArr[5, CurrentCol] = 1;
                        if (board.PieceAt(4, CurrentCol).Color == "null") {
                            markArr[4, CurrentCol] = 1;
                        }
                    }
                }
                else if (IsWithinBoard(CurrentRow - 1, CurrentCol) && board.PieceAt(CurrentRow - 1, CurrentCol).Color == "null") {
                    markArr[CurrentRow - 1, CurrentCol] = 1;
                }

                if (IsWithinBoard(CurrentRow - 1, CurrentCol - 1) && board.PieceAt(CurrentRow - 1, CurrentCol - 1).Color == "Black") {
                    markArr[CurrentRow - 1, CurrentCol - 1] = 1;
                }
                if (IsWithinBoard(CurrentRow - 1, CurrentCol + 1) && board.PieceAt(CurrentRow - 1, CurrentCol + 1).Color == "Black") {
                    markArr[CurrentRow - 1, CurrentCol + 1] = 1;
                }
            }
            else {
                if (CurrentRow == 1) {
                    if (board.PieceAt(2, CurrentCol).Color == "null") {
                        markArr[2, CurrentCol] = 1;
                        if (board.PieceAt(3, CurrentCol).Color == "null") {
                            markArr[3, CurrentCol] = 1;
                        }
                    }
                }
                else if (IsWithinBoard(CurrentRow + 1, CurrentCol) && board.PieceAt(CurrentRow + 1, CurrentCol).Color == "null") {
                    markArr[CurrentRow + 1, CurrentCol] = 1;
                }

                if (IsWithinBoard(CurrentRow + 1, CurrentCol - 1) && board.PieceAt(CurrentRow + 1, CurrentCol - 1).Color == "White") {
                    markArr[CurrentRow + 1, CurrentCol - 1] = 1;
                }
                if (IsWithinBoard(CurrentRow + 1, CurrentCol + 1) && board.PieceAt(CurrentRow + 1, CurrentCol + 1).Color == "White") {
                    markArr[CurrentRow + 1, CurrentCol + 1] = 1;
                }
            }

            return markArr;
        }
    }
}
