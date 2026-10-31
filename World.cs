static class D2
{
    public static void Nothing() { }

    public struct Pos
    {
        public int X;
        public int Y;

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class FunctionalBlock
    {
        public int Id;
        public List<Pos> Positions = new List<Pos> { new Pos(50, 10) };
        public char Char;
        public ConsoleColor Color;
        public Action Funct;
        public bool IsWalkable;

        public FunctionalBlock(int id, char chr, ConsoleColor color, Action act, bool iswalkable)
        {
            Id = id;
            Char = chr;
            Color = color;
            Funct = act;
            IsWalkable = iswalkable;
        }

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

    //parametrs
    public static char Character = '@';
    public static ConsoleColor CharcherColor = ConsoleColor.White;
    public static char Block = '#';
    public static ConsoleColor BlockColor = ConsoleColor.Red;
    public static char Func1Block = '$';
    public static ConsoleColor Func1BlockColor = ConsoleColor.Green;
    public static Action Function1 = Nothing;
    public static char Func2Block = '%';
    public static ConsoleColor Func2BlockColor = ConsoleColor.Yellow;
    public static Action Function2 = Nothing;
    public static char Func3Block = '&';
    public static ConsoleColor Func3BlockColor = ConsoleColor.Blue;
    public static Action Function3 = Nothing;

    //
    public static List<FunctionalBlock> FunctionalBlocks = new List<FunctionalBlock>
    {
        new FunctionalBlock(1, '?', ConsoleColor.Cyan, Nothing, false),
    };

    public static List<Pos> Blocks = new List<Pos> { new Pos(10, 10) };
    public static List<Pos> Funcs1 = new List<Pos> { new Pos(20, 10) };
    public static List<Pos> Funcs2 = new List<Pos> { new Pos(30, 10) };
    public static List<Pos> Funcs3 = new List<Pos> { new Pos(40, 10) };

    //gameloop
    public static void Draw(Pos Chr)
    {
        Console.Clear();
        foreach (Pos block in Blocks)
        {
            Console.SetCursorPosition(block.X, block.Y);
            Console.ForegroundColor = BlockColor;
            Console.Write(Block);
            Console.ResetColor();
        }
        foreach (Pos funcblock in Funcs1)
        {
            Console.SetCursorPosition(funcblock.X, funcblock.Y);
            Console.ForegroundColor = Func1BlockColor;
            Console.Write(Func1Block);
            Console.ResetColor();
        }
        foreach (Pos funcblock in Funcs2)
        {
            Console.SetCursorPosition(funcblock.X, funcblock.Y);
            Console.ForegroundColor = Func2BlockColor;
            Console.Write(Func2Block);
            Console.ResetColor();
        }
        foreach (Pos funcblock in Funcs3)
        {
            Console.SetCursorPosition(funcblock.X, funcblock.Y);
            Console.ForegroundColor = Func3BlockColor;
            Console.Write(Func3Block);
            Console.ResetColor();
        }
        for (int i = 0; i < FunctionalBlocks.Count; i++)
        {
            FunctionalBlocks[i].SelfDraw();
        }

        Console.SetCursorPosition(Chr.X, Chr.Y);
        Console.ForegroundColor = CharcherColor;
        Console.Write(Character);
        Console.ResetColor();
    }

    public static void Start()
    {
        Console.Clear();
        Console.CursorVisible = false;
        Pos Chr = new Pos(Console.WindowWidth / 2, Console.WindowHeight / 2);
        //blocks
        List<Pos> blocks = Blocks;
        List<Pos> func1blocks = Funcs1;
        List<Pos> func2blocks = Funcs2;
        List<Pos> func3blocks = Funcs3;

        Pos Move = new Pos(0, 0);
        Draw(Chr);
        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (Chr.Y > 0)
                        {
                            Move = new Pos(0, -1);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Chr.Y < Console.WindowHeight - 1)
                        {
                            Move = new Pos(0, 1);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (Chr.X > 0)
                        {
                            Move = new Pos(-1, 0);
                        }
                        else
                        {
                            Move = new Pos(0, 0);
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (Chr.X < Console.WindowWidth - 1)
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
                Pos futurepos = new(Chr.X + Move.X, Chr.Y + Move.Y);
                if (blocks.Contains(futurepos)) { }
                else if (func1blocks.Contains(futurepos))
                {
                    Function1();
                    Chr = new Pos(futurepos.X, futurepos.Y);
                }
                else if (func2blocks.Contains(futurepos))
                {
                    Function2();
                    Chr = new Pos(futurepos.X, futurepos.Y);
                }
                else if (func3blocks.Contains(futurepos))
                {
                    Function3();
                    Chr = new Pos(futurepos.X, futurepos.Y);
                }
                else
                {
                    Chr = new Pos(futurepos.X, futurepos.Y);
                }
                for (int i = 0; i < FunctionalBlocks.Count; i++)
                {
                    if (FunctionalBlocks[i].Positions.Contains(futurepos))
                    {
                        FunctionalBlocks[i].Funct();
                        if (!FunctionalBlocks[i].IsWalkable)
                        {
                            Chr = new Pos(futurepos.X, futurepos.Y);
                        }
                    }
                }
                Draw(Chr);
            }
        }
    }
}
