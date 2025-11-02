using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
    public Sprite icon;
    public string itemDescription;


}
