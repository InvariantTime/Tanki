namespace Tanki.Game.Controlling
{
    public readonly ref struct TankCommands
    {
        public bool Shoot { get; init; }

        public bool Move { get; init; }

        public float Rotation { get; init; }
    }
}
