using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System Seriazable para manejo como entidad
[System.Serializable]
public class Joke
{
    public string[] categories;
    public string created_at;
    public string icon_url;
    public string id;
    public string updated_at;
    public string url;
    public string value;
}
