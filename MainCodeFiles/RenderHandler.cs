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
        private Game _game;
        private readonly Dictionary<String, char> _symbols = new Dictionary<string, char> { { "Pawn", '♟' }, { "Knight", '♞' }, { "Queen", '♛' }, { "Bishop", '♝' }, { "Rook", '♜' } };

        public RenderHandler(Game game) {
            _game = game;
        }

        public void AddWhiteCaps (Label label) {
            _whiteCaps = label;
        }

        public void AddBlackCaps (Label label) {
            _blackCaps = label;
        }

        public void AddCheckLabel (Label label) {
            _labelCheck = label;
        }

        public void AddBtnArray (Button[,] arr) {
            _btnArr = arr;
        }

        public void AddLetters (Label[] label) {
            _letterArr = label;
        }

        public void AddNumbers (Label[] label) {
            _numbArr = label;
        }

        public void Render () {
            Render(_game.Board);
        }

        public void Render (Board board) {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (!_game.Flip) {
                        _btnArr[i, j].Image = board.PieceAt((i, j)).Color != "null" ? board.PieceAt((i, j)).Image : null;
                    }
                    else {
                        _btnArr[i, j].Image = board.PieceAt((7-i, 7-j)).Color != "null" ? board.PieceAt((7-i, 7-j)).Image : null;
                    }
                    _btnArr[i, j].BackColor = (i + j) % 2 == 0 ? _game.ColorSettings.White : _game.ColorSettings.Black;
                }
                _letterArr[i].Text = "" + (char)(65 + (_game.Flip ? (7 - i) : i));
                _numbArr[i].Text = "" + (char)(49 + (_game.Flip ? (7 - i) : i));
            }

            StringBuilder labText = new StringBuilder();
            foreach (var x in _game.Player1.Captures) 
                labText.Append(_symbols[x.Key], x.Value);
            _whiteCaps.Text = labText.ToString();

            labText = new StringBuilder();
            foreach (var x in _game.Player2.Captures) 
                labText.Append(_symbols[x.Key], x.Value);
            _blackCaps.Text = labText.ToString();
        }

        public void UpdateCheckLabel (string newText) {
            _labelCheck.Text = newText;
        }
    }
}
