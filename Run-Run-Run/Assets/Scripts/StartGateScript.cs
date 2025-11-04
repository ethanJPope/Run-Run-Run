using UnityEngine;

public class StartGateScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TimerScript.instance.SetInStartGate(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TimerScript.instance.SetInStartGate(false);
        }
    }
}
