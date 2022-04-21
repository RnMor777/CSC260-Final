using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class DataStore {
        private Game _game;
        private Board _board;
        private RenderHandler _renderer;
        private TableHandler _tableHandler;
        private List<IPlayer> _players;
        private ColorSettings _colorSettings;
        private MoveHandler _moveHandler;
        private GameScreenForm _form;

        private static volatile string _playerTurn;
        private bool _flipBoard;
        private bool _block;
        private int _movesMade;
        private int _halfmoves;

        public bool Flip {
            get { return _flipBoard; }
            set { _flipBoard = value; }
        }

        public bool Block {
            get { return _block; }
            set { _block = value; }
        }

        public int MovesMade {
            get { return _movesMade; }
            set { _movesMade = value; }
        }

        public string PlayerTurn {
            get { return _playerTurn; }
            set { _playerTurn = value; }
        }

        public Game Game {
            get { return _game; }
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

        public MoveHandler MoveHandler {
            get { return _moveHandler; }
        }

        public GameScreenForm Form {
            get { return _form; }
        }

        public IPlayer Player1 {
            get { return _players.Where(p => p.Id == 1).First(); }
        }

        public IPlayer Player2 {
            get { return _players.Where(p => p.Id == 2).First(); }
        }

        public IPlayer CurrentPlayer {
            get { return _players.Where(p => p.Color == _playerTurn).First(); }
        }

        public List<IPlayer> PlayerList {
            get { return _players; }
        }

        public DataStore(GameScreenForm form) {
            _form = form;
            _game = new Game(this);
            _board = new Board ();
            _renderer = new RenderHandler(this);
            _tableHandler = new TableHandler(this);
            _moveHandler = new MoveHandler(this);
            _colorSettings = new ColorSettings();
            _players = new List<IPlayer>();
            _players.Add(new HumanPlayer(this, "White", 1));
            _players.Add(new HumanPlayer(this, "Black", 2));

            _playerTurn = "White";
            _halfmoves = 0;
            _movesMade = 0;
            _block = false;
            _flipBoard = false;
        }
    }
}
