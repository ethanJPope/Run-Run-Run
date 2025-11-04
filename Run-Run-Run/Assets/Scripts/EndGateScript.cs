using UnityEngine;

public class EndGateScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TimerScript.instance.SetInEndGate(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TimerScript.instance.SetInEndGate(false);
        }
    }
}
