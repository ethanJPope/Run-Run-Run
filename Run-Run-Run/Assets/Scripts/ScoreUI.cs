using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    void Update()
    {
        if (PlayerManager.instance != null)
        {
            scoreText.text = PlayerManager.instance.data.pumpkins.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerManager instance is null. Cannot update score UI.");
        }

    }
}
