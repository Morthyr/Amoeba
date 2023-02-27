using Player;

namespace Learn;
class Q
{
    static Random _r = new(); 
    const double _lr = 0.8;
    const double _gamma = 0.95;
    internal StateActionTable _table;
    Gamer _gamer;
    A[] _actions;

    public double Eps { get; set; } = 1.0;
    
    public Q(Gamer gamer, StateActionTable table, A[] actions)
    {
        _table = table;
        _gamer = gamer;
        _actions = actions;
    }

    public A Select(bool?[,] obs)
    {
        A best = SelectBest(obs);
        if(_r.NextDouble() < Eps)
        {
            int idx = _r.Next(_actions.Length);
            return _actions[idx];
        }
        return best;
    }

    public A SelectBest(bool?[,] obs)
    {
        return _actions.MaxBy(a => _table[obs, a]);
    }

    public double SelectBestValue(bool?[,] obs)
    {
        return _actions.Max(a => _table[obs, a]);
    }

    public void Learn(bool?[,] obs0, bool?[,] obs1, double reward, A action, bool done)
    {
        double best = reward;
        if(!done)
            best = SelectBestValue(obs1);
        
        double target = reward + _gamma * best;
        double error = target - _table[obs0, action];
        _table[obs0, action] += _lr * error;
    }
}
