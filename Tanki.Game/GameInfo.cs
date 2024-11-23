namespace Tanki.Game
{
    public readonly ref struct GameInfo
    {
        public GameScene Scene { get; init; }

        public float DeltaTime { get; init; }
    }
}
