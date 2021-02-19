using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followObject;

    [SerializeField] private Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = followObject.position + offSet;
        // var myForward = transform.forward;
        // var followForward = followObject.forward;
        // followForward.y = 0;
        // myForward.y = 0;
        // var angle = Vector3.Angle(myForward, followForward);
        // transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }
}