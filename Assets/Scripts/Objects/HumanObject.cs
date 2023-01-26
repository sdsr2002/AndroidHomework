using UnityEngine;

[CreateAssetMenu(fileName = "New HumanStats", menuName = "Stats/Race/Human")]
public class HumanObject : RaceObject
{
    private Vector3 _tempNewVelocity;
    RaycastHit[] _hitSingle = new RaycastHit[1];
    public override void Jump(Rigidbody rb)
    {
        if (Physics.RaycastNonAlloc(rb.GetComponentInChildren<Collider>().ClosestPoint(rb.transform.position + Vector3.down * 20f) + Vector3.up * 0.1f, Vector3.down, _hitSingle, 0.25f) != 0)
        {
            rb.velocity = rb.velocity + Vector3.up * Stats.Agility * 1.3f;
        }
    }

    public override void Move(Rigidbody rb, Vector2 moveDelta)
    {
        _tempNewVelocity = rb.velocity + Vector3.right * moveDelta.x * (Time.deltaTime * 10f * (float)Stats.Agility);

        FacingRight = _tempNewVelocity.x < 0;

        rb.velocity = new Vector3(
            Mathf.Clamp(_tempNewVelocity.x, -1f * (float)Stats.Agility, 1f * (float)Stats.Agility),                     // X
            Mathf.Min(_tempNewVelocity.y, 10f * (float)Stats.Agility),                                                  // Y
            0);                                                                                                         // Z
    }

    public override void RacialAbility(Transform Obj)
    {
        Debug.Log("Used Human Ability");
    }

    public override void Updatee(GameObject gameObject)
    {
        base.Updatee(gameObject);
        Debug.Log("Updated Human");
    }
}
