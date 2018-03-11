using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWorks.Snake
{
  public class Position
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Position() { }
    public Position(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }
  }
}
