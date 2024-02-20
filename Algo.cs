using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperWPF
{
    class Algo
    {
         static List<MineButton> GenerateMineField(int size)
        {
            List<MineButton> buttonList = new List<MineButton>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MineButton btn = new MineButton();
                    btn.posX = i;
                    btn.posY = j;
                    buttonList.Add(btn);
                }
            }
            return buttonList;
        }
    }
}
