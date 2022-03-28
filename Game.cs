using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Game {

        private Board _board;
        private string _playerTurn;
        private Dictionary<String, Dictionary<String, int>> _captures;
        private StringBuilder _fen;
        private StringBuilder _pgn;
        private int _halfmoves;
        private Pieces _activePiece;
        private List<(int i, int j)> _activeMoves;
        private List<List<(int i, int j)>> checks;
        
        public Game () {
            _board = new Board ();
            Dictionary<String, int> whiteCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            Dictionary<String, int> blackCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            _captures = new Dictionary<String, Dictionary<String, int>> { { "White", whiteCaptures}, { "Black", blackCaptures } };
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
            Render();
        }

        public void EscapeMove () {
            _activePiece = null;
            Render();
        }

        public void AttemptMove (int row, int col) {
            if (_activePiece != null && _activeMoves.Contains((row, col))) {
                DoMove(_board.PieceAt(row, col));
                return;
            }
            else if (_activePiece != null) {
                _activePiece = null;
            }

            Render();

            Pieces clickedPiece = _board.PieceAt(row, col);
            if (clickedPiece != null && clickedPiece.Color == _playerTurn) {
                List<(int i, int j)> moves = clickedPiece.Moves(_board, _playerTurn, false);

                foreach ((int i, int j) in moves) {
                    GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.LimeGreen;
                    if (_board.PieceAt(i, j).Color != "null") 
                        GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.Crimson;
                }

                if (moves.Capacity > 0) {
                    _activePiece = clickedPiece;
                    _activeMoves = moves;
                }
            }
        }

        private void DoMove (Pieces destination) {
            Dictionary<String, bool> flags = _board.MovePiece(_activePiece, destination);

            if (flags["Captured"])
                _captures[_playerTurn][destination.Name] += 1;

            _playerTurn = _playerTurn == "White" ? "Black" : "White";
            _activePiece = null;

            if (flags["In Check"]) 
                GameScreenForm.UpdateCheckLabel(String.Format("{0} in check", _playerTurn));
            else 
                GameScreenForm.UpdateCheckLabel("");

            Render();
        }

        public void Render () {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    GameScreenForm.BtnArr[i, j].Image = _board.PieceAt(i, j).Color != "null" ? _board.PieceAt(i, j).Image : null;
                    GameScreenForm.BtnArr[i, j].BackColor = (i + j) % 2 == 0 ? System.Drawing.Color.FromArgb(237, 209, 167) : System.Drawing.Color.FromArgb(191, 135, 73);
                }
            }
        }
    }
}
