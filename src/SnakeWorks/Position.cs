using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWorks.Snake
{
  public class Position : ICloneable, IEquatable<Position>
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Position() { }
    public Position(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    public object Clone()
    {
      return new Position(this.X, this.Y);
    }

    public bool Equals(Position other)
    {
      return (this.X == other.X && this.Y == other.Y);
    }
  }
}
