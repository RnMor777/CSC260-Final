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
        private List<List<(int i, int j)>> checks;
        
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
                moves = SieveCheck(moves, clickedPiece);

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

        private void DoMove (int row, int col) {
            _board.SetPieceAt(_activePiece.CurrentRow, _activePiece.CurrentCol, null);
            _activePiece.CurrentRow = row;
            _activePiece.CurrentCol = col;
            _board.SetPieceAt(row, col, _activePiece);
            _playerTurn = _playerTurn == "White" ? "Black" : "White";
            _activePiece = null;

            /*if (InCheck (_board, "White"))
               GameScreenForm.UpdateCheckLabel("White in check");
            else if (InCheck (_board, "Black"))
               GameScreenForm.UpdateCheckLabel("Black in check");
            else 
                GameScreenForm.UpdateCheckLabel(""); */

            _board.Render();
        }

        public List<(int i, int j)> SieveCheck(List<(int i, int j)> moves, Pieces piece) {
            int[] xmoves = { 1, 1,  1, 0, -1,  0, -1, -1 };
            int[] ymoves = { 1, 0, -1, 1,  1, -1, -1,  0 };
            bool[] noCheck = new bool[8];
            (int i, int j) kingPos = _board.KingPosition(_playerTurn);
            List<(int i, int j)> sievedMoves = new List<(int i, int j)>();
            String oppPlayer = _playerTurn == "White" ? "Black" : "White";
            Pieces tmp;
            bool sieve;
            int x, y;


            _board.SetPieceAt(piece.CurrentRow, piece.CurrentCol, null);
            foreach ((int i, int j) in moves) {
                tmp = _board.PieceAt(i, j);
                if (tmp.Color == "null") tmp = null;
                _board.SetPieceAt(i, j, piece);
                sieve = false;
                for (int k = 0; k < 8; k++) {
                    if (noCheck[k])
                        continue;

                    x = kingPos.i;
                    y = kingPos.j;
                    do {
                        x += xmoves[k];
                        y += ymoves[k];
                    } while (x >= 0 && x < 8 && y >= 0 && y < 8 && _board.PieceAt(x, y).Color == "null");

                    if (x < 0 || x > 7 || y < 0 || y > 7) 
                        continue;

                    Pieces foundPiece = _board.PieceAt(x, y);
                    if (k % 2 == 0) {
                        if (foundPiece.Color == oppPlayer && (foundPiece.Name == "Queen" || foundPiece.Name == "Bishop")) {
                            sieve = true;
                        }
                        else if (!foundPiece.Equals(piece)) {
                            noCheck[k] = true;
                        }
                    }
                    else {
                        if (foundPiece.Color == oppPlayer && (foundPiece.Name == "Queen" || foundPiece.Name == "Rook")) {
                            sieve = true;
                        }
                        else if (!foundPiece.Equals(piece)) {
                            noCheck[k] = true;
                        }

                    }
                }
                _board.SetPieceAt(i, j, tmp);

                if (!sieve)
                    sievedMoves.Add((i, j));

            }
            _board.SetPieceAt(piece.CurrentRow, piece.CurrentCol, piece);
            return sievedMoves;
        }

        /*public bool InCheck(Board board, string color) {
            (int i, int j) kingPos = board.KingPosition(color);

            Rook procRook = new Rook(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procRook.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Queen" || board.PieceAt(i, j).Name == "Rook") {
                    return true;
                }
            }

            Bishop procBishop = new Bishop(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procBishop.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Queen" || board.PieceAt(i, j).Name == "Bishop") {
                    return true;
                }
            }

            Knight procKnight = new Knight(color, kingPos.i, kingPos.j);
            foreach ((int i, int j) in procKnight.PossibleMoves(board)) {
                if (board.PieceAt(i, j).Name == "Knight") { 
                    return true;
                }
            }

            return false;
        } */

    }
}
