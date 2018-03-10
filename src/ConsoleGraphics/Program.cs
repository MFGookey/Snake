using System;
using System.Threading;

namespace SnakeWorks.Snake
{
  public class Program
  {
    static void Main(string[] args)
    {
      ISnakeDriver driver = new KeyboardSnakeDriver();
      IGame game = new Game(driver);
      game.RunGame();
      Console.Clear();
      Console.SetCursorPosition(0, 0);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.BackgroundColor = ConsoleColor.Black;
      Console.WriteLine($"Alive: {game.Alive}");
      Console.WriteLine($"Score: {game.Score}");
      Console.WriteLine($"Ticks: {game.Ticks}");
      Console.WriteLine($"TSLS: {game.TicksSinceLastScore}");
      Console.ReadKey(true);
    }
  }
}