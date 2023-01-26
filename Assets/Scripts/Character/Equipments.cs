using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [Header("Weapon")]
    public ItemBase RightArm;

    [Header("Armor")]
    public Armor Helm;
    public Armor Shoulder;
    public Armor Chest;

#if UNITY_EDITOR
    [Header("Debug - Only in editor")]
    public CharacterUnit CharacterUnit;
    private void Start()
    {
        if (CharacterUnit)
        {
            ReAddModifiersArmor(CharacterUnit.Race.Stats);
        }
    }
#endif
    public void ReAddModifiersArmor(Stats stats)
    {
        Helm.AddModifiers(stats);
        Shoulder.AddModifiers(stats);
        Chest.AddModifiers(stats);
    }

    public void EquipWeapon(ItemBase weapon, Stats stats)
    {
        if (RightArm != null) RightArm.RemoveModifiers(stats);
        RightArm = weapon;
        RightArm.AddModifiers(stats);
    }

    public void EquipArmor(Armor armor, Stats stats)
    {
        switch (armor.type)
        {
            case Armor.Type.Helm:
                if (Helm != null) Helm.RemoveModifiers(stats);
                Helm = armor;
                Helm.AddModifiers(stats);
                break;
            case Armor.Type.Shoulder:
                if (Shoulder != null) Shoulder.RemoveModifiers(stats);
                Shoulder = armor;
                Shoulder.AddModifiers(stats);
                break;
            case Armor.Type.Chest:
                if (Helm != null) Chest.RemoveModifiers(stats);
                Chest = armor;
                Chest.AddModifiers(stats);
                break;
        }
    }

}
