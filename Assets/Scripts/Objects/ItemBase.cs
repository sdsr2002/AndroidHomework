using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Type/New ItemBase")]
public class ItemBase : ScriptableObject
{
    public int ID = -1;
    [SerializeField] public string Name;
    public Type type;
    [SerializeField] protected List<Stat.Modifier> modifiers;

#if UNITY_EDITOR
    public void SetID(int newID)
    {
        ID = newID;
    }
#endif
    public virtual void AddModifiers(Stats stats)
    {
        foreach(Stat.Modifier mod in modifiers)
        {
            switch (mod.statType)
            {
                case Stat.Type.Strength:
                    stats._Strength.AddModifier(mod);
                    break;
                case Stat.Type.Agility:
                    stats._Agility.AddModifier(mod);
                    break;
                case Stat.Type.Constitution:
                    stats._Constitution.AddModifier(mod);
                    break;
            }
        }
    }

    public virtual void RemoveModifiers(Stats stats)
    {
        foreach (Stat.Modifier mod in modifiers)
        {
            switch (mod.statType)
            {
                case Stat.Type.Strength:
                    stats._Strength.RemoveModifier(mod);
                    break;
                case Stat.Type.Agility:
                    stats._Agility.RemoveModifier(mod);
                    break;
                case Stat.Type.Constitution:
                    stats._Constitution.RemoveModifier(mod);
                    break;
            }
        }
    }

    public enum Type
    {
        notAssigned,
        Helm,
        Shoulder,
        Chest
    }
}
