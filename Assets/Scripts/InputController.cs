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

    //Player Controls
    [Header("Player Controls")]
    [SerializeField] private float walkSpeed;

    private const float GRID_SIZE = 1f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
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
    }
    private void Move()
    {
        if (isMoving) return;

        Vector2 input = playerActions.Move.ReadValue<Vector2>();
        Vector2 direction = new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));

        if (direction != Vector2.zero)
        {
            targetPosition = (Vector2)transform.position + direction * GRID_SIZE;
            StartCoroutine(MoveStep());
        }
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
