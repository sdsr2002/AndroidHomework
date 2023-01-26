using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Equipments))]
public class CharacterUnit : MonoBehaviour
{
    [Header("Run Time")]
    [Space]
    public float Health;
    public float Stamina;
    private GameObject _Collider;
    private Rigidbody _rb;
    [Header("Data")]
    [Space]
    public RaceObject Race;

    [HideInInspector] public Equipments Equipments;

    // Propertys
    public float MaxHealth => 10f * Race.Stats.Constitution;
    public float MaxStamina => 2.5f * Race.Stats.Agility;

    #region Race
    public void Move(Rigidbody rb, Vector2 deltaMovement)
    {
        Race.Move(rb, deltaMovement);
    }
    public void Jump(Rigidbody rb)
    {
        if (Stamina >= Race.JumpCost)
        {
            Stamina -= Race.JumpCost;
            Race.Jump(rb);
        }
    }

    public void RacialAbility(Transform Obj)
    {
        Race.RacialAbility(Obj);
    }

    public void UpdateRaceObject(GameObject gameObject)
    {
        Race.Updatee(gameObject);
    }

    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Equipments = GetComponent<Equipments>();
        SetRaceData(Race);
    }

    private void Update()
    {
        HealthRegen();
        StaminaRegen();
    }

    [ContextMenu("ResetData")]
    public void SetRaceData()
    {
        SetRaceData(Race);
    }

    public void SetRaceData(RaceObject data)
    {
        _rb.useGravity = false;
        Equipments.RemoveModifiersArmor();
        Race = data;
        Equipments.ReAddModifiersArmor();
        if (_Collider) Destroy(_Collider);

        _Collider = Instantiate(Race.Prefab,
            Race.LocalPosition,
            transform.rotation,
            transform
            );

        _Collider.transform.localPosition = Race.LocalPosition;
        SetData();
        _rb.useGravity = true;
    }

    internal void SetData()
    {
        Health = MaxHealth;
        Stamina = MaxStamina;
    }

    public void HealthRegen()
    {
        float HealthRegened = Time.deltaTime * Race.Stats.Constitution * 0.25f;
        if (Health + HealthRegened < MaxHealth)
            Health += HealthRegened;
        else
        {
            Health = MaxHealth;
        }
    }

    public void StaminaRegen()
    {
        float StaminaRegened = Time.deltaTime * Race.Stats.Agility * 0.25f;
        if (Stamina + StaminaRegened < MaxStamina)
            Stamina += StaminaRegened;
        else
        {
            Stamina = MaxStamina;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= Mathf.Max(0f,damage * Mathf.Pow(0.99f, Race.Stats.Constitution));
    }

}
