using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class TableHandler {
        private TableLayoutPanel _moveTable;
        private Game _game;

        public TableHandler(Game game) {
            _game = game;
        }

        public void AddTable(TableLayoutPanel table) {
            _moveTable = table;
            _moveTable.AutoScroll = false;
            _moveTable.HorizontalScroll.Enabled = false;
            _moveTable.HorizontalScroll.Visible = false;
            _moveTable.AutoScroll = true;
        }

        public void UpdateTable (Moves move, int movesMade) {
            if (_moveTable.Controls.Count > 0) 
                _moveTable.Controls[_moveTable.Controls.Count - 1].BackColor = System.Drawing.Color.Transparent;

            if (movesMade % 2 == 0) {
                Label turn = new Label();
                turn.Height = 25;
                turn.Text = (movesMade/2+1).ToString();
                _moveTable.Controls.Add(turn, 0, movesMade/2);
                _moveTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }
            
            Label addMove = new Label();
            addMove.BackColor = _game.ColorSettings.Curr;
            addMove.Height = 25;
            addMove.Text = move.PGN;
            addMove.Name = "" + movesMade;
            AddEventMove(movesMade, addMove);
            _moveTable.Controls.Add(addMove, movesMade % 2 + 1, movesMade / 2);
            _moveTable.HorizontalScroll.Visible = false;
        }

        public void AddEventMove (int pos, Label label) {
            label.Click += (s, e) => {
                foreach (Label x in _moveTable.Controls) {
                    x.BackColor = System.Drawing.Color.Transparent;
                }
                if (pos == _game.MovesMade-1) {
                    _game.Block = false;
                    label.BackColor = _game.ColorSettings.Curr;
                }
                else {
                    _game.Block = true;
                    label.BackColor = _game.ColorSettings.Prev;
                }
                _locationPrevView = _game.MovesMade - pos - 1;
                _game.Renderer.Render(new Board(_previousMoves.ElementAt(_previousMoves.Count - pos - 1).FEN));
            };
        }

        public void GoBack(bool all) {
            _game.Block = true;
            foreach (Label x in _moveTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }

            if (all) {
                _game.Renderer.Render(new Board());
                _locationPrevView = _game.MovesMade;
            }
            else if (_locationPrevView < _game.MovesMade-1) {
                _locationPrevView++;
                _game.Renderer.Render(new Board(_previousMoves.ElementAt(_locationPrevView).FEN));
                _moveTable.Controls.Find("" + (_game.MovesMade - _locationPrevView - 1), false)[0].BackColor = nordPrev;
            }
            else {
                _locationPrevView = _game.MovesMade;
                _game.Renderer.Render(new Board());
            }
        }

        public void GoForward (bool all) {
            _game.Block = true;
            foreach (Label x in _moveTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }
            if (all) {
                _game.Renderer.Render();
                _game.Block = false;
                _locationPrevView = 0;
                _moveTable.Controls.Find("" + (_game.MovesMade - _locationPrevView - 1), false)[0].BackColor = _game.ColorSettings.Curr;
            }
            else if (_locationPrevView > 0) {
                _locationPrevView--;
                _game.Renderer.Render(new Board(_previousMoves.ElementAt(_locationPrevView).FEN));
                _moveTable.Controls.Find("" + (_game.MovesMade - _locationPrevView - 1), false)[0].BackColor = _game.ColorSettings.Prev;
            }
            if (_locationPrevView == 0 && _game.MovesMade != 0) {
                _game.Block = false;
                _moveTable.Controls.Find("" + (_game.MovesMade - _locationPrevView - 1), false)[0].BackColor = _game.ColorSettings.Curr;
            }
        }
    }
}
