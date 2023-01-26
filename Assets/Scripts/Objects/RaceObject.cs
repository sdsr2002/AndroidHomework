using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public abstract class RaceObject : ScriptableObject
{
    [Header("Stats")]
    public Stats Stats = new Stats();
    protected bool FacingRight = false;
    [Space]
    [Header("Colliders and Visuals")]
    public GameObject Prefab;
    public Vector3 LocalPosition = Vector3.zero;

    private Vector3 eulars;
    private float currentTurnVelocity;
    public float JumpCost => Mathf.Max(1f, 10f - (2f * Stats.Agility));

    public abstract void Move(Rigidbody rb, Vector2 deltaMovement);
    public abstract void Jump(Rigidbody rb);
    public abstract void RacialAbility(Transform Obj);
    public virtual void Rotate(Transform transform)
    {
        eulars = transform.root.eulerAngles;
        transform.rotation = FacingRight ? Quaternion.Euler(eulars.x, Mathf.SmoothDamp(eulars.y, 270f, ref currentTurnVelocity, 0.1f), eulars.z) : Quaternion.Euler(eulars.x, Mathf.SmoothDamp(eulars.y, 90f, ref currentTurnVelocity, 0.1f), eulars.z);
    }

    public virtual void Updatee(GameObject gameObject)
    {
        Rotate(gameObject.transform);
    }

    public virtual void OnEnable()
    {
        Stats.Clear();
    }
}
