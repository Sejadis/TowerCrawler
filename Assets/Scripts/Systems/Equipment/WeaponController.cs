using System;
using System.Collections.Generic;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Animation animation;

        [SerializeField] private Collider collider;
        private bool isAttacking;
        public Action<object> HitEffect { get; set; }
        private List<Collider> colliderMemory = new List<Collider>();

        // Start is called before the first frame update
        void Start()
        {
            collider.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                TriggerAttack();
            }

            if (isAttacking && animation.isPlaying == false)
            {
                isAttacking = false;
                collider.enabled = false;
                colliderMemory.Clear();
            }
        }

        public void TriggerAttack()
        {
            isAttacking = true;
            collider.enabled = true;
            animation.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (colliderMemory.Contains(other)) return;
            HitEffect(other.transform);
            Debug.Log("did collide with " + other);
            colliderMemory.Add(other);
        }
    }
}