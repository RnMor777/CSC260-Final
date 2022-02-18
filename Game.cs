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
        private List<(int i, int j)> _activeMoves;
        
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
            if (_activePiece != null && _activeMoves.Contains((row, col))) {
                DoMove(row, col);
                return;
            }
            else if (_activePiece != null) {
                _activePiece = null;
            }

            _board.Render();

            Pieces clickedPiece = _board.PieceAt(row, col);
            if (clickedPiece != null && clickedPiece.Color == _playerTurn) {
                List<(int i, int j)> moves = clickedPiece.PossibleMoves(_board);

                foreach ((int i, int j) in moves) {
                    clickedPiece.CurrentRow = i;
                    clickedPiece.CurrentCol = j;
                    _board.SetPieceAt(i, j, clickedPiece);
                    _board.SetPieceAt(row, col, null);
                    bool retCheck = InCheck(_board, _playerTurn);
                    _board.SetPieceAt(i, j, null);
                    _board.SetPieceAt(row, col, clickedPiece);

                    if (retCheck)
                        continue;

                    GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.LimeGreen;
                    if (_board.PieceAt(i, j).Color != "null") 
                        GameScreenForm.BtnArr[i, j].BackColor = System.Drawing.Color.Crimson;
                }
                clickedPiece.CurrentRow = row;
                clickedPiece.CurrentCol = col;

                if (moves.Capacity > 0) {
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

            if (InCheck (_board, "White"))
               GameScreenForm.UpdateCheckLabel("White in check");
            else if (InCheck (_board, "Black"))
               GameScreenForm.UpdateCheckLabel("Black in check");
            else 
                GameScreenForm.UpdateCheckLabel("");

            _board.Render();
        }

        public bool InCheck(Board board, string color) {
            (int i, int j) kingPos = board.KingPosition(color);

            Rook procRook = new Rook(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procRook.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Queen" || board.PieceAt(i, j).Name == "Rook") {
                    return true;
                }
            }

            Bishop procBishop = new Bishop(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procBishop.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Queen" || board.PieceAt(i, j).Name == "Rook") {
                    return true;
                }
            }

            Bishop procKnight = new Bishop(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procKnight.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Knight") { 
                    return true;
                }
            }

            return false;
        }

        public void ThreadRook () {

        }
    }
}
