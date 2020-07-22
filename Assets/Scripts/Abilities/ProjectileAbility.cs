using System;
using System.Collections.Generic;
using SejDev.Systems.Abilities;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewProjectileAbility",
        menuName = "Systems/Ability/Projectile Ability")]
    public partial class ProjectileAbility : Ability
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float forwardForce;
        [SerializeField] private float maxProjectileLifeTime = 0f;

        [SerializeField] private bool isAffectedByGravity;

        // private List<Quaternion> spawnAngles = new List<Quaternion>() {Quaternion.AngleAxis(0, Vector3.zero)};
        private readonly List<Vector2> spawnAngles = new List<Vector2>() {Vector2.zero};

        protected override void PerformAbility()
        {
            base.PerformAbility();
            var cameraTransform = abilityManager.TargetingCamera.transform;
            var right = cameraTransform.TransformDirection(Vector3.right);
            var up = cameraTransform.TransformDirection(Vector3.up);

            foreach (var angle in spawnAngles)
            {
                var rotation = Quaternion.AngleAxis(angle.x, right) * Quaternion.AngleAxis(angle.y, up);
                var projectile = Instantiate(projectilePrefab, abilityManager.AbilityOrigin.position,
                    rotation * cameraTransform.rotation);

                var rigidBody = projectile.GetComponent<Rigidbody>();
                if (rigidBody == null)
                {
                    throw new NullReferenceException("Projectile has no RigidBody");
                }

                rigidBody.useGravity = isAffectedByGravity;
                // Debug.Log($"forward: {projectile.transform.forward} \n force: {forwardForce} \n result: {projectile.transform.forward * forwardForce}");

                // rigidBody.velocity = projectile.transform.forward * forwardForce;
                // rigidBody.AddForce(projectile.transform.forward * forwardForce, ForceMode.Impulse);
                rigidBody.AddRelativeForce(Vector3.forward * forwardForce, ForceMode.Impulse);


                if (maxProjectileLifeTime > 0)
                {
                    Destroy(projectile, maxProjectileLifeTime);
                }
            }
        }
    }
}