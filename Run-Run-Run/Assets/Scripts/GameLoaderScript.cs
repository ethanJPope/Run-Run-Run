using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLoaderScript : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
