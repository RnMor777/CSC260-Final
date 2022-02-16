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
        private int[] _whiteCaptures;
        private int[] _blackCaptures;
        private StringBuilder _fen;
        private StringBuilder _pgn;
        private int _halfmoves;
        private Pieces _activePiece;
        private int[,] _activeMoves;
        
        public Game () {
            _board = new Board ();
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

        public void EscapeMove () {
            _activePiece = null;
            _board.Render();
        }

        public void AttemptMove (int row, int col) {
            if (_activePiece != null && _activeMoves[row, col] == 1) {
                DoMove(row, col);
                return;
            }
            else if (_activePiece != null) {
                _activePiece = null;
            }

            _board.Render();

            Pieces clickedPiece = _board.PieceAt(row, col);
            if (clickedPiece != null && clickedPiece.Color == _playerTurn) {
                int[,] moves = clickedPiece.PossibleMoves(_board);
                int counter = 0;
                Board tmp = new Board(_board);

                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        if (moves[i, j] == 1) {
                            //if (!putIntoCheck) {
                                GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.LimeGreen;
                                if (_board.PieceAt(i, j).Color != "null") 
                                    GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.Crimson;
                                counter++;
                            //}
                        }
                    }
                }

                if (counter > 0) {
                    _activePiece = clickedPiece;
                    _activeMoves = moves;
                }
            }
        }

        private void DoMove (int row, int col) {
            _board.SetPieceAt(_activePiece.CurrentRow, _activePiece.CurrentCol, null);
            _activePiece.CurrentRow = row;
            _activePiece.CurrentCol = col;
            _board.SetPieceAt(row, col, _activePiece);
            _playerTurn = _playerTurn == "White" ? "Black" : "White";
            _activePiece = null;
            if (InCheck (_board)) {
                GameScreenForm.UpdateCheckLabel(_playerTurn + " in check");
            }
            else {
                GameScreenForm.UpdateCheckLabel("");
            }
            _board.Render();
        }

        public bool InCheck(Board board) {
            return false;
        }
    }
}
