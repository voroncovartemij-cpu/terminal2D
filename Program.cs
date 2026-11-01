static void ChangeColor(TwoDism.Character character)
{
    if (character.CharacterColor == ConsoleColor.Green)
    {
        character.CharacterColor = ConsoleColor.White;
    }
    else
    {
        character.CharacterColor = ConsoleColor.Green;
    }
}
static void Teleportation(TwoDism.Character character)
{
    character.CharacterPos = new(Console.WindowWidth / 2, Console.WindowHeight / 2);
}

TwoDism.AddBlock(
    new(
        '#',
        ConsoleColor.Red,
        [new TwoDism.Pos(10, 10)],
        TwoDism.Nothing,
        false,
        false
    )
);
TwoDism.AddBlock(
    new(
        '$',
        ConsoleColor.Green,
        [new TwoDism.Pos(20, 10)],
        ChangeColor,
        true,
        true
    )
);
TwoDism.AddBlock(
    new(
        '%',
        ConsoleColor.Yellow,
        [new TwoDism.Pos(30, 10)],
        ChangeColor,
        true,
        false
    )
);
TwoDism.AddBlock(
    new(
        '~',
        ConsoleColor.Blue,
        [new TwoDism.Pos(40, 10)],
        Teleportation,
        true,
        false
    )
);
TwoDism.AddBlock(
    new(
        '0',
        ConsoleColor.Cyan,
        [new TwoDism.Pos(50, 10)],
        ChangeColor,
        false,
        false
    )
);
TwoDism.Start();
TwoDism.ClearAll();
TwoDism.Start();
