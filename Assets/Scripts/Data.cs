using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private static bool soundEnabled = true;
    public static bool SoundEnabled
    {
        get{return soundEnabled;}
        set{soundEnabled = value;}
    }
}
