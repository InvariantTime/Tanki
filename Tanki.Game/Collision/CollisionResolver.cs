using Tanki.Game.Objects;

namespace Tanki.Game.Collision
{
    public class CollisionResolver
    {
        public void Resolve(World world, IReadOnlyCollection<GameObject> objects)
        {
            foreach (var obj in objects)
            {
                if (obj.Collider.CollideWith(world) == true)
                    obj.OnCollide(world);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                var obj1 = objects.ElementAt(i);

                for (int j = i + 1; j < objects.Count; j++)
                {
                    var obj2 = objects.ElementAt(j);

                    if (obj1.Collider.CollideWith(obj2.Collider) == true)
                    {
                        obj1.OnCollide(obj2);
                        obj2.OnCollide(obj1);
                    }
                }
            }
        }
    }
}
