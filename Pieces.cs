using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal interface Pieces {
        string Name { get; }
        string Color { get; }
        int PieceWorth { get; }
        int[] CalculateMoves(int currentLocation);
    }
}
