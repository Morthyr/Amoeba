using System.Collections.ObjectModel;
using System.Drawing;

namespace Player;
abstract class Gamer
{
    public Env Env { get; private set; }

    public void StartGame(Env env, int tableSize, int serieLength, bool first)
    {
        Env = env;
        StartGame(tableSize, serieLength, first);
    }

    protected abstract void StartGame(int tableSize, int serieLength, bool first);
    public abstract Point PlaceMark(Point mark, ReadOnlyCollection<Point> marks);
}
