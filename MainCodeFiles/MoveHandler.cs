using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class MoveHandler {
        public Game _game;

        public MoveHandler(Game game) {
            _game = game;
        }

        public void AttemptMove (int row, int col) {
            if (_block)
                return;
            if (_activePiece != null && _activeMoves.Contains((row, col))) {
                DoMove(_board.PieceAt((row, col)));
                //(Pieces x, (int i, int j)) move = _ai.TakeTurn(_board, this);
                //GameScreenForm.UpdateCheckLabel(String.Format("Name: {2}, Row: {0}, Col: {1}", move.Item2.i, move.Item2.j, move.x.Name));
                return;
            }
            else if (_activePiece != null) {
                _activePiece = null;
            }

            Render();

            Pieces clickedPiece = _board.PieceAt((row, col));
            if (clickedPiece != null && clickedPiece.Color == _playerTurn) {
                List<(int i, int j)> moves = clickedPiece.Moves(_board, _playerTurn, false);

                foreach ((int i, int j) in moves) {
                    if (!GameScreenForm.Flip) {
                        GameScreenForm.BtnArr[i, j].BackColor = nordGreen;
                        if (_board.PieceAt((i, j)).Color != "null")
                            GameScreenForm.BtnArr[i, j].BackColor = nordRed;
                    }
                    else {
                        GameScreenForm.BtnArr[7-i, 7-j].BackColor = nordGreen;
                        if (_board.PieceAt((i, j)).Color != "null")
                            GameScreenForm.BtnArr[7-i, 7-j].BackColor = nordRed;
                    }
                }

                if (moves.Capacity > 0) {
                    _activePiece = clickedPiece;
                    _activeMoves = moves;
                }
            }
        }

        private void DoMove (Pieces destination) {
            int currCol = _activePiece.CurrentCol;
            GameScreenForm.UpdateCheckLabel("");

            _previousMoves.Push(new Moves(_activePiece, destination, _board.EnPassant));

            if (_activePiece.Name == "Rook") {
                King king = (King)_board.PieceAt(_board.KingPosition(_activePiece.Color));
                _previousMoves.Peek().Castling = (king.CastleLeft, king.CastleRight);
            }

            Dictionary<String, bool> flags = _board.MovePiece(_activePiece, destination);

            if (flags["Captured"]) {
                if (destination.Name == "Empty")
                    _captures[_playerTurn]["Pawn"] += 1;
                else
                    _captures[_playerTurn][destination.Name] += 1;
            }

            if (flags["Castle"])
                _previousMoves.Peek().WasCastle(_activePiece.CurrentCol < currCol);

            if (flags["EnPassant"]) 
                _previousMoves.Peek().WasPassant(currCol);

            if (flags["In Check"]) {
                _previousMoves.Peek().WasCheck();
                GameScreenForm.UpdateCheckLabel(String.Format("{0} in check", Game.FlipColor(_playerTurn)));
            }

            if (flags["Checkmate"]) {
                GameScreenForm.UpdateCheckLabel("You Lose");
            }

            _playerTurn = Game.FlipColor(_playerTurn);
            _activePiece = null;
            UpdateMoves(_previousMoves.Peek());
            _previousMoves.Peek().FEN = CreateFen(_board);
            _movesMade += 1;

            Render();
        }


        public void UndoMove () {
            if (_block)
                return;
            if (_previousMoves.Count > 0) {
                Moves last = _previousMoves.Pop();
                Pieces start = last.Start;
                Pieces end = last.End;

                _board.SetPieceAt(start);
                _board.SetPieceAt(end);
                _playerTurn = FlipColor(_playerTurn);
                _board.EnPassant = last.EnPassantSquare;

                if (end.Color != "null") {
                    _captures[_playerTurn][end.Name] -= 1;
                }

                if (last.Flags["EnPassant"]) {
                    Pieces extra = last.Extra;
                    _board.SetPieceAt(extra);
                    _captures[_playerTurn][extra.Name] -= 1;
                }

                if (last.Flags["Castle"]) {
                    Pieces extra = last.Extra;
                    _board.SetPieceAt(extra);
                    _board.SetPieceAt((extra.CurrentRow, extra.CurrentCol == 0 ? 3 : 5), null);
                }

                if (start.Name == "Rook") {
                    King king = (King)_board.PieceAt(_board.KingPosition(start.Color));
                    king.CastleLeft = last.Castling.l;
                    king.CastleRight = last.Castling.r;
                }

                int count = GameScreenForm.MovementTable.Controls.Count - 1;
                GameScreenForm.MovementTable.Controls.RemoveAt(count);
                _movesMade--;
                if (_movesMade % 2 == 0) 
                    GameScreenForm.MovementTable.Controls.RemoveAt(count - 1);

                if (GameScreenForm.MovementTable.Controls.Count > 0)
                    GameScreenForm.MovementTable.Controls[GameScreenForm.MovementTable.Controls.Count - 1].BackColor = nordCurr;

                Render();
            }
        }
    }
}
