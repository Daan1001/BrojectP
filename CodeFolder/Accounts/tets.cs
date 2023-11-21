class Playlist
{
    private string[] _songs;
    public Playlist(int size) => _songs = new string[size];

    public int Count { get => _songs.Length; }

    public string this[int index]{get => _songs[index]; set => _songs[index] = value;}

    public static Playlist operator +(Playlist one, Playlist two){
        String[] newList = new String[one._songs.Length+two._songs.Length];
        for(int i = 0; i< one._songs.Length; i++){
            newList[i] = one[i];
        }
        for(int i = one._songs.Length; i < (two._songs.Length+one._songs.Length); i++){
            newList[i] = two[i-one._songs.Length];
        }
        Playlist PL = new Playlist(newList.Length);
        for(int i = 0; i < newList.Length; i++){
            PL[i] = newList[i];
        }
        return PL;
    } 
}
