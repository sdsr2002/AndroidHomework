using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Container> items = new List<Container>();

    [System.Serializable]
    public class Container
    {
        [HideInInspector]public string name;
        public int id;
        public ItemBase _object;
    }

    public List<ItemBase> GetAllItemsOfType(ItemBase.Type type)
    {
        List<ItemBase> returnValue = new List<ItemBase>();
        for(int i = 0; i < this.items.Count; i++)
        {
            if(this.items[i]._object.type == type) returnValue.Add(this.items[i]._object);
        }
        return returnValue;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i].id = i;
            items[i]._object.SetID(i);
            items[i].name = items[i]._object.Name;
        }
    }
#endif
    public ItemBase GetItemFromID(int v)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i]._object.ID == v) return items[i]._object;
        }
        Debug.LogError("Did not find item with ID:" + v);
        return null;
    }
}
