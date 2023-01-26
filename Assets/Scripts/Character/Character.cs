using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput),typeof(Rigidbody), typeof(CharacterUnit))]
public class Character : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction => _playerInput.actions["Move"];
    private InputAction _jumpAction => _playerInput.actions["Jump"];

    private Rigidbody _rb;

    private CharacterUnit _characterUnit;

    private void Awake()
    {
        // CharacterUnit
        if (!_characterUnit)
        {
            if (!TryGetComponent<CharacterUnit>(out _characterUnit))
            {
                Debug.LogWarning($"No CharacterUnit on: '{name}'");
            }
        }

        // Rigidbody
        if (!_rb)
        { 
            if(!TryGetComponent<Rigidbody>(out _rb))
            {
                Debug.LogWarning($"No Rigidbody on: '{name}'");
            }
        }

        // PlayerInputs
        _playerInput = GetComponent<PlayerInput>();
        _jumpAction.performed += ctx => Jump();
        _moveAction.started += ctx => StartCoroutine("MoveContinuesly");
        _moveAction.canceled += ctx => StopCoroutine("MoveContinuesly");
    }

    private void Update()
    {
        _characterUnit.UpdateRaceObject(gameObject);
    }

    private IEnumerator MoveContinuesly()
    {
        while (true)
        {
            Move(_moveAction.ReadValue<Vector2>());
            yield return new WaitForEndOfFrame();
        }
    }

    private void Jump()
    {
        _characterUnit.Race.Jump(_rb);
    }

    private void Move(Vector2 moveDelta)
    {
        if (!_rb) return;

        _characterUnit.Race.Move(_rb, moveDelta);
    }

    public void CharacterJump()
    {
        Jump();
    }

    private void OnDrawGizmos()
    {
        Collider _tempCol = GetComponentInChildren<Collider>();
        if (_tempCol) Gizmos.DrawRay(_tempCol.ClosestPoint(transform.position + Vector3.down * 20f) + Vector3.up * 0.1f, Vector3.down);
    }
}
