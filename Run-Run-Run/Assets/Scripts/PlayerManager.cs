using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private bool resetPlayer = false;
    public static PlayerManager instance;
    public PlayerData data = new PlayerData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayer();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (resetPlayer)
        {
            ResetPlayer();
            resetPlayer = false;
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

    public void AddPumpkins(int amount)
    {
        data.pumpkins += amount;
        SavePlayer();
    }

    public void SpendPumpkins(int amount)
    {
        data.pumpkins = Mathf.Max(0, data.pumpkins - amount);
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
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            data = JsonUtility.FromJson<PlayerData>(json);

            if (data.ownedItems == null)
                data.ownedItems = new System.Collections.Generic.List<string>();
        }
    }

    public void ResetPlayer()
    {
        data.pumpkins = 0;
        data.ownedItems.Clear();
        data.bestTime = Mathf.Infinity;
        PlayerPrefs.DeleteKey("PlayerData");
        SavePlayer();

        if (TimerScript.instance != null)
            TimerScript.instance.ResetTimerDisplay();
    }

    public void TrySetBestTime(float newTime)
    {
        if (newTime < data.bestTime)
        {
            data.bestTime = newTime;
            SavePlayer();
            Debug.Log($"New best time: {newTime:F2}s");
        }
    }
}
