using SejDev.Player;
using SejDev.Systems.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIScreen playerGameScreen;

    public void DisablePlayer()
    {
        mouseLook.enabled = false;
        playerMovement.enabled = false;
        playerGameScreen.Hide();
    }

    public void EnablePlayer()
    {
        mouseLook.enabled = true;
        playerMovement.enabled = true;
        playerGameScreen.Show();
    }
}