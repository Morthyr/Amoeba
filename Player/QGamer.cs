using System.Collections.ObjectModel;
using System.Drawing;
using Learn;

namespace Player;
class QGamer : Gamer
{
    Q _q;
    const double _eps_decay = 0.999995;
    private double _eps = 1.0;
    internal StateActionTable _table;

    protected override void StartGame(int tableSize, int serieLength, bool first)
    {
        if(_table == null)
            _table = new();

        List<A> actions = new List<A>(tableSize * tableSize);
        
        for(int y = 0; y < tableSize; y++)
        {
            for(int x = 0; x < tableSize; x++)
            {
                actions.Add(new(){ X = x, Y = y});
            }
        }

        _q = new Q(this, _table, actions.ToArray());
        _eps *= _eps_decay;
        _q.Eps = _eps;
    }
    
    A _action;

    public void Step()
    {
        var obs0 = (bool?[,])Env.Table.Clone();

        _action = _q.Select(obs0);

        Env.TryStep();
        if(!Env.Done)
        {
            Env.TryStep();
        }

        var obs1 = (bool?[,])Env.Table.Clone();

        var reward = CalculateReward();
        _q.Learn(obs0, obs1, reward, _action, Env.Done);
    }

    double CalculateReward1()
    {
        double score = 0.0;
        if(Env._turn > 0)
        {
            score = 1 / (Env._turn + 1);
        }
        
        if(!Env.Done)
            return score;

        double myscore = Env.Scores[this] * score;
        return myscore;
    }

    double CalculateReward()
    {
        if(!Env.Done)
            return 0;

        double myscore = Env.Scores[this];
        if(myscore > 0)
            return myscore;
        return myscore;
    }

    public override Point PlaceMark(Point mark, ReadOnlyCollection<Point> marks)
    {
        return _action;
    }
}
