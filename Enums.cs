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
            Easy ,
            Medium ,
            Hard
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

        public enum CellContent
        {
            Num_0, 
            Num_1,
            Num_2,
            Num_3,
            Num_4,
            Num_5,
            Num_6,
            Num_7,
            Num_8,
            
            Mine
        }
    }
}
