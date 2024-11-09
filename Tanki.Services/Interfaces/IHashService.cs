namespace Tanki.Services.Interfaces
{
    public interface IHashService
    {
        string CreateHash(object @object);

        bool Compare(string hash, object @object);
    }
}
