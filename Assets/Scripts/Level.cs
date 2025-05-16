using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Levels
{
    [Range(1, 11)]
    public int platformPartCount = 11;

    [Range(0, 11)]
    public int deadAreaPartCount = 1;
}

[CreateAssetMenu(fileName = "Levels") ]
public class Level : ScriptableObject
{
    public List<Levels> levels = new List<Levels>();
}
