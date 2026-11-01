static void CharacterColorChange()
{
    D2.Chr.CharacterColor = ConsoleColor.Yellow;
}
D2.FunctionalBlocks.Add(
    new(
        5,
        '0',
        ConsoleColor.Cyan,
        [new D2.Pos(60, 10)],
        CharacterColorChange,
        true,
        true
    )
);
D2.Start();
