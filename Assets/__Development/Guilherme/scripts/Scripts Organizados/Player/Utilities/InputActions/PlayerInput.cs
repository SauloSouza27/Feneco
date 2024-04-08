using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public NewControlsTrue InputActions { get; private set; }
    public NewControlsTrue.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new NewControlsTrue();

        PlayerActions = InputActions.Player;
    }
    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }
}
