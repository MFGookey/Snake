using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWorks.Snake
{
  class KeyboardSnakeDriver : ISnakeDriver
  {
    public Direction GetDirection(Direction currentDirection)
    {
      if (Console.KeyAvailable)
      {
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
          case ConsoleKey.RightArrow:
            if (currentDirection != Direction.Left)
            {
              return Direction.Right;
            }
            break;
          case ConsoleKey.LeftArrow:
            if (currentDirection != Direction.Right)
            {
              return Direction.Left;
            }
            break;
          case ConsoleKey.UpArrow:

            if (currentDirection != Direction.Down)
            {
              return Direction.Up;
            }
            break;
          case ConsoleKey.DownArrow:

            if (currentDirection != Direction.Up)
            {
              return Direction.Down;
            }
            break;
          default:
            break;
        }
      } //Inputs & direction
      return currentDirection;
    }
  }
}
