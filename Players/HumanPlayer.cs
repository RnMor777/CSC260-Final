using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSC260_Final {
    internal class HumanPlayer : IPlayer {
        private Pieces _clickedPiece;
        //private static volatile Pieces _clickedPiece;
        //private static volatile int _xdest;
        //private static volatile int _ydest;
        private List<(int i, int j)> _activeMoves;

        private string _color;
        private string _name;
        private int _points;
        private int _id;
        private DataStore _game;
        Dictionary<string, int> _captures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0 }, { "Queen", 0 } };

        public string Name {
            get { return _name; }
        }

        public string Color {
            get { return _color; }
        }

        public int Id {
            get { return _id; }
        }

        public Dictionary<string, int> Captures {
            get { return _captures; }
        }

        public Pieces ActivePiece {
            set { _clickedPiece = value; }
        }

        public HumanPlayer(DataStore game, string color, int id) {
            _color = color;
            _game = game;
            _id = id;
            _activeMoves = new List<(int i, int j)>();
            _clickedPiece = null;
        }

        public Moves TakeTurn () {
            //GameScreenForm.WorkSignal.Wait();
            //_game.MoveHandler.ProcessMove(_clickedPiece, _game.Board.PieceAt((_xdest, _ydest)));
            //GameScreenForm.WorkSignal = new SemaphoreSlim(0, 2);
            //_clickedPiece = null;
            //_xdest = 0;
            //_ydest = 0;
            return null;
        }


        public void AddCapture (string name) {
            _captures[name]++;
        }

        public void RemoveCapture (string name) {
            _captures[name]--;
        }

        public bool AttemptMove (int row, int col) {
            if (_clickedPiece != null && _activeMoves.Contains((row, col))) {
                _game.MoveHandler.ProcessMove(_clickedPiece, _game.Board.PieceAt((row, col)));
                _clickedPiece = null;
                //_xdest = row;
                //_ydest = col;
                //GameScreenForm.WorkSignal.Release();
                return true;
            }
            else if (_clickedPiece != null) {
                _clickedPiece = null;
            }

            _game.Renderer.Render();

            Pieces clickedPiece = _game.Board.PieceAt((row, col));

            if (clickedPiece != null && clickedPiece.Color == _color) {
                List<(int i, int j)> moves = clickedPiece.Moves(_game.Board, _color, false);
                _game.Renderer.Render(moves);

                if (moves.Capacity > 0) {
                    _clickedPiece = clickedPiece;
                    _activeMoves = moves;
                }
            }
            return false;
        }
    }
}
