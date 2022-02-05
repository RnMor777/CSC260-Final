using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Board {

        private Pieces[] _pieces;

        public Board () {
            _pieces = new Pieces[64]; 
            _pieces[0] = new Pieces();

        }

        public Board (string fen) {

        }
    }
}
