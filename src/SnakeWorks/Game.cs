﻿using System;
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

      int x = 20;
      int y = 20;
      int colourTog = 1;
      this.Alive = true;
      this.Score = 0;
      this.Ticks = 0;
      this.TicksSinceLastScore = 0;
      bool pelletOn = false;
      int pelletX = 0;
      int pelletY = 0;

      int[] xPoints;
      xPoints = new int[8] { 20, 19, 18, 17, 16, 15, 14, 13 };
      int[] yPoints;
      yPoints = new int[8] { 20, 20, 20, 20, 20, 20, 20, 20 };


      while (Alive)
      {
        if (pelletOn == false)
        {
          bool collide = false;
          pelletOn = true;
          pelletX = rnd.Next(4, Console.WindowWidth - 4);
          pelletY = rnd.Next(4, Console.WindowHeight - 4);

          for (int l = (xPoints.Length - 1); l > 1; l--)
          {
            if (xPoints[l] == pelletX & yPoints[l] == pelletY)
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
            Console.SetCursorPosition(pelletX, pelletY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = bgColor;
            Console.Write("#");
            pelletOn = true;
          }

        }
        Array.Resize<int>(ref xPoints, snakeLength);
        Array.Resize<int>(ref yPoints, snakeLength);

        System.Threading.Thread.Sleep(delay);
        this.Ticks++;
        this.TicksSinceLastScore++;
        colourTog++;
        direction = this.Driver.GetDirection(direction);

        switch (direction)
        {
          case Direction.Right:
            x += 1;
            break;
          case Direction.Left:
            x -= 1;
            break;
          case Direction.Down:
            y += 1;
            break;
          case Direction.Up:
            y -= 1;
            break;
        }

        xPoints[0] = x;
        yPoints[0] = y;

        for (int l = (xPoints.Length - 1); l > 0; l--)
        {
          xPoints[l] = xPoints[l - 1];
          yPoints[l] = yPoints[l - 1];
        }


        try
        {
          Console.SetCursorPosition(xPoints[0], yPoints[0]);
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
          Console.SetCursorPosition(xPoints[xPoints.Length - 1], yPoints[yPoints.Length - 1]);
        }
        catch (System.ArgumentOutOfRangeException)
        {
          Alive = false;
        }
        Console.BackgroundColor = bgColor;
        Console.Write(" ");

        if (x == pelletX & y == pelletY)
        {
          pelletOn = false;
          snakeLength += 1;
          delay -= delay / 16;
          new Thread(() => Console.Beep(320, 250)).Start();
          this.TicksSinceLastScore = 0;
        }

        for (int l = (xPoints.Length - 1); l > 1; l--)
        {
          if (xPoints[l] == xPoints[0] & yPoints[l] == yPoints[0])
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