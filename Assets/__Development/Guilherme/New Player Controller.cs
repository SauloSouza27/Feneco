using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    public bool pause = false;
    public NewControlsTrue controlePlayer;
    private InputAction move, fire1, fire2, jump, dash;
    public float speed = 150;
    public float rotacao = 6;
    public float jumper = 6;
    private Rigidbody rb;
    private void Awake()
    {
        controlePlayer = new NewControlsTrue();
    }
    private void OnEnable()
    {
        move = controlePlayer.Player.Move;
        move.Enable();

        fire1 = controlePlayer.Player.Fire1;
        fire1.Enable();

        fire2 = controlePlayer.Player.Fire2;
        fire2.Enable();

        jump = controlePlayer.Player.Jump;
        jump.Enable();

        dash = controlePlayer.Player.Dash;
        dash.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        fire1.Disable();
        fire2.Disable();
        jump.Disable();
        dash.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(pause == false)
        {
            Movement();
        }
    }
    void Movement()
    {
        Vector3 speedP = Vector3.zero;
        if (move.ReadValue<Vector2>().y > 0)
        {
            speedP = transform.forward * move.ReadValue<Vector2>().y * speed * Time.fixedDeltaTime;
        }
        else
        {
            transform.Rotate(0, move.ReadValue<Vector2>().y * rotacao, 0);
        }
        speedP.y = (rb.velocity.y < 0) ? rb.velocity.y * 1.03f : rb.velocity.y;

        rb.velocity = speedP;

        transform.Rotate(0, move.ReadValue<Vector2>().x * rotacao, 0);
    }
}
