using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Game {

        private DataStore _store;

        public Game(DataStore store) {
            _store = store;
        }

        public void Run() { 
            _store.Renderer.Render();
            //Moves moveMade;
            //while (true) {
             //   moveMade = CurrentPlayer.TakeTurn();

              //  PlayerTurn = FlipColor(PlayerTurn);
               // MovesMade += 1;
                //Renderer.Render();
            //}
        }

        public void TakeInput(int i, int j) {
            if (_store.CurrentPlayer.GetType() != typeof(AiPlayer) && !_store.Block) 
                if (((HumanPlayer)_store.CurrentPlayer).AttemptMove(i, j)) {
                    _store.PlayerTurn = FlipColor(_store.PlayerTurn);
                    _store.MovesMade++;
                    _store.Renderer.Render();
                }
        }

        public void TakeUndo() {
            if (!_store.Block && _store.MovesMade > 0) {
                _store.MoveHandler.UndoMove();
                _store.MovesMade--;
                _store.TableHandler.RemoveTop();
            }
            _store.Renderer.Render();
        }

        public void EscapeMove () {
            if (_store.CurrentPlayer.GetType() != typeof(AiPlayer) && !_store.Block) 
                ((HumanPlayer)_store.CurrentPlayer).ActivePiece = null;
            _store.Renderer.Render();
        }

        public void FlipBoard () {
            _store.Flip = !_store.Flip;
            _store.Renderer.Render();
        }

        public static string CreateFen(Board board) {
            StringBuilder fen = new StringBuilder();
            int openSpots = 0;

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (board.PieceAt((i,j)).Color == "null") {
                        openSpots++;
                        continue;
                    }
                    if (openSpots > 0) {
                        fen.Append((char)(48 + openSpots));
                        openSpots = 0;
                    }
                    int toCapital = board.PieceAt((i,j)).Color == "White" ? 0 : 32;
                    switch (board.PieceAt((i, j)).Name) {
                        case "Rook":
                            fen.Append((char)(82 + toCapital));
                            break;
                        case "Knight":
                            fen.Append((char)(78 + toCapital));
                            break;
                        case "Bishop":
                            fen.Append((char)(66 + toCapital));
                            break;
                        case "Queen":
                            fen.Append((char)(81 + toCapital));
                            break;
                        case "King":
                            fen.Append((char)(75 + toCapital));
                            break;
                        case "Pawn":
                            fen.Append((char)(80 + toCapital));
                            break;
                    }
                }
                if (openSpots > 0) {
                    fen.Append((char)(48 + openSpots));
                    openSpots = 0;
                }
                if (i != 7)
                    fen.Append("/");
            }

            fen.Append(" ");
            return fen.ToString();
        }

        public static string FlipColor (string color) {
            return color == "Black" ? "White" : "Black";
        }
    }
}
