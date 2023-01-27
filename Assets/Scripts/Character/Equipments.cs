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

    public CharacterUnit CharacterUnit;
    Stats stats => CharacterUnit.Race.Stats;
    private void Awake()
    {
        RandomizeEquipment();
        if (CharacterUnit) CharacterUnit.SetData();
    }

    public void RandomizeEquipment()
    {
        List<ItemBase> helms = GameManager.Instance.itemDatabase.GetAllItemsOfType(ItemBase.Type.Helm);
        List<ItemBase> Shoulders = GameManager.Instance.itemDatabase.GetAllItemsOfType(ItemBase.Type.Shoulder);
        List<ItemBase> Chests = GameManager.Instance.itemDatabase.GetAllItemsOfType(ItemBase.Type.Chest);

        int helmChoosen = Random.Range(0, helms.Count);
        int ShoulderChoosen = Random.Range(0, Shoulders.Count);
        int ChestChoosen = Random.Range(0, Chests.Count);

        EquipArmor(helms[helmChoosen] as Armor);
        EquipArmor(Shoulders[ShoulderChoosen] as Armor);
        EquipArmor(Chests[ChestChoosen] as Armor);

        Debug.Log("Equiped " + Helm.name);
        Debug.Log("Equiped " + Shoulder.name);
        Debug.Log("Equiped " + Chest.name);
    }

    public void ReAddModifiersArmor()
    {
        if (!CharacterUnit)
        {
            CharacterUnit = GetComponent<CharacterUnit>();
        }
        if (Helm) Helm.AddModifiers(stats);
        if (Shoulder) Shoulder.AddModifiers(stats);
        if (Chest) Chest.AddModifiers(stats);
    }

    public void RemoveModifiersArmor()
    {
        if (!CharacterUnit)
        {
            CharacterUnit = GetComponent<CharacterUnit>();
        }
        if (Helm) Helm.RemoveModifiers(stats);
        if (Shoulder) Shoulder.RemoveModifiers(stats);
        if (Chest) Chest.RemoveModifiers(stats);
    }

    public void EquipWeapon(ItemBase weapon, Stats stats)
    {
        if (!CharacterUnit)
        {
            CharacterUnit = GetComponent<CharacterUnit>();
        }
        if (RightArm != null) RightArm.RemoveModifiers(stats);
        RightArm = weapon;
        RightArm.AddModifiers(stats);
    }

    public void EquipArmor(Armor armor)
    {
        if (!CharacterUnit)
        {
            CharacterUnit = GetComponent<CharacterUnit>();
        }
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
                if (Chest != null) Chest.RemoveModifiers(stats);
                Chest = armor;
                Chest.AddModifiers(stats);
                break;
        }
    }

    [ContextMenu("SaveTest")]
    public void SaveTesting()
    {
        SaveSystem.SaveArmorEquipment(this, "Testing");
    }

    [ContextMenu("LoadTest")]
    public void LoadTesting()
    {
        Equipments eq = this;
        SaveSystem.LoadArmorEquipments(ref eq, "Testing");
    }

}
