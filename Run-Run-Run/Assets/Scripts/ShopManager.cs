using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public Transform itemContainer;
    public GameObject itemPrefab;
    public ShopItem[] items;
    public int pumpkins = 0;
    void Start()
    {
        foreach (ShopItem item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            newItem.transform.Find("Name").GetComponent<Text>().text = item.itemName;
            newItem.transform.Find("Price").GetComponent<Text>().text = $"{item.price} 🎃";
            newItem.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            newItem.GetComponent<Button>().onClick.AddListener(() => BuyItem(item));

        }
    }
    void BuyItem(ShopItem item)
    {
        if (pumpkins >= item.price)
        {
            pumpkins -= item.price;
            Debug.Log($"Bought {item.itemName}");
        }
    }
    public void ToggleShop(bool isOpen)
    {
        shopUI.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }
}
