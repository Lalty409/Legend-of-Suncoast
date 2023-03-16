using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> Items = new List<Item>();
    public int numberOfKeys;

    void Start() {
        numberOfKeys = 0;
    }
    public void addItem(Item itemToAdd) {
        if(itemToAdd.isKey) {
            numberOfKeys++;

        }
        else {
            //video part 33 26:10
        }
    }

}
