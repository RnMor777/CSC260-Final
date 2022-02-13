using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Board {

        private Pieces[,] _pieces;

        public Board () {
            _pieces = new Pieces[8,8];
            _pieces[0, 0] = new Rook("Black", 0, 0);
            _pieces[0, 1] = new Knight("Black", 0, 1);
            _pieces[0, 2] = new Bishop("Black", 0, 2);
            _pieces[0, 3] = new Queen("Black", 0, 3);
            _pieces[0, 4] = new King("Black", 0, 4);
            _pieces[0, 5] = new Bishop("Black", 0, 5);
            _pieces[0, 6] = new Knight("Black", 0, 6);
            _pieces[0, 7] = new Rook("Black", 0, 7);
            _pieces[7, 0] = new Rook("White", 7, 0);
            _pieces[7, 1] = new Knight("White", 7, 1);
            _pieces[7, 2] = new Bishop("White", 7, 2);
            _pieces[7, 3] = new Queen("White", 7, 3);
            _pieces[7, 4] = new King("White", 7, 4);
            _pieces[7, 5] = new Bishop("White", 7, 5);
            _pieces[7, 6] = new Knight("White", 7, 6);
            _pieces[7, 7] = new Rook("White", 7, 7);
            for (int i=0; i<8; i++) {
                _pieces[1, i] = new Pawn("Black", 1, i);
                _pieces[6, i] = new Pawn("White", 6, i);
            }
        }

        public Board (string fen) {

        }

        public void Render () {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (_pieces[i,j] != null) {
                        GameScreenForm.BtnArr[i, j].Image = _pieces[i, j].Image; 
                    }
                    else {
                        GameScreenForm.BtnArr[i, j].Image = null;
                    }
                }
            }
        }
    }
}
