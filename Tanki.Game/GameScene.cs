using System.Collections.ObjectModel;
using Tanki.Game.Objects;

namespace Tanki.Game
{
    public class GameScene
    {
        private readonly Queue<GameObject> _tempObjects;
        private readonly List<GameObject> _objects;

        public World World { get; }

        public IReadOnlyCollection<GameObject> Objects { get; }

        public GameScene(World world)
        {
            _objects = new();
            _tempObjects = new();

            World = world;
            Objects = new ReadOnlyCollection<GameObject>(_objects);
        }

        public void Instantiate(GameObject obj)
        {
            _tempObjects.Enqueue(obj);
        }

        public void UpdateState()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                var @object = _objects[i];

                if (@object.NeedDestroy == true)
                {
                    _objects.Remove(@object);
                    i--;
                }
            }

            _objects.AddRange(_tempObjects);
            _tempObjects.Clear();
        }
    }
}
