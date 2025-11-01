using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] private GameObject pointController;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointScript.instance.AddPoints(1); 
            Destroy(gameObject);
        }
    }
}
