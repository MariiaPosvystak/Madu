using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class HorizontalLine : Figure // гоизонтальная линия озраничения поля змейки
    {
        public HorizontalLine(int xLeft, int xReiht, int y, char sym)
        {
            pList = new List<Point>();
            for(int x = xLeft; x <= xReiht; x++)
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);
            }

        }
    }
}
