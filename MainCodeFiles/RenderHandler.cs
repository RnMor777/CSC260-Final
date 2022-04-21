using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class RenderHandler {
        private Button[,] _btnArr;
        private Label _labelCheck;
        private Label _whiteCaps;
        private Label _blackCaps;
        private Label[] _letterArr;
        private Label[] _numbArr;
        private DataStore _store;
        private readonly Dictionary<string, char> _symbols = new Dictionary<string, char> { { "Pawn", '♟' }, { "Knight", '♞' }, { "Queen", '♛' }, { "Bishop", '♝' }, { "Rook", '♜' } };

        public Label WhiteCaps {
            set { _whiteCaps = value; }
        }

        public Label BlackCaps {
            set { _blackCaps = value; }
        }

        public Label CheckLabel {
            set { _labelCheck = value; }
        }

        public Button[,] ButtonArr {
            set { _btnArr = value; }
        }

        public Label[] LetterArr {
            set { _letterArr = value; }
        }

        public Label[] NumbArr {
            set { _numbArr = value; }
        }

        public RenderHandler(DataStore game) {
            _store = game;
        }

        public void Render () {
            Render(_store.Board);
        }

        public void Render (Board board) {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (!_store.Flip) {
                        _btnArr[i, j].Image = board.PieceAt((i, j)).Color != "null" ? board.PieceAt((i, j)).Image : null;
                    }
                    else {
                        _btnArr[i, j].Image = board.PieceAt((7-i, 7-j)).Color != "null" ? board.PieceAt((7-i, 7-j)).Image : null;
                    }
                    _btnArr[i, j].BackColor = (i + j) % 2 == 0 ? _store.ColorSettings.White : _store.ColorSettings.Black;
                }
                _letterArr[i].Text = "" + (char)(65 + (_store.Flip ? (7 - i) : i));
                _numbArr[i].Text = "" + (char)(49 + (_store.Flip ? (7 - i) : i));
            }

            StringBuilder labText = new StringBuilder();
            foreach (var x in _store.Player1.Captures) 
                labText.Append(_symbols[x.Key], x.Value);

            //_whiteCaps.Invoke((MethodInvoker)delegate {
                _whiteCaps.Text = labText.ToString();
            //});

            labText = new StringBuilder();
            foreach (var x in _store.Player2.Captures) 
                labText.Append(_symbols[x.Key], x.Value);

            //_blackCaps.Invoke((MethodInvoker)delegate {
                _blackCaps.Text = labText.ToString();
            //});
        }

        public void Render (List<(int i, int j)> moves) {
            foreach ((int i, int j) in moves) {
                int x = _store.Flip ? 7 - i : i;
                int y = _store.Flip ? 7 - j : j;

                _btnArr[x, y].BackColor = _store.ColorSettings.Green;
                if (_store.Board.PieceAt((i,j)).Color != "null")
                    _btnArr[x, y].BackColor = _store.ColorSettings.Red;
            }
        }

        public void UpdateCheckLabel (string newText) {
            _labelCheck.Text = newText;
        }
    }
}
