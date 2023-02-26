using System.Collections.ObjectModel;
using System.Drawing;

namespace Player;
class RandomGamer : Gamer
{
    static Random _r = new();        
    int _tableSize;

    protected override void StartGame(int tableSize, int serieLength, bool first)
    {
        _tableSize = tableSize;
    }

    public override Point PlaceMark(Point mark, ReadOnlyCollection<Point> marks)
    {
        List<Point> all = new();
        for(int y = 0; y < _tableSize; y++)
        {
            for(int x = 0; x < _tableSize; x++)
            {
                all.Add(new(x, y));
            }
        }

        var avail = all.Except(marks).ToArray();
        return avail[_r.Next(0, avail.Length)];
    }

}
