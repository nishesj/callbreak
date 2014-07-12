using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cards
{
    /// <summary>
    /// states of the game 
    /// </summary>

    class IntroState : IGameState
    {
        string Name;
        public void Render()
        {
            // set player name
            NameBox box = NameBox.getNameBox();
            if (box.ShowDialog() == DialogResult.OK)
            {

                // write in registry for later refrence :-)
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Games\\CallBreak");
                if (regKey != null)
                {
                    regKey.SetValue("PlayerName", box.playerNameTextBox.Text.Trim());
                    regKey.Close();
                }
               Cards.Main.playerName = box.playerNameTextBox.Text.Trim();
            }
            else
            {
                Environment.Exit(0);
            }
        }


        public IntroState(ref string playerName)
        {
            Name = playerName;

        }

    }
}
