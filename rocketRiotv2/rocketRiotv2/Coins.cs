using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Media;
using System.IO;


namespace rocketRiotv2
{
    public class Coin
    {
        int quadrant;
        Random random;
        Canvas canvas = new Canvas();
        public Rectangle coin = new Rectangle();
        BitmapImage bitmapImage;
        ImageBrush coinFill;


        public Coin(int q, Canvas c, Random r)
        {
            quadrant = q;
            canvas = c;
            random = r;
            //quadrant is not yet used
        }
        public void generate()
        {
            try
            {
                canvas.Children.Remove(coin);
            }
            catch
            {

            }
            //Image
            bitmapImage = new BitmapImage(new Uri("coin.png", UriKind.Relative));
            coinFill = new ImageBrush(bitmapImage);
            coin.Fill = coinFill;

            //Coin size
            coin.Height = 20;
            coin.Width = 20;

            //Set Position

            Canvas.SetLeft(coin, random.Next(180) + quadrant * 200);
            Canvas.SetTop(coin, random.Next(581));

            //Add to canvas
            canvas.Children.Add(coin);
        }

        public void remove()
        {
            canvas.Children.Remove(coin);
        }

        public PointCollection locations()
        {
            PointCollection returnPoints = new PointCollection();
            double pointX = Canvas.GetLeft(coin);
            double pointY = Canvas.GetTop(coin);
            returnPoints.Add(new Point(pointX, pointY + 10));
            returnPoints.Add(new Point(pointX + 20, pointY + 10));
            returnPoints.Add(new Point(pointX + 10, pointY));
            returnPoints.Add(new Point(pointX + 10, pointY + 20));
            return returnPoints;
        }
    }
}