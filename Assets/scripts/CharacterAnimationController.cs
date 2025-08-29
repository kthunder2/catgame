using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;       // Movement speed
    public float moveThreshold = 0.1f; // Minimum distance to consider moving

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position; // Start at current position
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
        CheckMouseClickRaycast(); // Check for PolygonCollider2D at mouse click
    }

    void HandleMovement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (targetPosition.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1); // Face left
            else
                transform.localScale = new Vector3(1, 1, 1);  // Face right

            if (Vector3.Distance(transform.position, targetPosition) <= moveThreshold)
                isMoving = false;
        }
    }

    void HandleAnimation()
    {
        if (isMoving)
            animator.Play("jump"); // Replace with your moving animation
        else
            animator.Play("idle");
    }

    void CheckMouseClickRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if there is a PolygonCollider2D at the mouse position
            Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPos);

            if (hitCollider == null)
            {
                Debug.Log("Mouse click did not hit any collider, character will not move");
            }
            else if (hitCollider.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("It's an obstacle!");
            }
            else
            {
                if (hitCollider != null && hitCollider is PolygonCollider2D)
                {
                    Debug.Log("Mouse clicked on a 2D PolygonCollider2D: " + hitCollider.name);

                    // Only move if a polygon collider was hit
                    targetPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
                    isMoving = true;
                }
                else
                {
                    Debug.Log("Mouse click did not hit a PolygonCollider2D, character will not move");
                }
            }

            
        }
    }
}
