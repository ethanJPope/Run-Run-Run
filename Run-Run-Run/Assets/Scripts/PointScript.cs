using UnityEngine;

public class PointScript : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Points", 0);
    }
    public void AddPoints(int points)
    {
        int currentPoints = PlayerPrefs.GetInt("Points");
        currentPoints += points;
        PlayerPrefs.SetInt("Points", currentPoints);
    }

    public int GetPoints()
    {
        return PlayerPrefs.GetInt("Points");
    }
}
