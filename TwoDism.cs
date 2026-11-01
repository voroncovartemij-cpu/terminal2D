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
    public class Character(char chr, ConsoleColor color, Pos pos)
    {
        public char Char { get; set; } = chr;
        public ConsoleColor CharacterColor { get; set; } = color;
        public Pos CharacterPos { get; set; } = pos;

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
            char chr,
            ConsoleColor color,
            List<Pos> positions,
            Action<Character> act,
            bool iswalkable,
            bool disposable
        )

    {
        public List<Pos> Positions { get; set; } = positions;
        public char Char { get; set; } = chr;
        public ConsoleColor Color { get; set; } = color;
        public Action<Character> Funct { get; set; } = act;
        public bool IsWalkable { get; set; } = iswalkable;
        public bool Disposable { get; set; } = disposable;

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
    public static void MoveDraw(Pos oldpos)
    {
        Console.SetCursorPosition(oldpos.X, oldpos.Y);
        Console.Write(' ');
        for (int i = 0; i < FunctionalBlocks.Count; i++)
        {
            if (FunctionalBlocks[i].Positions.Contains(oldpos))
            {
                Console.SetCursorPosition(oldpos.X, oldpos.Y);
                Console.ForegroundColor = FunctionalBlocks[i].Color;
                Console.Write(FunctionalBlocks[i].Char);
                Console.ResetColor();
            }
        }
        Console.SetCursorPosition(Chr.CharacterPos.X, Chr.CharacterPos.Y);
        Console.ForegroundColor = Chr.CharacterColor;
        Console.Write(Chr.Char);
        Console.ResetColor();
    }

    public static void ClearAll()
    {
        Chr = new(
            '@',
            ConsoleColor.White,
            new Pos(0, 0)
        );
        FunctionalBlocks.Clear();
    }
    public static void Start()//игровой цикл
    {
        Console.CursorVisible = false;
        Pos move = new(0, 0);
        Pos oldpos;
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
                oldpos = new(Chr.CharacterPos.X, Chr.CharacterPos.Y);
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
                            if (FunctionalBlocks[i].Disposable)
                            {
                                FunctionalBlocks[i].Positions.Remove(futurepos);
                            }
                            if (!FunctionalBlocks[i].IsWalkable)
                            {
                                confirm = false;
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
                MoveDraw(oldpos);
            }
        }
    }
}

