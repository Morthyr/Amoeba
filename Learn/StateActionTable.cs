namespace Learn;
class StateActionTable
{
    internal Dictionary<StateAction, double> _stateAction = new();

    static object _lockObj = new();

    private double Create(StateAction sa)
    {
        lock(_lockObj)
        {
            double r = sa.State[sa.Action.X, sa.Action.Y].HasValue ? double.MinValue : 0.0;
            if(!_stateAction.ContainsKey(sa))
            {
                if(!_stateAction.TryAdd(sa, r))
                {
                    Console.WriteLine("Couldn't set {0} to {1}", sa, r);
                }
            }
            return r;
        }
    }

    public double this[bool?[,] state, A action]
    {
        get 
        {
            StateAction sa = new() { State = (bool?[,])state.Clone(), Action = new() { X = action.X, Y = action.Y } };
            if(!_stateAction.ContainsKey(sa))
            {
                return Create(sa);
            }
            return _stateAction[sa];
        }
        set 
        {
            StateAction sa = new() { State = (bool?[,])state.Clone(), Action = new() { X = action.X, Y = action.Y } };
            if(!_stateAction.ContainsKey(sa))
            {
                Create(sa);
            }
            _stateAction[sa] = value;
        }
    }
}
