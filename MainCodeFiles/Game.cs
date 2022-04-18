using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC260_Final {
    internal class Game {
        private Board _board;
        private RenderHandler _renderer;
        private TableHandler _tableHandler;
        private List<IPlayer> _players;
        private ColorSettings _colorSettings;

        private string _playerTurn;
        private bool _flipBoard;
        private bool _block;
        private int _movesMade;


        private int _halfmoves;
        private Pieces _activePiece;
        private List<(int i, int j)> _activeMoves;
        private Stack<Moves> _previousMoves;
        private int _locationPrevView;


        public bool Flip {
            get { return _flipBoard; }
        }

        public bool Block {
            get { return _block; }
            set { _block = value; }
        }

        public int MovesMade {
            get { return _movesMade; }
        }

        public Board Board {
            get { return _board; }
        }

        public RenderHandler Renderer {
            get { return _renderer; }
        }

        public TableHandler TableHandler {
            get { return _tableHandler; }
        }

        public ColorSettings ColorSettings {
            get { return _colorSettings; }
        } 

        public IPlayer Player1 {
            get { return _players.Where(p => p.Id == 1).First(); }
        }

        public IPlayer Player2 {
            get { return _players.Where(p => p.Id == 2).First(); }
        }

        public Game () {
            _board = new Board ();
            _renderer = new RenderHandler(this);
            _tableHandler = new TableHandler(this);
            _colorSettings = new ColorSettings();
            //Dictionary<String, int> whiteCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            //Dictionary<String, int> blackCaptures = new Dictionary<String, int> { { "Pawn", 0 }, { "Rook", 0 }, { "Bishop", 0 }, { "Knight", 0}, { "Queen", 0} };
            //_captures = new Dictionary<String, Dictionary<String, int>> { { "White", whiteCaptures}, { "Black", blackCaptures } };
            _halfmoves = 0;
            _previousMoves = new Stack<Moves> ();
            _playerTurn = "White";
            _movesMade = 0;
            _block = false;
            _locationPrevView = 0;
        }

        public void Run () {
            _renderer.Render();
        }

        public void EscapeMove () {
            _activePiece = null;
            _renderer.Render();
        }

        public void FlipBoard () {
            _flipBoard = !_flipBoard;
            _renderer.Render();
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
