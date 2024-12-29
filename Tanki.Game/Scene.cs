using System.Collections.ObjectModel;
using Tanki.Game.Objects;

namespace Tanki.Game
{
    public class Scene
    {
        private readonly List<GameObject> _objects;
        private readonly Queue<GameObject> _temps;

        public IReadOnlyCollection<GameObject> Objects { get; }

        public World World { get; }

        public Scene(World world)
        {
            _objects = new List<GameObject>();
            _temps = new Queue<GameObject>();

            Objects = new ReadOnlyCollection<GameObject>(_objects);
            World = world;
        }

        public void UpdateState()
        {
            _objects.AddRange(_temps);
            _temps.Clear();

            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].IsDestroyed == true)
                {
                    _objects.RemoveAt(i);
                    i--;
                }
            }
        }

        public void AddObject(GameObject @object)
        {
            _temps.Enqueue(@object);
        }
    }
}