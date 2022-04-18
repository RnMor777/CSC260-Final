using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class AiPlayer : IPlayer {

        private Board _board;
        private string _color;
        private string _name;
        private int _points;
        private int _positions;
        private int _id;
        private Weights _weight;
        private bool _isWhite;
        private Stack<Moves> _previousMoves;
        private Game _game;
        Dictionary<string, int> _captures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };

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

        public AiPlayer() {
            _previousMoves = new Stack<Moves>();
            _weight = new Weights();
            _color = "Black";
        }


        public (Pieces x, (int i, int j)) TakeTurn (Board board, Game game) {
            _board = new Board(board);
            _game = game;
            return MiniMaxRoot(2, true);
        }

        private (Pieces x, (int i, int j)) MiniMaxRoot (int depth, bool isMaxing) {
            _positions = 0;
            float bestMove = -9999;
            float retMove;
            (Pieces x, (int i, int j)) bestMoveFound = (null,(-1, -1));

            foreach (Pieces x in _board.AllPieces.Where(p => p.Color == _color).ToList()) {
                foreach ((int i, int j) in x.Moves(_board, _color, false)) {
                    MakeMove(x, (i, j));
                    retMove = MiniMax(depth - 1, -10000, 10000, !isMaxing);
                    UndoMove();
                    if (retMove >= bestMove) {
                        bestMove = retMove;
                        bestMoveFound = (x,(i, j));
                    }
                }
            }
            return bestMoveFound;
        }

       private float MiniMax (int depth, int alpha, int beta, bool isMaxing) {
            _positions++;

            if (depth == 0) {
                return -EvaluateBoard();
            }

            float bestMove = isMaxing ? -9999 : 9999;
            float retMove;
            string color = isMaxing ? "Black" : "White";
            bool _isWhite = isMaxing ? true : false;
            (int i, int j) init;

            foreach (Pieces x in _board.AllPieces.Where(p => p.Color == color).ToList()) {
                init = x.Position;
                foreach ((int i, int j) in x.Moves(_board, color, false)) {
                    MakeMove(x, (i, j));
                    //_game.Render();
                    //_form.Refresh();
                    //System.Threading.Thread.Sleep(500);
                    retMove = MiniMax(depth - 1, alpha, beta, !isMaxing);
                    UndoMove();
                    //_game.Render();
                    //_form.Refresh();
                    if (isMaxing) {
                        bestMove = Math.Max(retMove, bestMove);
                        alpha = Math.Max(alpha, (int)bestMove);
                    }
                    else {
                        bestMove = Math.Min(retMove, bestMove);
                        alpha = Math.Min(beta, (int)bestMove);
                    }

                    if (beta < alpha)
                        return bestMove;

                    x.Position = init;
                }
            }
            return bestMove;
        }

        private float EvaluateBoard() {
            float tot = 0;
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    tot += GetPieceValue(_board.PieceAt((i, j)));
                }
            }
            return tot;
        }

        private float GetPieceValue (Pieces piece) {
            float val;
            (int i, int j) pos = piece.Position;
            switch (piece.Name) {
                case "Pawn":
                    val = 10 + ((_isWhite ? _weight.pawnEvalWhite[pos.i, pos.j] : _weight.pawnEvalWhite[7-pos.i, 7-pos.j])/10);
                    break;
                case "Rook":
                    val = 50 + ((_isWhite ? _weight.rookEvalWhite[pos.i, pos.j] : _weight.rookEvalWhite[7-pos.i, 7-pos.j])/10);
                    break;
                case "Knight":
                    val = 30 + (_weight.knightEvalWhite[pos.i, pos.j]/10);
                    break;
                case "Bishop":
                    val = 30 + ((_isWhite ? _weight.bishopEvalWhite[pos.i, pos.j] : _weight.bishopEvalWhite[7-pos.i, 7-pos.j])/10);
                    break;
                case "Queen":
                    val = 90 + (_weight.queenEvalWhite[pos.i, pos.j]/10);
                    break;
                case "King":
                    val = 900 + ((_isWhite ? _weight.kingEvalWhite[pos.i, pos.j] : _weight.kingEvalWhite[7-pos.i, 7-pos.j])/10);
                    break;
                default:
                    val = 0;
                    break;
            }
            return _isWhite ? val : -val;
        }

        private void MakeMove (Pieces piece, (int i, int j) dest) {
            _previousMoves.Push(new Moves(piece, _board.PieceAt(dest), _board.EnPassant));
            _board.MovePiece(piece, _board.PieceAt(dest));
        }

        private void UndoMove () {
            Moves last = _previousMoves.Pop();
            Pieces start = last.Start;
            Pieces end = last.End;
            _board.SetPieceAt(start);
            _board.SetPieceAt(end);
        }
    }
}
