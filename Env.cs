using System.Drawing;

namespace Player;
class Env
{
    int _tableSize = 3;
    int _serieLength = 3;
    bool _first = true;

    Gamer[] _gamers;
    int _turn;
    Point _mark;
    List<Point> _marks;

    public bool?[,] Table { get; private set; }
    public Dictionary<Gamer, int> Scores { get; private set; }
    public bool Done { get; private set; }

    public void StartGame(int tableSize, int serieLength, bool first, Gamer gamer, Gamer enemy, Dictionary<Gamer, int> scores)
    {
        _tableSize = tableSize;
        _serieLength = serieLength;
        _first = first;

        _mark = new Point(-1, -1);
        _marks = new List<Point>(tableSize * tableSize);
        Table = new bool?[tableSize, tableSize];
        Scores = scores;
        Done = false;

        _gamers = new Gamer[] { first ? gamer : enemy, first ? enemy : gamer};

        _gamers[0].StartGame(this, tableSize, serieLength, first);
        _gamers[1].StartGame(this, tableSize, serieLength, first);

        if(!first)
        {
            TryStep();
        }
    }

    public void TryStep()
    {
        _mark = _gamers[_turn % 2].PlaceMark(_mark, _marks.AsReadOnly());
        if(IsOccupied())
        {
            Win(_turn + 1);
            Done = true;
        }
        else
        {
            _marks.Add(_mark);
            Table[_mark.X, _mark.Y] = _turn % 2 == 0;
            if(IsWinner())
            {
                Win(_turn);
                Done = true;
            }
            else if(IsFull())
            {
                Tie();
                Done = true;
            }
            else
            {
                _turn++;
            }
        }
    }

    private bool IsOccupied()
    {
        return Table[_mark.X, _mark.Y].HasValue;
    }

    private void Win(int turn)
    {
        Scores[_gamers[turn % 2]] = 3;
        Scores[_gamers[(turn + 1) % 2]] = 0;
    }

    private void Tie()
    {
        Scores[_gamers[0]] = 1;
        Scores[_gamers[1]] = 1;
    }

    private bool IsFull()
    {
        return Table.Cast<bool?>().All(c => c.HasValue);
    }

    private bool IsWinner()
    {
        return CheckHorizontal() || CheckVertical() || CheckDiagonal();
    }

    private bool CheckHorizontal()
    {
        bool isEnemy = (_turn) % 2 == 1;
        for(int y = 0; y < _tableSize; y++)
        {
            int count = 0;
            for(int x = 0; x < _tableSize; x++)
            {
                if(!Table[x, y].HasValue || Table[x,y] == isEnemy)
                    count = 0;
                else
                    count++;

                if(count == _serieLength)
                    return true;
            }
        }
        return false;
    }

    private bool CheckVertical()
    {
        bool isEnemy = (_turn) % 2 == 1;
        for(int x = 0; x < _tableSize; x++)
        {
            int count = 0;
            for(int y = 0; y < _tableSize; y++)
            {
                if(!Table[x, y].HasValue || Table[x,y] == isEnemy)
                    count = 0;
                else
                    count++;

                if(count == _serieLength)
                    return true;
            }
        }
        return false;
    }

    private bool CheckDiagonal()
    {
        bool isEnemy = (_turn) % 2 == 1;
        for(int i = 0; i < _tableSize; i++)
        {
            int cx = 0, cy = 0, cx2 = 0, cy2 = 0;
            for(int d = 0; d < _tableSize; d++)
            {

                if(i + d >= _tableSize || !Table[d, i + d].HasValue || Table[d, i + d] == isEnemy)
                {
                    cy = 0;
                }
                else
                {
                    cy++;
                }

                if(cy == _serieLength)
                    return true;

                if(i + d >= _tableSize || !Table[i + d, d].HasValue || Table[i + d, d] == isEnemy)
                {
                    cx = 0;
                }
                else
                {
                    cx++;
                }

                if(cx == _serieLength)
                    return true;

                ////

                if(_tableSize - 1 - d < 0 || !Table[d, _tableSize - 1 - d].HasValue || Table[d, _tableSize - 1 - d] == isEnemy)
                {
                    cy2 = 0;
                }
                else
                {
                    cy2++;
                }

                if(cy2 == _serieLength)
                    return true;

                
                if(_tableSize - 1 - d < 0 || !Table[_tableSize - 1 - d, d].HasValue || Table[_tableSize - 1 - d, d] == isEnemy)
                {
                    cx2 = 0;
                }
                else
                {
                    cx2++;
                }

                if(cx2 == _serieLength)
                    return true;
            }
        }
        return false;
    }
}
