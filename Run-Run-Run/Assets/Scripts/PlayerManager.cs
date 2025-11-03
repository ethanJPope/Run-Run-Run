using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData data = new PlayerData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasItem(string itemName)
    {
        return data.ownedItems.Contains(itemName);
    }

    public void AddItem(string itemName)
    {
        if (!data.ownedItems.Contains(itemName))
        {
            data.ownedItems.Add(itemName);
            SavePlayer();
        }
    }

    public void AddPumkins(int amount)
    {
        data.pumpkins += amount;
        SavePlayer();
    }

    public void SpendPumpkins(int amount)
    {
        data.pumpkins -= amount;
        if (data.pumpkins < 0)
        {
            data.pumpkins = 0;
        }
        SavePlayer();
    }

    public void SavePlayer()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    public void LoadPlayer()
    {
        if(PlayerPrefs.HasKey("PlayerData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            data = JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
