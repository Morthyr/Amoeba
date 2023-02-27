using Player;

Env env = new();
QGamer gamer = new();
RandomGamer enemy = new();
Dictionary<Gamer, int> scores = new();

int tableSize = int.Parse(args[0]);
int serieLength = int.Parse(args[1]);
string dir = string.Empty;
if(args.Length > 2)
    dir = args[2] + "\\";

try
{
    int cycles = 100000;
    int cycle = 0;
    while(cycle++ < cycles)
    {
        scores.Clear();
        bool first = cycle % 2 == 0;
        Console.WriteLine("Starting game: {0}", cycle);
        env.StartGame(tableSize, serieLength, first, gamer, enemy, scores);
        while(!env.Done)
            gamer.Step();

        Console.WriteLine("Game: {0}, result: {1} - {2}", cycle, env.Scores[gamer], env.Scores[enemy]);
    }
}
catch(Exception)
{
    throw;
}

Saver s = new();
s.Save(gamer._table._stateAction, dir + $"{tableSize}_{serieLength}.model");