using UnityEngine;

public class PumpkinScript : MonoBehaviour
{
    [SerializeField] private GameObject pointController;
    private PointScript pointScript;
    void Start()
    {

        pointScript = pointController.GetComponent<PointScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pointScript.AddPoints(1);
            Destroy(gameObject);
        }
    }
}
