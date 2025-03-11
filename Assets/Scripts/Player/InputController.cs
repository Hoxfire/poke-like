using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    private PlayerControls playerControls;
    private PlayerControls.PlayerActions playerActions;
    private PlayerControls.MenuActions menuActions;

    private bool isMoving;
    //a getter it lets you get the isMoveing Value without changing it by acident
    public bool IsMoving { get { return isMoving; } }

    //on the gameobject
    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private Animator animator;

    //Player Controls
    [Header("Player Controls")]
    [SerializeField] public float walkSpeed;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] public LayerMask obstacleLayer;

    [Header("Menu Controls")]
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private InventoryManager inventoryManager;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        playerControls = new PlayerControls();
        playerActions = playerControls.Player;
        menuActions = playerControls.Menu;

        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateSpriteDirection();
        Menu();
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
            //Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
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
            //Camera.main.transform.DOShakeRotation(0.05f,5,2);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private bool wasUIPressedLastFrame = false;

    private void Menu()
    {
        //This is to open the menu and check if the button was pressed inthe last frame
        bool isMenuPressed = menuActions.Open.IsPressed();

        if (isMenuPressed && !wasUIPressedLastFrame)
        {
            menuManager.ToggleMenu();
            Debug.Log("Menu");
            if (!menuManager.menuPanle.activeSelf)
            {
                playerActions.Enable();
            }
            else
            {
                playerActions.Disable();
            }
        }

        //moves the currsor in the first menu up and down
        bool upAndDown = menuActions.Move.IsPressed();

        if (upAndDown && !wasUIPressedLastFrame)
        {
            Vector2 pos = menuActions.Move.ReadValue<Vector2>();

            menuManager.menuUI.UpdateCursor(pos, menuManager.menuState);
        }

        wasUIPressedLastFrame = isMenuPressed;
    }

    public IEnumerator sceneTransition(string newScene, DoorEventChannel doorEventChannel, Vector2 newPos)
    {
        playerControls.Disable();
        yield return new WaitForSeconds(1);
        playerControls.Enable();
        doorEventChannel.RaiseDoorEnteredEvent(newScene);
        transform.position = newPos;
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
