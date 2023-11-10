using UnityEngine; //Para la clase JsonUtility
using System.Net;
using System.IO;

public class APIHelper
{
    /// <summary>
    /// Llamada get a API de jokes de Chuck Norris
    /// Para hacer post: https://stackoverflow.com/questions/39246236/how-can-i-post-data-using-httpwebrequest
    /// </summary>
    /// <returns></returns>
    public static Joke GetNewJoke()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://api.chucknorris.io/jokes/random");

        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<Joke>(json);
    }
 
}
