using System.Drawing;

class Saver
{
    internal static string StateToString(bool?[,] state) => new string(state.Cast<bool?>().Select(c => c switch
    {
        true => 'x',
        false => 'o',
        _ => '.'
    }).ToArray());

    static bool?[,] StringToState(string stateString)
    {
        int tableSize = (int)Math.Sqrt(stateString.Length);
        bool?[,] table = new bool?[tableSize, tableSize];
        var values = stateString.Select<char, bool?>(c => c switch
        {
            'x' => true,
            'o' => false,
            _ => null
        }).ToList();

        for(int i = 0; i < stateString.Length; i++)
        {
            table[i % tableSize, i / tableSize] = values[i];
        }

        return table;
    }

    static double[] StateToValues(bool?[,] state) => state.Cast<bool?>().Select(c => c switch
    {
        true => 1.0,
        false => -1.0,
        _ => 0
    }).ToArray();

    static double[] StringToValues(string stateString) => stateString.Select(c => c switch
    {
        'x' => 1.0,
        'o' => -1.0,
        _ => 0
    }).ToArray();

    public void Save(Dictionary<Learn.StateAction, double> data, string file)
    {
        using StreamWriter w = new(file);
        foreach(var (key, value) in data)
        {
            w.Write(StateToString(key.State));
            w.Write('+');
            w.Write(key.Action.X);
            w.Write(',');
            w.Write(key.Action.Y);
            w.Write(' ');
            w.WriteLine(value);
        }
        return;
        
        var states = data.Select(d => StateToString(d.Key.State)).Distinct();

        var best = states.ToDictionary(d => d, d =>
        {
            Learn.StateAction sa = new (){ State = StringToState(d) };
            Point max = new Point(0,0); double maxValue = 0; 
            for(int y = 0; y < sa.State.GetLength(0); y++)
            {
                for(int x = 0; x < sa.State.GetLength(0); x++)
                {
                    sa.Action = new Point(x, y);
                    double value = 0;
                    if(data.ContainsKey(sa))
                        value = data[sa];

                    if(value > maxValue)
                    {
                        maxValue = value;
                        max = sa.Action;
                    }
                }
            }
            return max;
        });

        using StreamWriter sw = new(file);
        foreach(var (key, value) in best)
        {
            sw.Write(key);
            sw.Write('+');
            sw.Write(value.X);
            sw.Write(',');
            sw.WriteLine(value.Y);
        }
    }
}