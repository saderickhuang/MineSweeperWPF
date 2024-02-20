using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperWPF
{
    // <summary>
    /// Enums for the game 
    /// </summary>
    public class Enums
    {
        public enum Level
        {
            Easy = 10,
            Medium = 15,
            Hard = 20
        }

        public enum CellType
        {
            Empty,
            Mine,
            Number
        }

        public enum CellState
        {
            Covered,
            Uncovered,
            Flagged
        }
        public enum GameStatus
        {
            NotStarted,
            InProgress,
            Won,
            Lost
        }
    }
}
