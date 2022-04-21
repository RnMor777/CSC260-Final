using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class MoveHandler {
        public DataStore _store;
        private Stack<Moves> _previousMoves;

        public MoveHandler(DataStore game) {
            _store = game;
            _previousMoves = new Stack<Moves> ();
        }

        public Moves MoveAt (int pos) {
            return _previousMoves.ElementAt(pos);
        }

        public void ProcessMove (Pieces start, Pieces destination) {
            (int i, int j) startPos = start.Position;

            _store.Renderer.UpdateCheckLabel("");

            Moves newMove = new Moves(start, destination);
            newMove.AddEnpSquare(_store.Board.EnPassant);
            if (start.Name == "Rook") {
                King king = (King)_store.Board.PieceAt(_store.Board.KingPosition(start.Color));
                newMove.Castling = (king.CastleLeft, king.CastleRight);
            }

            string opColor = Game.FlipColor(start.Color);
            _store.Board.SetPieceAt(destination.Position, start);
            _store.Board.SetPieceAt(startPos, null);
            _store.Board.CalcCheck(opColor);

            // EnPassant Capture
            if (destination.Position == _store.Board.EnPassant) { 
                _store.Board.SetPieceAt((startPos.i + (destination.CurrentRow - startPos.i) / 2, destination.CurrentCol), null);
                newMove.WasPassant(startPos.j);
                _store.PlayerList.Where(p => p.Color == _store.PlayerTurn).First().AddCapture("Pawn");
            }
            // Normal Capture
            if (destination.Name != "Empty") {
                _store.PlayerList.Where(p => p.Color == _store.PlayerTurn).First().AddCapture(destination.Name);
            }

            _store.Board.EnPassant = (-1, -1);

            // Pawn Movement
            if (start.Name.Equals("Pawn")) {
                if (Math.Abs(destination.CurrentRow - startPos.i) == 2)
                    _store.Board.EnPassant = (startPos.i + (destination.CurrentRow - startPos.i) / 2, start.CurrentCol);
            }

            // King Movement
            if (start.Name == "King") {
                ((King)start).CastleLeft = false;
                ((King)start).CastleRight = false;

                if (startPos.j - start.CurrentCol == 2) {
                    _store.Board.SetPieceAt((start.CurrentRow, start.CurrentCol + 1), _store.Board.PieceAt((start.CurrentRow, 0)));
                    _store.Board.SetPieceAt((start.CurrentRow, 0), null);
                    newMove.WasCastle(true);
                }
                if (startPos.j - start.CurrentCol == -2) {
                    _store.Board.SetPieceAt((start.CurrentRow, start.CurrentCol - 1), _store.Board.PieceAt((start.CurrentRow, 7)));
                    _store.Board.SetPieceAt((start.CurrentRow, 7), null);
                    newMove.WasCastle(false);
                }
            }

            // Rook Movement
            if (start.Name == "Rook") {
                if (startPos.j == 0) {
                    ((King)_store.Board.PieceAt(_store.Board.KingPosition(start.Color))).CastleLeft = false;
                }
                else if (startPos.j == 7) {
                    ((King)_store.Board.PieceAt(_store.Board.KingPosition(start.Color))).CastleRight = false;
                }
            }

            // Check + Checkmate
            if (_store.Board.CalcCheck(opColor)) {
                newMove.WasCheck();
                _store.Renderer.UpdateCheckLabel(String.Format("{0} in check", Game.FlipColor(start.Color)));
                if (_store.Board.CheckMate(opColor)) {
                    newMove.WasMate();
                    _store.Renderer.UpdateCheckLabel("You Lose");
                }
            }

            newMove.FEN = Game.CreateFen(_store.Board);
            _previousMoves.Push(newMove);
            _store.TableHandler.UpdateTable(newMove, _store.MovesMade);
        }

        public void UndoMove () {
            Moves last = _previousMoves.Pop();
            Pieces start = last.Start;
            Pieces end = last.End;

            _store.Board.SetPieceAt(start);
            _store.Board.SetPieceAt(end);
            _store.PlayerTurn = Game.FlipColor(_store.PlayerTurn);
            _store.Board.EnPassant = last.EnPassantSquare;

            if (end.Color != "null") {
                _store.CurrentPlayer.RemoveCapture(end.Name);
            }

            if (last.Flags["EnPassant"]) {
                Pieces extra = last.Extra;
                _store.Board.SetPieceAt(extra);
                _store.CurrentPlayer.RemoveCapture(extra.Name);
            }

            if (last.Flags["Castle"]) {
                Pieces extra = last.Extra;
                _store.Board.SetPieceAt(extra);
                _store.Board.SetPieceAt((extra.CurrentRow, extra.CurrentCol == 0 ? 3 : 5), null);
            }

            if (start.Name == "Rook") {
                King king = (King)_store.Board.PieceAt(_store.Board.KingPosition(start.Color));
                king.CastleLeft = last.Castling.l;
                king.CastleRight = last.Castling.r;
            }
        }
    }
}
