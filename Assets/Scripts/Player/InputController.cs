using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class InputController : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerControls.PlayerActions playerActions;

    private bool isMoving;
    //a getter it lets you get the isMoveing Value without changing it by acident
    public bool IsMoving { get { return isMoving; } }

    //on the gameobject
    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private Animator animator;

    //Player Controls
    [Header("Player Controls")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] public LayerMask obstacleLayer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateSpriteDirection();
    }

    private Vector2 targetPosition;
    private void UpdateSpriteDirection()
    {
        Vector2 input = playerActions.Move.ReadValue<Vector2>();
        
        if (input.x > 0)
            playerRenderer.flipX = false;
        else if (input.x < 0)
            playerRenderer.flipX = true;
        if (input.y > 0)
            animator.SetBool("Forward",false);
        else if (input.y < 0)
            animator.SetBool("Forward", true);

        animator.SetBool("IsMoving", IsMoving);
    }
    private void Move()
    {
        if (isMoving) return;

        Vector2 input = playerActions.Move.ReadValue<Vector2>();
        Vector2 direction = Vector2.zero;

        // Prioritize horizontal movement
        if (Mathf.Abs(input.x) > 0)
        {
            direction.x = Mathf.Sign(input.x);
        }
        // If no horizontal input, check for vertical
        else if (Mathf.Abs(input.y) > 0)
        {
            direction.y = Mathf.Sign(input.y);
        }

        if (CanMove(direction))
        {
            targetPosition = (Vector2)transform.position + direction * gridSize;
            StartCoroutine(MoveStep());
        }
    }

    private bool CanMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gridSize, obstacleLayer);
        return hit.collider == null;
    }

    private IEnumerator MoveStep()
    {
        isMoving = true;
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
