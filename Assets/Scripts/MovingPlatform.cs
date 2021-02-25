using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 2f;
    private Vector3 startPosition;
    private Vector3 currentDestination;
    private bool movePlayer;
    private CharacterController characterController;

    private bool isReturning;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentDestination = destination.position;
        moveDirection = (currentDestination - startPosition).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(currentDestination, transform.position) < 0.1f)
        {
            moveDirection *= -1;
            isReturning = !isReturning;

            currentDestination = isReturning ? startPosition : destination.position;
        }
    }

    private void FixedUpdate()
    {
        if (movePlayer && characterController != null)
        {
            characterController.Move(moveDirection * (speed * Time.fixedDeltaTime));
        }

        transform.position += moveDirection * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterController = other.GetComponent<CharacterController>();
            movePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterController = null;
            movePlayer = false;
        }
    }
}