using UnityEngine;

public class OpenShopScript : MonoBehaviour
{
    public ShopManager shopManager;
    private bool isPlayerNearby = false;
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            shopManager.ToggleShop(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            shopManager.ToggleShop(false);
        }
    }
}
