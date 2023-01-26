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
        public int id;
        public ItemBase _object;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i].id = i;
            items[i]._object.SetID(i);
        }
    }
#endif
}
