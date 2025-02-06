using System.Numerics;

namespace Tanki.Game.Visualization
{
    public class VisualComposition
    {
        public string Object { get; set; } = string.Empty;

        public Dictionary<string, object> Values { get; }

        public VisualComposition()
        {
            Values = new Dictionary<string, object>();
        }

        public void InsertObject(string name, object value)
        {
            Values.Add(name, value);
        }

        public T? GetObject<T>(string name)
        {
            Values.TryGetValue(name, out var value);

            return (T?)value;
        }

        public void Free()
        {
            Values.Clear();
        }
    }
}
