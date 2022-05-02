using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal interface IPlayer {
        Dictionary <string, int> Captures { get; }
        string Name { get; }
        int Id { get; }
        string Color { get; }

        void AddCapture(string name);
        void RemoveCapture(string name);
        //Task<Moves> TakeTurn();
        Moves TakeTurn();
    }
}
