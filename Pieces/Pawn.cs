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

        public override Pieces Copy() {
            return new Pawn(this.Color, this.CurrentRow, this.CurrentCol);
        }

        protected override List<(int i, int j)> PossibleMoves (Board board) {
            List<(int i, int j)> retList = new List<(int, int)>();
            int baseRow = Color == "White" ? 6 : 1;
            int direction = Color == "White" ? -1 : 1;
            string oppColor = Color == "White" ? "Black" : "White";

            if (CurrentRow == baseRow) {
                if (board.PieceAt(baseRow + direction, CurrentCol).Color == "null") {
                    retList.Add((baseRow + direction, CurrentCol));

                    if (board.PieceAt(baseRow + 2 * direction, CurrentCol).Color == "null")
                        retList.Add((baseRow + 2 * direction, CurrentCol));
                }
            }
            else if (IsWithinBoard(CurrentRow + direction, CurrentCol) && board.PieceAt(CurrentRow + direction, CurrentCol).Color == "null") 
                retList.Add((CurrentRow + direction, CurrentCol));

            if (IsWithinBoard(CurrentRow + direction, CurrentCol - 1) && board.PieceAt(CurrentRow + direction, CurrentCol - 1).Color == oppColor)
                retList.Add((CurrentRow + direction, CurrentCol - 1));
            if (IsWithinBoard(CurrentRow + direction, CurrentCol + 1) && board.PieceAt(CurrentRow + direction, CurrentCol + 1).Color == oppColor)
                retList.Add((CurrentRow + direction, CurrentCol + 1));

            if (IsWithinBoard(CurrentRow + direction, CurrentCol - 1) && board.EnPassant == (CurrentRow + direction, CurrentCol - 1)) 
                retList.Add((CurrentRow + direction, CurrentCol - 1));
            if (IsWithinBoard(CurrentRow + direction, CurrentCol + 1) && board.EnPassant == (CurrentRow + direction, CurrentCol + 1)) 
                retList.Add((CurrentRow + direction, CurrentCol + 1));

            return retList;
        }
    }
}
