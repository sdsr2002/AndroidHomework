using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Type/New Armor")]
public class Armor : ItemBase
{
    public Type type;
    public enum Type
    {
        notAssigned,
        Helm,
        Shoulder,
        Chest
    }
}
