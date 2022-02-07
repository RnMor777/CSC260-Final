using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Board {

        private Pieces[,] _pieces;

        public Board () {
            _pieces = new Pieces[8,8];
            _pieces[0, 0] = new Rook("Black", 0, 0);
            _pieces[0, 1] = new Knight("Black", 0, 1);
            _pieces[0, 2] = new Bishop("Black", 0, 2);
            _pieces[0, 3] = new Queen("Black", 0, 3);
            _pieces[0, 4] = new King("Black", 0, 4);
            _pieces[0, 5] = new Bishop("Black", 0, 5);
            _pieces[0, 6] = new Knight("Black", 0, 6);
            _pieces[0, 7] = new Rook("Black", 0, 7);
        }

        public Board (string fen) {

        }
    }
}
