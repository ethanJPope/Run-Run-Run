using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int pumpkins;
    public List<string> ownedItems;
    public float bestTime = Mathf.Infinity;

}
