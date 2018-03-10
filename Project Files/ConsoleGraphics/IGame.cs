namespace SnakeWorks.Snake
{
  public interface IGame
  {
    int Score { get; }

    int Ticks { get; }

    int TicksSinceLastScore { get; }

    bool Alive { get; }

    void RunGame();
  }
}
