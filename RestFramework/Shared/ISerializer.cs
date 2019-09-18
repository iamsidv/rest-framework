namespace RestAPIModule.Serialize
{
    public interface ISerializer
    {
        T DeserialiseObject<T>(string serialized);
        object DeserialiseObject(string serialized);

        string SerializeObject<T>(T obj);
        string SerializeObject(object obj);
    }
}
