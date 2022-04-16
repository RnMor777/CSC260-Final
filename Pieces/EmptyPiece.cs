using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class EmptyPiece : Pieces {

        public EmptyPiece((int i, int j) pos) {
            this.Name = "Empty";
            this.PieceWorth = 0;
            this.Color = "null";
            this.Position = pos;
        }

        public override Pieces Copy() {
            return new EmptyPiece(this.Position);
        }

        protected override List<(int i, int j)> PossibleMoves(Board board) {
            List<(int i, int j)> retList = new List<(int, int)>();
            return retList;
        }
    }
}
