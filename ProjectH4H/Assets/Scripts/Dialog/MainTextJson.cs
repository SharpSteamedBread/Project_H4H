using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTextJson : MonoBehaviour
{
    
}

[System.Serializable]
public class DialogMidboss
{
    public int dialogNumber;
    public string charName;
    public string charDialog;
}

[System.Serializable]
public class DialogList
{
    public List<DialogMidboss> dialogMidboss;
}

