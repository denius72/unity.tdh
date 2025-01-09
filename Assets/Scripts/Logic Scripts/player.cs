using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private GameObject mCanvas;
    private GameObject lCanvas;
    public GameObject playerobj;
    public MasterControls controls;
    private Animator animator;
    private gamelogic game;

    public float player_speed = 7.0f;
    public float player_focus = 0.2f;
    public float rotationSpeed = 10.0f;
    public float cameraSmoothness = 10.0f;

    private Vector2 movement = Vector2.zero;

    void Awake()
    {
        controls = new MasterControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        player_speed = 5.0f;
        lCanvas = GameObject.Find("Canvas_Loading");
        game = lCanvas.GetComponent<gamelogic>();
        mCanvas = GameObject.Find("Canvas");
        animator = playerobj.GetComponent<Animator>();
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Focus.performed += OnFocusPerformed;
        controls.Player.Focus.canceled += OnFocusCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Focus.performed -= OnFocusPerformed;
        controls.Player.Focus.canceled -= OnFocusCanceled;
    }

    private void OnFocusPerformed(InputAction.CallbackContext context)
    {
        player_speed = 10.0f;
    }

    private void OnFocusCanceled(InputAction.CallbackContext context)
    {
        player_speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Read movement input
        movement = controls.Player.Movement.ReadValue<Vector2>();

        // Move the player
        Vector3 targetPosition = new Vector3(playerobj.transform.position.x + movement.x * player_speed, 
                                             playerobj.transform.position.y, 
                                             playerobj.transform.position.z + movement.y * player_speed);

        // Check if the game is in orthographic exploration mode
        if (game.gamestate == gamelogic.GameState.EXPLORATION_MODE_ORTOGRAPHIC)
        {
            // For isometric view, we need to apply a 45 degree rotation offset to the movement direction
            Vector3 isometricMovement = new Vector3(movement.x - movement.y, 0, movement.x + movement.y);
            targetPosition = playerobj.transform.position + isometricMovement * player_speed;
        }

        playerobj.transform.position = Vector3.Lerp(playerobj.transform.position, targetPosition, Time.deltaTime);

        // Rotate the player based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Apply 45 degree offset if in orthographic exploration mode
            if (game.gamestate == gamelogic.GameState.EXPLORATION_MODE_ORTOGRAPHIC)
            {
                angle += 45.0f;
            }

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
            playerobj.transform.rotation = Quaternion.Slerp(playerobj.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Update animator
        animator.SetFloat("Spd", movement.magnitude);
    }
}
