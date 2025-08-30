using System.Collections;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;       // Movement speed
    public float moveThreshold = 0.1f; // Minimum distance to consider moving
    public float autoMoveInterval = 3f; // Time between automatic target selections
    public float idleChance = 0.3f;    // Chance to stay idle instead of moving

    private Vector3 targetPosition;
    private bool isMoving = false;

    private bool manualMovementEnabled = false;
    private bool autoMovementEnabled = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;

        if (autoMovementEnabled)
            StartCoroutine(AutoMoveRoutine());
    }

    void Update()
    {
        if (manualMovementEnabled)
            CheckMouseClickRaycast();

        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (targetPosition.x < transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            if (Vector3.Distance(transform.position, targetPosition) <= moveThreshold)
                isMoving = false;
        }
    }

    void HandleAnimation()
    {
        if (isMoving)
            animator.Play("jump");
        else
            animator.Play("idle");
    }

    void CheckMouseClickRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TrySetTarget(mouseWorldPos);
        }
    }

    void TrySetTarget(Vector2 position)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(position);

        if (hitCollider == null)
        {
            Debug.Log("No collider at position.");
            return;
        }
        if (Shop.instance.isMenuOpen)
        {
            Debug.Log("Shop menu is open, character will not move");
            return;
        }
        if (hitCollider.CompareTag("Obstacle"))
        {
            Debug.Log("Hit an obstacle!");
            return;
        }
        if (hitCollider is PolygonCollider2D)
        {
            Debug.Log("Valid target: " + hitCollider.name);
            targetPosition = new Vector3(position.x, position.y, transform.position.z);
            isMoving = true;
        }
    }

    // --- Automatic Movement ---
    IEnumerator AutoMoveRoutine()
    {
        while (true)
        {
            if (autoMovementEnabled)
            {
                // Decide if we stay idle this interval
                if (Random.value > idleChance)
                {
                    // Pick a random point around the character within radius 1
                    Vector2 pos = transform.position;
                    Vector2 randomOffset = Random.insideUnitCircle;
                    Vector2 candidatePosition = pos + randomOffset;
                    
                    // Try set as target (only if PolygonCollider2D and valid)
                    TrySetTarget(candidatePosition);
                }
                else
                {
                    Debug.Log("Auto-movement idle this interval");
                }
            }

            yield return new WaitForSeconds(autoMoveInterval);
        }
    }


    // --- Button functions ---
    public void EnableManualMovement()
    {
        manualMovementEnabled = true;
        autoMovementEnabled = false;
        Debug.Log("Manual movement enabled, automatic movement disabled");
    }

    public void EnableAutomaticMovement()
    {
        manualMovementEnabled = false;
        autoMovementEnabled = true;
        Debug.Log("Automatic movement enabled, manual movement disabled");
    }

    public void DeleteCharacter()
    {
        StopAllCoroutines();
        Destroy(gameObject);
        
    }
}
