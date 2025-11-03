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
        if (PlayerManager.instance.HasItem(item.itemName))
        {
            Debug.Log($"{item.itemName} already purchased!");
            return;
        }

        if (PlayerManager.instance.data.pumpkins >= item.price)
        {
            PlayerManager.instance.SpendPumpkins(item.price);
            PlayerManager.instance.AddItem(item.itemName);
            Debug.Log($"Purchased {item.itemName} for {item.price}");
        }
        else
        {
            Debug.Log("Not enough pumpkins!");
        }
    }
    public void ToggleShop(bool isOpen)
    {
        shopUI.SetActive(isOpen);
        Time.timeScale = isOpen ? 0f : 1f;
    }
}
