using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Cards
{
    [Serializable]
    public class HumanPlayer : player
    {


        public HumanPlayer(int id) : base(id, true) { }


        public bool  setName(string name)
        {
            if (name.Length > 1)
            {
                this.Name = name;
                return true;
            }           
            return false;

        }

        public override void InitializeName()
        {
                        
          
        }
        /// <summary>
        /// Show Bid Dialog and sets the player BID
        /// </summary>
        /// <param name="gData"></param>
        /// <returns></returns>
        public override int Bid(GameData gData)
        {

            BidForm BF = new BidForm(gData);
            BF.StartPosition = FormStartPosition.CenterParent;

            if (BF.ShowDialog() == DialogResult.OK)
            {
                return (int)BF.BidTrackBar.Value;
            }


            else return 0;
        }
    }
}
