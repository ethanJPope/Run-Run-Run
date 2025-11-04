using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] private int pumpkinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.AddPumpkins(pumpkinValue);
            Debug.Log($"Collected {pumpkinValue} pumpkin(s)! Total: {PlayerManager.instance.data.pumpkins}");
        }

        Destroy(gameObject);
    }
}
