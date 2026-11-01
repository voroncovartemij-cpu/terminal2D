static class D2
{
    //функция заглушка
    public static void Nothing() { }
    //координаты
    public struct Pos(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }
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
            Action act,
            bool iswalkable,
            bool disposable
        )

    {
        public int Id = id;
        public List<Pos> Positions = positions;
        public char Char = chr;
        public ConsoleColor Color = color;
        public Action Funct = act;
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
    public static List<FunctionalBlock> FunctionalBlocks = [
        //удаляемо
        new (
            1,
            '#',
            ConsoleColor.Red,
            [new (10, 10) ],
            Nothing,
            false,
            false
        ),
        new (
            2,
            '$',
            ConsoleColor.Green,
            [ new (20, 10) ],
            Nothing,
            true,
            false
        ),
        new (
            3,
            '%',
            ConsoleColor.Yellow,
            [  new (30, 10) ],
            Nothing,
            true,
            false
        ),
        new (
            4,
            '&',
            ConsoleColor.Blue,
             [new (40, 10)],
            Nothing,
            false,
            false
        ),
        new (
            5,
            '~',
            ConsoleColor.Cyan,
            [new (50, 10) ],
            Nothing,
            true,
            true
        ),
        //вот до сюда
        ];
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
        Pos Move;
        bool Confirm;
        Draw();
        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (Chr.CharacterPos.Y > 0)
                        {
                            Move = new Pos(0, -1);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Chr.CharacterPos.Y < Console.WindowHeight - 1)
                        {
                            Move = new Pos(0, 1);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (Chr.CharacterPos.X > 0)
                        {
                            Move = new Pos(-1, 0);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (Chr.CharacterPos.X < Console.WindowWidth - 1)
                        {
                            Move = new Pos(1, 0);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }

                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.CursorVisible = true;
                        return;
                    default:
                        Move = new Pos(0, 0);
                        break;
                }
                Confirm = true;
                Pos futurepos = new(Chr.CharacterPos.X + Move.X, Chr.CharacterPos.Y + Move.Y);
                for (int i = 0; i < FunctionalBlocks.Count; i++)
                {
                    if (FunctionalBlocks[i].Positions.Contains(futurepos))
                    {
                        FunctionalBlocks[i].Funct();
                        if (!FunctionalBlocks[i].IsWalkable)
                        {
                            Confirm = false;
                        }
                        if (FunctionalBlocks[i].Disposable)
                        {
                            FunctionalBlocks[i].Positions.RemoveAll(f => f.X == futurepos.X && f.Y == futurepos.Y);
                        }
                    }
                }
                if (Confirm)
                {
                    Chr.CharacterPos = new Pos(futurepos.X, futurepos.Y);
                }
                Draw();
            }
        }
    }
}

