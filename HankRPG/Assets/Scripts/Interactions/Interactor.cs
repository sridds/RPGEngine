using UnityEngine;

/// <summary>
/// This is the class that will be attached to the player controller pawn to interact with the enviornment.
/// </summary>
public class Interactor : MonoBehaviour
{
    [SerializeField]
    private float _interactionRayDistance = 5.0f;

    [SerializeField]
    private LayerMask _rayIgnoreLayers;

    private void Update()
    {
        // Attempt to interact
        if (Input.GetKeyDown(KeyCode.Z)) {
            Interact();
        }
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _interactionRayDistance, ~_rayIgnoreLayers);
        if (hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
        {

        }
    }
}
