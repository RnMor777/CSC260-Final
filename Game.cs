using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Game {

        public static System.Drawing.Color nordRed = System.Drawing.Color.FromArgb(191, 97, 106);
        public static System.Drawing.Color nordGreen = System.Drawing.Color.FromArgb(163,190,140);
        public static System.Drawing.Color nordWhite = System.Drawing.Color.FromArgb(236,239,244);
        public static System.Drawing.Color nordBlack = System.Drawing.Color.FromArgb(94,129,172);
        public static System.Drawing.Color nordPrev = System.Drawing.Color.FromArgb(143,188,187);
        public static System.Drawing.Color nordCurr = System.Drawing.Color.FromArgb(180,142,173);

        private Board _board;
        private string _playerTurn;
        private Dictionary<String, Dictionary<String, int>> _captures;
        private Dictionary<String, String> _symbols;
        private int _halfmoves;
        private Pieces _activePiece;
        private List<(int i, int j)> _activeMoves;
        //private List<List<(int i, int j)>> checks;
        private Stack<Moves> _previousMoves;
        private int _movesMade;
        private bool _block;
        private int _locationPrevView;

        public Game () {
            _board = new Board ();
            Dictionary<String, int> whiteCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            Dictionary<String, int> blackCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            _captures = new Dictionary<String, Dictionary<String, int>> { { "White", whiteCaptures}, { "Black", blackCaptures } };
            _symbols = new Dictionary<String, String> { { "Pawn", "♟" }, { "Knight", "♞" }, { "Queen", "♛" }, { "Bishop", "♝" }, { "Rook", "♜" } };
            _halfmoves = 0;
            _previousMoves = new Stack<Moves> ();
            _playerTurn = "White";
            _movesMade = 0;
            _block = false;
            _locationPrevView = 0;
        }

        public void Run () {
            Render();
        }

        public void EscapeMove () {
            _activePiece = null;
            Render();
        }

        public void AttemptMove (int row, int col) {
            if (_block)
                return;
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
                    if (!GameScreenForm.Flip) {
                        GameScreenForm.BtnArr[i, j].BackColor = nordGreen;
                        if (_board.PieceAt(i, j).Color != "null")
                            GameScreenForm.BtnArr[i, j].BackColor = nordRed;
                    }
                    else {
                        GameScreenForm.BtnArr[7-i, 7-j].BackColor = nordGreen;
                        if (_board.PieceAt(i, j).Color != "null")
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

            _previousMoves.Push(new Moves(_board.AllPieces, _activePiece, destination, _board.EnPassant));

            if (_activePiece.Name == "Rook") {
                King king = (King)_board.PieceAt(_board.KingPosition(_activePiece.Color));
                _previousMoves.Peek().Castling = (king.CastleLeft, king.CastleRight);
            }

            Dictionary<String, bool> flags = _board.MovePiece(_activePiece, destination);

            if (flags["Captured"])
                _captures[_playerTurn][destination.Name] += 1;

            if (flags["Castle"])
                _previousMoves.Peek().WasCastle(_activePiece.CurrentCol < currCol);

            if (flags["EnPassant"]) 
                _previousMoves.Peek().WasPassant(currCol);


            _playerTurn = _playerTurn == "White" ? "Black" : "White";
            _activePiece = null;
            UpdateMoves(_previousMoves.Peek());
            _previousMoves.Peek().FEN = CreateFen(_board);
            _movesMade += 1;

            GameScreenForm.UpdateCheckLabel("");
            if (flags["In Check"]) {
                _previousMoves.Peek().WasCheck();
                GameScreenForm.UpdateCheckLabel(String.Format("{0} in check", _playerTurn));
            }
            Render();
        }

        public void Render () {
            Render(_board);
        }

        private void Render (Board board) {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (!GameScreenForm.Flip) {
                        GameScreenForm.BtnArr[i, j].Image = board.PieceAt(i, j).Color != "null" ? board.PieceAt(i, j).Image : null;
                        //GameScreenForm.BtnArr[i, j].BackColor = (i + j) % 2 == 0 ? System.Drawing.Color.FromArgb(240, 217, 181) : System.Drawing.Color.FromArgb(181, 136, 99);
                        GameScreenForm.BtnArr[i, j].BackColor = (i + j) % 2 == 0 ? nordWhite : nordBlack;
                    }
                    else {
                        GameScreenForm.BtnArr[i, j].Image = board.PieceAt(7-i, 7-j).Color != "null" ? board.PieceAt(7-i, 7-j).Image : null;
                        GameScreenForm.BtnArr[i, j].BackColor = (i + j) % 2 == 0 ? nordWhite : nordBlack;
                        //GameScreenForm.BtnArr[i, j].BackColor = (i + j) % 2 == 0 ? System.Drawing.Color.FromArgb(240, 217, 181) : System.Drawing.Color.FromArgb(181, 136, 99);
                    }
                }
            }
            Dictionary<String, int> x = _captures["White"];
            StringBuilder labText = new StringBuilder();
            foreach (String y in x.Keys) {
                for (int i = 0; i < x[y]; i++) {
                    labText.Append(_symbols[y]);
                }
            }
            GameScreenForm.WhiteCaps.Text = labText.ToString();

            x = _captures["Black"];
            labText = new StringBuilder();
            foreach (String y in x.Keys) {
                for (int i = 0; i < x[y]; i++) {
                    labText.Append(_symbols[y]);
                }
            }
            GameScreenForm.BlackCaps.Text = labText.ToString();

            //GameScreenForm.UpdateCheckLabel(CreateFen(board));
        }

        public void UpdateMoves (Moves move) {
            if (GameScreenForm.MovementTable.Controls.Count > 0) 
                GameScreenForm.MovementTable.Controls[GameScreenForm.MovementTable.Controls.Count - 1].BackColor = System.Drawing.Color.Transparent;

            if (_movesMade % 2 == 0) {
                Label turn = new Label();
                turn.Height = 25;
                turn.Text = (_movesMade/2+1).ToString();
                GameScreenForm.MovementTable.Controls.Add(turn, 0, _movesMade/2);
                GameScreenForm.MovementTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }
            
            Label addMove = new Label();
            addMove.BackColor = nordCurr;
            addMove.Height = 25;
            addMove.Text = move.PGN;
            addMove.Name = "" + _movesMade;
            AddEventMove(_movesMade, addMove);
            GameScreenForm.MovementTable.Controls.Add(addMove, _movesMade % 2 + 1, _movesMade / 2);
            GameScreenForm.MovementTable.HorizontalScroll.Visible = false;
        }

        public void AddEventMove (int pos, Label label) {
            label.Click += (s, e) => {
                foreach (Label x in GameScreenForm.MovementTable.Controls) {
                    x.BackColor = System.Drawing.Color.Transparent;
                }
                if (pos == _movesMade-1) {
                    _block = false;
                    label.BackColor = nordCurr;
                }
                else {
                    _block = true;
                    label.BackColor = nordPrev;
                }
                _locationPrevView = _movesMade - pos - 1;
                GameScreenForm.UpdateCheckLabel(_previousMoves.ElementAt(_previousMoves.Count - pos - 1).FEN);
                Render(new Board(_previousMoves.ElementAt(_previousMoves.Count - pos - 1).FEN));
            };
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
                _playerTurn = _playerTurn == "White" ? "Black" : "White";
                _board.EnPassant = last.EnPassantSquare;

                if (end.Color != "Null") {
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
                    _board.SetPieceAt(extra.CurrentRow, extra.CurrentCol == 0 ? 3 : 5, null);
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

        public void GoBack(bool all) {
            _block = true;
            foreach (Label x in GameScreenForm.MovementTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }

            if (all) {
                Render(new Board());
                _locationPrevView = _movesMade;
            }
            else if (_locationPrevView < _movesMade-1) {
                _locationPrevView++;
                Render(new Board(_previousMoves.ElementAt(_locationPrevView).FEN));
                GameScreenForm.MovementTable.Controls.Find("" + (_movesMade - _locationPrevView - 1), false)[0].BackColor = nordPrev;
            }
            else {
                _locationPrevView = _movesMade;
                Render(new Board());
            }
        }

        public void GoForward (bool all) {
            _block = true;
            foreach (Label x in GameScreenForm.MovementTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }
            if (all) {
                Render();
                _block = false;
                _locationPrevView = 0;
                GameScreenForm.MovementTable.Controls.Find("" + (_movesMade - _locationPrevView - 1), false)[0].BackColor = nordCurr;
            }
            else if (_locationPrevView > 0) {
                _locationPrevView--;
                Render(new Board(_previousMoves.ElementAt(_locationPrevView).FEN));
                GameScreenForm.MovementTable.Controls.Find("" + (_movesMade - _locationPrevView - 1), false)[0].BackColor = nordPrev;
            }
            if (_locationPrevView == 0 && _movesMade != 0) {
                _block = false;
                GameScreenForm.MovementTable.Controls.Find("" + (_movesMade - _locationPrevView - 1), false)[0].BackColor = nordCurr;
            }
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
    }
}
