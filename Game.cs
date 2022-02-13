using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class Game {

        private Board _board;
        private string _playerTurn;
        private int[] _whiteCaptures;
        private int[] _blackCaptures;
        private StringBuilder _fen;
        private StringBuilder _pgn;
        private int _halfmoves;
        
        public Game () {
            _board = new Board ();
            _playerTurn = "White";
            _whiteCaptures = new int[5] { 0, 0, 0, 0, 0 };
            _blackCaptures = new int[5] { 0, 0, 0, 0, 0 };
            _fen = new StringBuilder();
            _pgn = new StringBuilder();
            _halfmoves = 0;
            _playerTurn = "White";
        }

        public Game (StringBuilder pgn, StringBuilder fen) : this() {
            _fen = fen;
            _pgn = pgn;
        }

        public void Run () {
            _board.Render();
        }
    }
}
