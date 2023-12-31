using Newtonsoft.Json;
public static class JsonFile<T>{
    public static List<T>? listOfObjects = new List<T>();
    public static void Read(String FilePath){
        try{
            listOfObjects = null;
            StreamReader reader = new(FilePath);
            string File2Json = reader.ReadToEnd();
            listOfObjects = JsonConvert.DeserializeObject<List<T>>(File2Json)!;
            reader.Close();
        } catch(FileNotFoundException){
            Console.WriteLine("File doesn't exist.");
        }
    }
    public static void Write(String FilePath, T obj){
        try{
            StreamWriter writer = new(FilePath);
            listOfObjects?.Add(obj);
            string List2Json = "";
            if(obj is Account){
                List<Account> SortedAllAccountList = JsonFile<Account>.listOfObjects!.OrderBy(o=>o.username).ToList();
                List2Json = JsonConvert.SerializeObject(SortedAllAccountList, Formatting.Indented);
            } else {
                List2Json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
            }
            writer.Write(List2Json);
            writer.Close();
        } catch(FileNotFoundException){
            Console.WriteLine("File doesn't exist.");
        }
    }
}