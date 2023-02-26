using Player;

Env env = new();
QGamer gamer = new();
RandomGamer enemy = new();

int tableSize = 3;
int serieLength = 3;
Dictionary<Gamer, int> scores = new();

try
{
    int cycles = 10000;
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
s.Save(gamer._q._table._stateAction, args[0] + "\\3_3.model");