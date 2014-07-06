using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    public interface IAIStrategy
    {
       SpadesCard makeMove(GameData gamedata);
        
    }
}
