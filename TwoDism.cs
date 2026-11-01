static class TwoDism
{
    //функция заглушка
    public static void Nothing(Character character) { }
    public static void AddBlock(FunctionalBlock functionalBlock)
    {
        FunctionalBlocks.Add(functionalBlock);
    }
    //координаты
    public readonly record struct Pos(int X, int Y);
    //игрок класс
    public class Character(int id, char chr, ConsoleColor color, Pos pos)
    {
        public int Id = id;
        public char Char = chr;
        public ConsoleColor CharacterColor = color;
        public Pos CharacterPos = pos;

        public void SelfDraw()
        {
            Console.SetCursorPosition(CharacterPos.X, CharacterPos.Y);
            Console.ForegroundColor = CharacterColor;
            Console.Write(Char);
            Console.ResetColor();
        }
    }
    //функ блок класс
    public class FunctionalBlock(
            int id,
            char chr,
            ConsoleColor color,
            List<Pos> positions,
            Action<Character> act,
            bool iswalkable,
            bool disposable
        )

    {
        public int Id = id;
        public List<Pos> Positions = positions;
        public char Char = chr;
        public ConsoleColor Color = color;
        public Action<Character> Funct = act;
        public bool IsWalkable = iswalkable;
        public bool Disposable = disposable;

        public void SelfDraw()
        {
            foreach (Pos block in Positions)
            {
                Console.SetCursorPosition(block.X, block.Y);
                Console.ForegroundColor = Color;
                Console.Write(Char);
                Console.ResetColor();
            }
        }
    }

    //игрок
    public static Character Chr = new(
        0,
        '@',
        ConsoleColor.White,
        new Pos(0, 0)
    );
    //список функ блоков
    public static List<FunctionalBlock> FunctionalBlocks = [];
    public static void Draw()//отрисовка
    {
        Console.Clear();
        for (int i = 0; i < FunctionalBlocks.Count; i++)
        {
            FunctionalBlocks[i].SelfDraw();
        }
        Chr.SelfDraw();
    }

    public static void Start()//игровой цикл
    {
        Console.Clear();
        Console.CursorVisible = false;
        Pos move = new(0, 0);
        bool confirm;
        Draw();
        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        move = new Pos(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        move = new Pos(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        move = new Pos(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        move = new Pos(1, 0);
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.CursorVisible = true;
                        return;
                }

                confirm = true;
                Pos futurepos = new(Chr.CharacterPos.X + move.X, Chr.CharacterPos.Y + move.Y);
                if (futurepos.X < 0 || futurepos.X > Console.WindowWidth - 1 || futurepos.Y < 0 || futurepos.Y > Console.WindowHeight - 1)
                {
                    futurepos = Chr.CharacterPos;
                }
                List<Action<Character>> act = [];
                if (Chr.CharacterPos != futurepos)
                {
                    for (int i = 0; i < FunctionalBlocks.Count; i++)
                    {
                        if (FunctionalBlocks[i].Positions.Contains(futurepos))
                        {
                            act.Add(FunctionalBlocks[i].Funct);
                            if (!FunctionalBlocks[i].IsWalkable)
                            {
                                confirm = false;
                            }
                            if (FunctionalBlocks[i].Disposable)
                            {
                                FunctionalBlocks[i].Positions.Remove(futurepos);
                            }
                        }
                    }
                }
                if (confirm)
                {
                    Chr.CharacterPos = futurepos;
                }
                for (int i = 0; i < act.Count; i++)
                {
                    act[i](Chr);
                }
                move = new(0, 0);
                Draw();
            }
        }
    }
}

