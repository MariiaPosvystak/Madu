using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class HorizontalLine
    {
        List<Point> pList;
        
        public HorizontalLine(int xLeft, int xReiht, int y, char sym)
        {
            pList = new List<Point>();
            for(int x = xLeft; x <= xReiht; x++)
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);
            }

        }
        public void Drow()
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }
    }
}
