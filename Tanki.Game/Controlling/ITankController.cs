namespace Tanki.Game.Controlling
{
    public interface ITankController
    {
        TankCommands Update(World world, TankInfo info);

        void AddTankEvent(TankEvents @event);
    }
}
