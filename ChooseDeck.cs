using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cards
{
    public partial class ChooseDeck : Form
    {
        static Card card = new Card();
        public CardBack choosenCardBack = CardBack.Sky;
        public List<PictureBox> PictureBoxList = new List<PictureBox>();
        public ChooseDeck()
        {
            InitializeComponent();
        }

        private void ChooseDeck_ControlAdded(object sender, ControlEventArgs e)
        {
           
        }

        void picBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox senderControl = (PictureBox)sender;
            choosenCardBack = (CardBack)senderControl.Tag;
            this.DialogResult = DialogResult.OK;
            
            
            //throw new NotImplementedException();
        }

        void picBox_Paint(object sender, PaintEventArgs e)
        {

            PictureBox senderControl = (PictureBox)sender;
            card.Begin(e.Graphics);
            card.DrawCardBack(new Point(0, 0), (CardBack)senderControl.Tag);
            card.End();
          
            
            
        }

        private void ChooseDeck_Load(object sender, EventArgs e)
        {
            List<CardBack> unUsedCardBack = new List<CardBack>();
            unUsedCardBack.Add(CardBack.Crosshatch);
            unUsedCardBack.Add(CardBack.The_X);
            unUsedCardBack.Add(CardBack.The_O);
            unUsedCardBack.Add(CardBack.Unused);
            
            
            
            
            foreach(CardBack cb in Enum.GetValues(typeof(CardBack)))
            {

                if (unUsedCardBack.Contains(cb)) continue;
                PictureBoxList.Add(new PictureBox());
                int lastIndex = PictureBoxList.Count - 1;
                //set property
                PictureBoxList[lastIndex].Tag = cb;
                PictureBoxList[lastIndex].SizeMode = PictureBoxSizeMode.AutoSize;
                PictureBoxList[lastIndex].Height = 120;
                PictureBoxList[lastIndex].Cursor = Cursors.Hand;
            }

            foreach (PictureBox pb in PictureBoxList)
            {
                flowLayoutPanel.Controls.Add(pb);
            }
           
        }

        private void flowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            PictureBox picBox = (PictureBox)e.Control;
            picBox.Paint += new PaintEventHandler(picBox_Paint);
            picBox.MouseDoubleClick += new MouseEventHandler(picBox_MouseDoubleClick);
        }

        
       
    }
}
