using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private GameObject mCanvas;
    private GameObject lCanvas;
    private GameObject maincam;
    public GameObject playerobj;
    public MasterControls controls;
    private Animator animator;
    private gamelogic game;

    public float player_speed = 7.0f;
    public float player_focus = 0.2f;
    public float rotationSpeed = 10.0f;
    public float cameraSmoothness = 10.0f;

    private Vector2 movement = Vector2.zero;
    private Vector3 cameraOffset;

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
        maincam = GameObject.Find("Main Camera");
        animator = playerobj.GetComponent<Animator>();
        controls.Player.Enable();

        // Calculate initial camera offset
        cameraOffset = maincam.transform.position - playerobj.transform.position;
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
        Vector3 targetPosition = new Vector3(playerobj.transform.position.x + movement.x * player_speed, playerobj.transform.position.y, playerobj.transform.position.z + movement.y * player_speed);
        playerobj.transform.position = Vector3.Lerp(playerobj.transform.position, targetPosition, Time.deltaTime);

        // Smoothly follow the player with the camera
        Vector3 targetCameraPosition = playerobj.transform.position + cameraOffset;
        maincam.transform.position = Vector3.Lerp(maincam.transform.position, targetCameraPosition, Time.deltaTime * cameraSmoothness);

        // Rotate the player based on movement direction
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
            playerobj.transform.rotation = Quaternion.Slerp(playerobj.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Update animator
        animator.SetFloat("Spd", movement.magnitude);
    }
}
