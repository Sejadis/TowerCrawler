using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SejDev.Systems.Abilities
{
    public class GroundTargeting : IAbilityTargeter
    {
        private readonly Camera cam;
        private readonly float range;
        private readonly MonoBehaviour monoBehaviour;
        private readonly float targetRadius;
        private readonly LayerMask layerMask;
        private readonly Vector3 screenCenter;

        private Vector3 newPos = new Vector3(0, 5, 0);
        private Projector projector;
        private GameObject targetObject;
        public bool IsTargeting { get; private set; }
        public bool RequiresSeperateTargeting => true;

        // // Start is called before the first frame update
        // void Start()
        // {
        //     StartTargeting(2, 1 << LayerMask.NameToLayer("Ground"));
        // }
        //
        // // Update is called once per frame
        // void Update()
        // {
        //
        //     if (Input.GetKeyDown(KeyCode.O))
        //     {
        //         Debug.Log(GetTarget());
        //     }
        // }

        public GroundTargeting(Camera cam, float range, float targetRadius, LayerMask layerMask,
            MonoBehaviour monoBehaviour)
        {
            this.cam = cam;
            this.range = range;
            this.monoBehaviour = monoBehaviour;
            this.targetRadius = targetRadius;
            this.layerMask = layerMask;
            screenCenter = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
        }

        public void StartTargeting()
        {
            targetObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GroundTargetPrefab"),
                cam.transform.parent);
            projector = targetObject.GetComponent<Projector>();
            projector.orthographicSize = targetRadius;
            projector.ignoreLayers = ~layerMask;

            monoBehaviour.StartCoroutine(TargetingRoutine());
        }

        public object GetTarget()
        {
            if (!IsTargeting)
            {
                throw new InvalidOperationException("GetTarget can not be called before StartTargeting");
            }

            Physics.Raycast(targetObject.transform.position, Vector3.down, out var hit, Mathf.Infinity, layerMask,
                QueryTriggerInteraction.Ignore);

            IsTargeting = false;

            return hit.point as object;
        }

        private IEnumerator TargetingRoutine()
        {
            IsTargeting = true;
            while (IsTargeting)
            {
                Ray ray = cam.ScreenPointToRay(screenCenter);

                if (Physics.Raycast(ray, out var hit, range, 1 << 8 /*,LayerMask.NameToLayer("Ground")*/))
                {
                    Vector2 hitInXZPlane = new Vector2(hit.point.x, hit.point.z);
                    var position = cam.transform.position;
                    Vector2 cameraInXZPlane = new Vector2(position.x, position.z);
                    float distance = Vector2.Distance(hitInXZPlane, cameraInXZPlane);
                    newPos.z = distance;
                }
                else
                {
                    newPos.z = range;
                }

                targetObject.transform.localPosition = newPos;
                yield return null;
            }

            Object.Destroy(targetObject);
        }
    }
}