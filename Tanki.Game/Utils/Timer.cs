namespace Tanki.Game.Utils
{
    public class GameTimer
    {
        public DateTime Prev { get; private set; }

        public float Time { get; }

        public bool IsTimeOut => (DateTime.Now - Prev).TotalSeconds > Time;

        public GameTimer(float seconds)
        {
            Time = seconds;
            Prev = DateTime.Now;
        }

        public void Reset()
        {
            Prev = DateTime.Now;
        }
    }
}
