using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;
    [SerializeField] private Text timerText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private float time = 0;

    float bestTime = 0;
    public bool inStartGate = true;
    public bool inEndGate = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (PlayerManager.instance != null)
        {
            bestTime = PlayerManager.instance.data.bestTime;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            time = 0;
            inStartGate = true;
            inEndGate = false;
        }
        if (PlayerManager.instance != null)
        {
            bestTime = PlayerManager.instance.data.bestTime;
        }
        if (!inStartGate && !inEndGate)
        {
            time += Time.deltaTime;
        }
        if (bestTime != Mathf.Infinity)
        {
            string bestFormatted = string.Format("{0:00}:{1:00}", (int)(bestTime / 60), (int)(bestTime % 60));
            bestTimeText.text = "BestTime: " + bestFormatted;
        }
        int totalSeconds = Mathf.FloorToInt(time);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetTimerDisplay()
    {
        time = 0f;
        inStartGate = true;
        inEndGate = false;

        if (timerText != null)
            timerText.text = "00:00";

        if (bestTimeText != null)
            bestTimeText.text = "Best Time: --:--";

        Debug.Log("Timer reset by PlayerManager.");
    }

    public void SetInStartGate(bool inGate)
    {
        inStartGate = inGate;
    }

    public void SetInEndGate(bool inGate)
    {
        inEndGate = inGate;
        if (inEndGate)
        {
            PlayerManager.instance.TrySetBestTime(time);
        }
    }
}
