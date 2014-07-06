using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    public class GameStateManager
    {
        private IGameState state;
        public void Process()
        {
            if (state != null)
            {
                state.Render();
            }
        }
        public IGameState switchState(IGameState newState)
        {
            IGameState oldState = state;
            state = newState;
            return (oldState);
        }
        public GameStateManager(IGameState initialState)
        {
            state = initialState;
        }
    }
}
