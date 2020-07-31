using SejDev.Systems.Abilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Camera UICamera;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than 1 GameManager in the scene, destroying this", this);
            Destroy(this);
        }

        Instance = this;
    }

    public void DisablePlayer()
    {
        InputManager.Instance.PlayerInput.Abilities.Disable();
        InputManager.Instance.PlayerInput.Controls.Disable();
        player.DisablePlayer();
    }

    public void EnablePlayer()
    {
        InputManager.Instance.PlayerInput.Abilities.Enable();
        InputManager.Instance.PlayerInput.Controls.Enable();
        player.GetComponent<AbilityHandler>()?.ReloadAbilities();
        player.EnablePlayer();
    }
}