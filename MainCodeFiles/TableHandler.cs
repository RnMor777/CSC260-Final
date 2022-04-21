using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class TableHandler {
        private TableLayoutPanel _moveTable;
        private DataStore _store;
        private int _locationPrevView;

        public TableHandler(DataStore game) {
            _locationPrevView = 0;
            _store = game;
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
            addMove.BackColor = _store.ColorSettings.Curr;
            addMove.Height = 25;
            addMove.Text = move.PGN;
            addMove.Name = "" + movesMade;
            AddEventMove(movesMade, addMove, move);
            _moveTable.Controls.Add(addMove, movesMade % 2 + 1, movesMade / 2);
            _moveTable.HorizontalScroll.Visible = false;
        }

        public void RemoveTop() {
            int count = _moveTable.Controls.Count - 1;
            _moveTable.Controls.RemoveAt(count);
            if (_store.MovesMade % 2 == 0)
                _moveTable.Controls.RemoveAt(count - 1);

            if (_moveTable.Controls.Count > 0)
                _moveTable.Controls[_moveTable.Controls.Count - 1].BackColor = _store.ColorSettings.Curr;
        }

        public void AddEventMove (int pos, Label label, Moves move) {
            label.Click += (s, e) => {
                foreach (Label x in _moveTable.Controls) {
                    x.BackColor = System.Drawing.Color.Transparent;
                }
                if (pos == _store.MovesMade-1) {
                    _store.Block = false;
                    label.BackColor = _store.ColorSettings.Curr;
                }
                else {
                    _store.Block = true;
                    label.BackColor = _store.ColorSettings.Prev;
                }
                _locationPrevView = _store.MovesMade - pos - 1;
                _store.Renderer.Render(new Board(move.FEN));
            };
        }

        public void GoBack(bool all) {
            _store.Block = true;
            foreach (Label x in _moveTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }

            if (all) {
                _store.Renderer.Render(new Board());
                _locationPrevView = _store.MovesMade;
            }
            else if (_locationPrevView < _store.MovesMade-1) {
                _locationPrevView++;
                _store.Renderer.Render(new Board(_store.MoveHandler.MoveAt(_locationPrevView).FEN));
                _moveTable.Controls.Find("" + (_store.MovesMade - _locationPrevView - 1), false)[0].BackColor = _store.ColorSettings.Prev;
            }
            else {
                _locationPrevView = _store.MovesMade;
                _store.Renderer.Render(new Board());
            }
        }

        public void GoForward (bool all) {
            _store.Block = true;
            foreach (Label x in _moveTable.Controls) {
                x.BackColor = System.Drawing.Color.Transparent;
            }
            if (all) {
                _store.Renderer.Render();
                _store.Block = false;
                _locationPrevView = 0;
                _moveTable.Controls.Find("" + (_store.MovesMade - _locationPrevView - 1), false)[0].BackColor = _store.ColorSettings.Curr;
            }
            else if (_locationPrevView > 0) {
                _locationPrevView--;
                _store.Renderer.Render(new Board(_store.MoveHandler.MoveAt(_locationPrevView).FEN));
                _moveTable.Controls.Find("" + (_store.MovesMade - _locationPrevView - 1), false)[0].BackColor = _store.ColorSettings.Prev;
            }
            if (_locationPrevView == 0 && _store.MovesMade != 0) {
                _store.Block = false;
                _moveTable.Controls.Find("" + (_store.MovesMade - _locationPrevView - 1), false)[0].BackColor = _store.ColorSettings.Curr;
            }
        }
    }
}
