/*
 * cardsdll wrapper class
 * Downloaded from www.publicjoe.co.uk
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the author(s) be held liable for any damages arising from
 * the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely.
 */

using Cards.Properties;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Cards
{
    /// <summary>
    /// 
    /// </summary>
    public enum CardSuit : int
    {
        Clubs = 0,
        Diamonds = 1,
        Hearts = 2,
        Spades = 3
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CardRank : int
    {
        Ace = 0,
        Two = 1,
        Three = 2,
        Four = 3,
        Five = 4,
        Six = 5,
        Seven = 6,
        Eight = 7,
        Nine = 8,
        Ten = 9,
        Jack = 10,
        Queen = 11,
        King = 12
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CardBack : int
    {
        Crosshatch = 53,
        Sky = 54,
        Mineral = 55,
        Fish = 56,
        Frog = 57,
        Moonflower = 58,
        Island = 59,
        Squares = 60,
        Magenta = 61,
        Sanddunes = 62,
        Space = 63,
        Lines = 64,
        Toycars = 65,
        Unused = 66,
        The_X = 67,
        The_O = 68
    }

    /// <summary>
    /// Mode
    /// </summary>
    public enum CardMode : int
    {
        RankCollated = 0, // card number starts from 0
        SuitCollated = 1, // card number offset by 1 for blank card.

        Highlight = 2, /* Same as RankCollated except drawn inverted */
        Ghost = 3, /* Draw a ghost card -- for ace piles */
        Remove = 4, /* draw background specified by color */
        InvisibleGhost = 5, /* ? */
        DeckX = 6, /* Draw X */
        DeckO = 7  /* Draw O */
    }

    /// <summary>
    /// Card
    /// </summary>
    [Serializable]
    public class Card : IDisposable
    {
        // Internal constants e.g. Magic Numbers.

        /// <summary>
        /// Default width used by card's DLL.
        /// </summary>
        internal const int DefaultWidth = 71;
        /// <summary>
        /// Default height used by card's DLL.
        /// </summary>
        internal const int DefaultHeight = 97;
        /// <summary>
        /// Default border mask value.
        /// </summary>
        internal const int BorderMask = 0x00FFFFFF;

        /// <summary>
        /// .NET Graphics surface used for drawing.
        /// </summary>
        private Graphics graphicsSurface;

        /// <summary>
        /// Win32 HDC surface use for Win32 drawing.
        /// </summary>
        private IntPtr graphicsDC;

        /// <summary>
        /// Last value from the card's DLL.
        /// </summary>
        private bool lastReturnValue;

        /// <summary>
        /// Is this instance current being disposed?
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Current Mode to draw Card
        /// </summary>
        private static int mode;
        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// Constructor which initialise access to the card's DLL.
        /// </summary>
        public Card()
        {
            int width = DefaultWidth;
            int height = DefaultHeight;
            mode = (int)CardMode.RankCollated;
            NativeMethods.cdtInit(ref width, ref height);
        }

        #region Implementation: IDisposable & Finaliser
        /// <summary>
        /// Instance finaliser for the class for unmanaged resources.
        /// </summary>
        ~Card()
        {
            Dispose(false);
        }

        /// <summary>
        /// Called by caller's when the instance is finished with.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal dispose function which release managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">true iff managed and unmanaged are being disposed of else unmanaged only.</param>
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false,  only the following code is executed.
                if (this.graphicsSurface != null && this.graphicsDC != IntPtr.Zero)
                {
                    this.graphicsSurface.ReleaseHdc(this.graphicsDC);
                    this.graphicsDC = IntPtr.Zero;
                }
                NativeMethods.cdtTerm();

                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (graphicsSurface != null)
                    {
                        graphicsSurface.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        #endregion

        #region Internal Helpers
        /// <summary>
        /// Make sure that the internally kept HDC is released, if it has been obtained from the graphics surface.
        /// </summary>
        private void ReleaseDC()
        {
            if (HasDC())
            {
                this.graphicsSurface.ReleaseHdc(this.graphicsDC);
                this.graphicsDC = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Make sure that we has a HDC to use, claim from graphics surface if we have not.
        /// </summary>
        private void EnsureDC()
        {
            if (!HasDC())
            {
                this.graphicsDC = this.graphicsSurface.GetHdc();
            }
        }

        /// <summary>
        /// Check if the HDC has been obtained from the graphics surface.
        /// </summary>
        /// <returns></returns>
        private bool HasDC()
        {
            return this.graphicsDC != IntPtr.Zero;
        }

        #endregion

        /// <summary>
        /// Called just before using the card drawing functions.
        /// </summary>
        /// <param name="graphicsSurface">Graphics object on which the cards are to be drawn.</param>
        public void Begin(Graphics graphicsSurface)
        {
            this.graphicsSurface = graphicsSurface;
            this.graphicsDC = IntPtr.Zero;
        }

        /// <summary>
        /// Called after card drawing has been complete to relase claimed resources.
        /// </summary>
        public void End()
        {
            ReleaseDC();
            this.graphicsSurface = null;
        }

        /// <summary>
        /// Used to return the last return value from a card's DLL call.
        /// </summary>
        public bool LastReturnValue
        {
            get { return lastReturnValue; }
        }

        public System.Drawing.Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            //a holder for the result
            Bitmap result = new Bitmap(width, height);
            //set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

        #region Drawing Functions
        /// <summary>
        /// Draws a card in a given mode with specified suit and rank.
        /// </summary>
        /// <param name="topLeft">Top left location to start drawing the card.</param>
        /// <param name="cardIndex">Card index (dependant on mode), use the ToCardIndex</param>
        public void DrawCard(Point topLeft, int cardIndex)
        {
            EnsureDC();
            lastReturnValue = NativeMethods.cdtDraw(this.graphicsDC, topLeft.X, topLeft.Y, cardIndex, mode, BorderMask);
        }

        /// <summary>
        /// Draws a card with a given background
        /// </summary>
        /// <param name="topLeft">Top left location to start drawing the card.</param>
        /// <param name="cardBack">Background image index. See CardBack enumeration.</param>
        public void DrawCardBack(Point topLeft, CardBack cardBack)
        {
            EnsureDC();
            lastReturnValue = NativeMethods.cdtDraw(this.graphicsDC, topLeft.X, topLeft.Y, (int)cardBack, (int)CardMode.SuitCollated, BorderMask);
        }

        /// <summary>
        /// Draws a frame of an animated background.
        /// </summary>
        /// <param name="topLeft">Top left location to start drawing the card.</param>
        /// <param name="cardBack">Background image index. see CardBack enumeration.</param>
        /// <param name="frameNo">Frame of animation to draw.</param>
        public void DrawCardBack(Point topLeft, CardBack cardBack, int frameNo)
        {
            EnsureDC();
            lastReturnValue = NativeMethods.cdtAnimate(this.graphicsDC, (int)cardBack, topLeft.X, topLeft.Y, frameNo);
        }

        /// <summary>
        /// Draw a card as if it was selected.
        /// </summary>
        /// <param name="topLeft">Top-left location to start drawing the card.</param>
        /// <param name="cardIndex">Index of card to draw.</param>
        public void DrawHighlightedCard(Point topLeft, int cardIndex)
        {
            EnsureDC();
            lastReturnValue = NativeMethods.cdtDraw(this.graphicsDC, topLeft.X, topLeft.Y, cardIndex, (int)CardMode.Highlight, BorderMask);
        }

        /// <summary>
        /// Draw empty card.
        /// </summary>
        /// <param name="topLeft">Top-left location to start drawing the card.</param>
        /// <param name="color">Colour to draw empty card.</param>
        public void DrawEmptyCard(Point topLeft, Color color)
        {
            EnsureDC();
            lastReturnValue = NativeMethods.cdtDraw(this.graphicsDC, topLeft.X, topLeft.Y, 1, (int)CardMode.Ghost, (int)color.ToArgb());
        }

        /// <summary>
        /// Draw an extruded card.
        /// </summary>
        /// <param name="topLeft">Top-left location to start drawing the card.</param>
        /// <param name="bottomRight">Bottom-right location to start drawing card.</param>
        /// <param name="cardIndex">Index of card to draw.</param>
        public void DrawExtrudedCard(Point topLeft, Point bottomRight, int cardIndex)
        {
            EnsureDC();
            Size d = new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            lastReturnValue = NativeMethods.cdtDrawExt(this.graphicsDC, topLeft.X, topLeft.Y, d.Width, d.Height, cardIndex, mode, BorderMask);
        }

        /// <summary>
        /// Internal version of the DrawCard routine which renders card to a bitmap.
        /// </summary>
        /// <param name="offScreenBitmap">Bitmap on which to draw image.</param>
        /// <param name="topLeft">Top-left location to start drawing the card.</param>
        /// <param name="cardIndex">Index of the card to draw.</param>
        private void InternalDrawCard(Bitmap offScreenBitmap, Point topLeft, int cardIndex)
        {
            // Make sure IDisposable's Dispose method is called even on a Exception.
            using (Graphics bitmapGraphics = Graphics.FromImage(offScreenBitmap))
            {
                IntPtr deviceContextHandle = IntPtr.Zero;
                try
                {
                    // Get a reference to the Win32 HDC
                    deviceContextHandle = bitmapGraphics.GetHdc();
                    // Draw card image onto bitmap.
                    lastReturnValue = NativeMethods.cdtDraw(deviceContextHandle, topLeft.X, topLeft.Y, cardIndex, mode, BorderMask);
                }
                finally
                {
                    if (deviceContextHandle != IntPtr.Zero)
                    {
                        // Release HDC if it was claimed.
                        bitmapGraphics.ReleaseHdc(deviceContextHandle);
                    }
                }
            }
        }

        /// <summary>
        /// Draw rotated a card using 3-point model.
        /// </summary>
        /// <param name="upperLeft">destination for the upper-left point of the original</param>
        /// <param name="upperRight">destination for the upper-right point of the original</param>
        /// <param name="lowerLeft">destination for the lower-left of the original</param>
        /// <param name="cardIndex">Index of the card to draw.</param>
        public void DrawRotatedCard(Point upperLeft, Point upperRight, Point lowerLeft, int cardIndex)
        {
            // Create bitmap big enough for just one card.
            using (Bitmap offScreenBitmap = new Bitmap(DefaultWidth, DefaultHeight))
            {
                // Draw card onto internal bitmap before rotation.
                //
                this.InternalDrawCard(offScreenBitmap, new Point(0, 0), cardIndex);

                // Must release HDC before drawing using the .NET Graphics object.        
                ReleaseDC();

                // Use destination point to render image
                Point[] destinationPoints = { upperLeft, upperRight, lowerLeft };
                //
                // Draw image onto the graphic surface using destination point as a guide.
                //
                graphicsSurface.DrawImage(offScreenBitmap, destinationPoints);
            }
        }

        /// <summary>
        /// Draw roated a card using an angle.
        /// </summary>
        /// <param name="upperLeft">Top-left location to start drawing the card.</param>
        /// <param name="angle">Angle from horizontal -ve is CCW, +ve is CW.</param>
        /// <param name="cardIndex">Index of card to draw.</param>
        public void DrawRotatedCard(Point upperLeft, int angle, int cardIndex)
        {
            // Create bitmap big enough for just one card.
            using (Bitmap offScreenBitmap = new Bitmap(DefaultWidth, DefaultHeight))
            {
                // Draw card onto internal bitmap before rotation.
                //
                InternalDrawCard(offScreenBitmap, new Point(0, 0), cardIndex);

                // Must release HDC before drawing using the .NET Graphics object.        
                ReleaseDC();

                // Convert Angle to Radians
                double radAngle = ((angle / 180.0) * Math.PI);

                Point upperRight = new Point(upperLeft.X + (int)(DefaultWidth * Math.Cos(radAngle)),
                                              upperLeft.Y + (int)(-DefaultWidth * Math.Sin(radAngle)));
                Point lowerLeft = new Point(upperLeft.X + (int)(DefaultHeight * Math.Sin(radAngle)),
                                             upperLeft.Y + (int)(DefaultHeight * Math.Cos(radAngle)));

                // Use destination point to render image
                Point[] destinationPoints = { upperLeft, upperRight, lowerLeft };
                //
                // Draw image onto the graphic surface using destination point as a guide.
                //
                graphicsSurface.DrawImage(offScreenBitmap, destinationPoints);
            }
        }
        #endregion

        #region Static Helpers
        /// <summary>
        /// Helper that converts a given Card's rank, suit, mode into the card index.
        /// </summary>
        /// <param name="suit">Suit to draw.</param>
        /// <param name="rank">Rank of card.</param>
        /// <returns></returns>
        public static int ToCardIndex(CardSuit suit, CardRank rank)
        {
            int cardNo = -1;

            switch (mode)
            {
                case 0: /*CardMode.RankCollated*/
                    cardNo = ((int)rank) * 4 + ((int)suit);
                    break;

                case 1: /* CardMode.SuitCollated */
                    cardNo = 1 + ((int)rank) + 13 * ((int)suit);
                    break;

                default:
                    cardNo = 0;
                    break;
            }
            return cardNo;
        }

        /// <summary>
        /// Helper that converts card's background image index into card index.
        /// </summary>
        /// <param name="back"></param>
        /// <returns></returns>
        public static int ToCardIndex(CardBack back)
        {
            return (int)back;
        }

        /// <summary>
        /// Helper that converts card index to Suit of Card.
        /// </summary>
        /// <param name="cardIndex">Card's index.</param>
        /// <returns></returns>
        public static CardSuit SuitFromCardIndex(int cardIndex)
        {
            if (mode == (int)CardMode.SuitCollated && (cardIndex >= 1 && cardIndex <= 52))
            {
                return (CardSuit)((cardIndex - 1) / 13);
            }
            else if (mode == (int)CardMode.RankCollated && (cardIndex >= 0 && cardIndex <= 51))
            {
                return (CardSuit)(cardIndex % 4);
            }
            else
            {
                throw new ApplicationException("Suite only valid to SuitCollated, RankCollated modes.");
            }
        }

        /// <summary>
        /// Helper that converts card index to Rank of Card.
        /// </summary>
        /// <param name="cardIndex"></param>
        /// <returns></returns>
        public static CardRank RankFromCardIndex(int cardIndex)
        {
            if (mode == (int)CardMode.SuitCollated && (cardIndex >= 1 && cardIndex <= 52))
            {
                return (CardRank)((cardIndex - 1) % 13);
            }
            else if (mode == (int)CardMode.RankCollated && (cardIndex >= 0 && cardIndex <= 51))
            {
                return (CardRank)(cardIndex / 4);
            }
            else
            {
                throw new ApplicationException("Rank only valid to SuitCollated, RankCollated modes.");
            }
        }
        #endregion
    }

    #region Native Methods
    internal class NativeMethods
    {
        private NativeMethods() { }

        /* public Interface to cards.dll */
        [DllImport("cards.dll")]
        public static extern bool cdtInit(ref int width, ref int height);

        [DllImport("cards.dll")]
        public static extern void cdtTerm();

        [DllImport("cards.dll")]
        public static extern bool cdtDraw(IntPtr hdc,
                                           int x,
                                           int y,
                                           int card,
                                           int mode,
                                           int color);

        [DllImport("cards.dll")]
        public static extern bool cdtDrawExt(IntPtr hdc,
                                              int x,
                                              int y,
                                              int dx,
                                              int dy,
                                              int card,
                                              int type,
                                              int color);

        [DllImport("cards.dll")]
        public static extern bool cdtAnimate(IntPtr hdc,
                                              int cardback,
                                              int x,
                                              int y,
                                              int frame);
    }
    #endregion
}
