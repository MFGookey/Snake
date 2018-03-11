using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeWorks.Snake
{
  public class Game : AbstractGame, IGame
  {

    public Game(ISnakeDriver driver) : base(driver)
    {
      Driver = driver;
      Score = 0;
      Ticks = 0;
      TicksSinceLastScore = 0;
      Alive = true;
    }

    public int Score { get; private set; }

    public int Ticks { get; private set; }

    public int TicksSinceLastScore { get; private set; }

    public bool Alive { get; private set; }

    private ISnakeDriver Driver { get; set; }

    public void RunGame()
    {
      Console.CursorVisible = (false);
      Console.Title = "Snaaaaake!";


      Console.SetWindowSize(56, 38);

      Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.BackgroundColor = ConsoleColor.Black;
      Console.Clear();

      ConsoleColor bgColor = Console.BackgroundColor;
      ConsoleColor fgColor = Console.ForegroundColor;
      int delay = 150;
      Direction direction = Direction.Right;

      int snakeLength = 8;

      Random rnd = new Random();

      Position currentPosition = new Position(20, 20);

      int colourTog = 1;
      this.Alive = true;
      this.Score = 0;
      this.Ticks = 0;
      this.TicksSinceLastScore = 0;
      bool pelletOn = false;
      var pelletPosition = new Position(0, 0);


      var points = new Position[] {
        new Position { X=20, Y=20 },
        new Position { X=19, Y=20 },
        new Position { X=18, Y=20 },
        new Position { X=17, Y=20 },
        new Position { X=16, Y=20 },
        new Position { X=15, Y=20 },
        new Position { X=14, Y=20 },
        new Position { X=13, Y=20 }
      };

      while (Alive)
      {
        if (pelletOn == false)
        {
          bool collide = false;
          pelletOn = true;
          pelletPosition.X = rnd.Next(4, Console.WindowWidth - 4);
          pelletPosition.Y = rnd.Next(4, Console.WindowHeight - 4);

          for (int l = (points.Length - 1); l > 1; l--)
          {
            if (points[l].Equals(pelletPosition))
            {
              collide = true;
            }
          }
          if (collide == true)
          {
            pelletOn = false;
            break;
          }
          else
          {
            Console.SetCursorPosition(pelletPosition.X, pelletPosition.Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = bgColor;
            Console.Write("#");
            pelletOn = true;
          }

        }


        Array.Resize<Position>(ref points, snakeLength);
        

        System.Threading.Thread.Sleep(delay);
        this.Ticks++;
        this.TicksSinceLastScore++;
        colourTog++;
        direction = this.Driver.GetDirection(direction);

        switch (direction)
        {
          case Direction.Right:
            currentPosition.X += 1;
            break;
          case Direction.Left:
            currentPosition.X -= 1;
            break;
          case Direction.Down:
            currentPosition.Y += 1;
            break;
          case Direction.Up:
            currentPosition.Y -= 1;
            break;
        }

        points[0] = (Position)currentPosition.Clone();

        for (int l = (points.Length - 1); l > 0; l--)
        {
          points[l] = points[l - 1];
        }


        try
        {
          Console.SetCursorPosition(points[0].X, points[0].Y);
        }
        catch (System.ArgumentOutOfRangeException)
        {
          Alive = false;
        }
        if (colourTog == 2)
        {
          Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        else
        {
          colourTog = 1;
          Console.BackgroundColor = ConsoleColor.Green;
        }
        Console.ForegroundColor = fgColor;
        Console.Write("*");

        try
        {
          Console.SetCursorPosition(points[points.Length-1].X, points[points.Length - 1].Y);
        }
        catch (System.ArgumentOutOfRangeException)
        {
          Alive = false;
        }
        Console.BackgroundColor = bgColor;
        Console.Write(" ");

        if (currentPosition.Equals(pelletPosition))
        {
          pelletOn = false;
          snakeLength += 1;
          delay -= delay / 16;
          new Thread(() => Console.Beep(320, 250)).Start();
          this.TicksSinceLastScore = 0;
        }

        for (int l = (points.Length - 1); l > 1; l--)
        {
          if (points[0].Equals(points[l]))
          {
            Alive = false;
          }

        }
        Score = ((snakeLength) - 8);
        Console.SetCursorPosition(2, 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("Score: {0} ", Score);

      }
      new Thread(() => Console.Beep(37, 1)).Start();
      Console.BackgroundColor = ConsoleColor.Black;
      Console.Clear();


      Console.Beep(831, 250);


      Console.Beep(785, 250);

      ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

      for (int i = 0; i < 1; i++)
      {
        foreach (var color in colors)
        {
          Console.SetCursorPosition(0, 0);
          Console.ForegroundColor = color;
          Console.Clear();
          Console.WriteLine("\n\n\n\n\n");
          Console.WriteLine("\n                       Game over :(");
          Console.WriteLine("\n\n                   Your score was: {0} !", Score);
          System.Threading.Thread.Sleep(100);
        }
      }
      Thread.Sleep(1000);
      Console.WriteLine("\n\n\n\n\n\n             -- Press Any Key To Continue --");
      Thread.Sleep(500);
      Console.ReadKey(true);
      Console.ReadKey(true);
    }
  }
}