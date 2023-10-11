using Newtonsoft.Json;

public static class JsonFile<T>{
    public static List<T>? listOfObjects;
    public static void Read(String FilePath){
        listOfObjects = null;
        StreamReader reader = new(FilePath);
        string File2Json = reader.ReadToEnd();
        listOfObjects = JsonConvert.DeserializeObject<List<T>>(File2Json)!;
        reader.Close();
    }
    public static void Write(String FilePath, T obj){
        StreamWriter writer = new(FilePath);
        listOfObjects.Add(obj);
        string List2Json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
        writer.Write(List2Json);
        writer.Close();
    }
}