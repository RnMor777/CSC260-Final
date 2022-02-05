using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Game {

        private Board _board;
        private string _playerTurn;
        private int[5] _whiteCaptures;
        private int[5] _blackCaptures;
        private StringBuilder _fen;
        private int _halfmoves;
        
        public Game () {
            _board = new Board ();
            _playerTurn = "White";
            _whiteCaptures = [0, 0, 0, 0, 0];
            _blackCaptures = [0, 0, 0, 0, 0];
            _fen = new StringBuilder();
            _halfmoves = 0;
        }

        public Game (string pgn, string fen) {

        }
    }
}
