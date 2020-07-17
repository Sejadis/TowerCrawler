using System;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewProjectileAbility",
        menuName = "Systems/Ability/Projectile Ability")]
    public class ProjectileAbility : Ability
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float forwardForce;
        [SerializeField] private float maxProjectileLifeTime = 0f;
        [SerializeField] private bool isAffectedByGravity;

        public override void Bind(IAbility abilityHandler)
        {
            base.Bind(abilityHandler);
        }

        protected override void PerformAbility()
        {
            base.PerformAbility();
            var projectile = Instantiate(projectilePrefab, abilityManager.AbilityOrigin.position, Quaternion.identity);
            var rigidBody = projectile.GetComponent<Rigidbody>();
            if (rigidBody == null)
            {
                throw new NullReferenceException("Projectile has no RigidBody");
            }

            rigidBody.useGravity = isAffectedByGravity;
            rigidBody.AddForce(abilityManager.TargetingCamera.transform.forward * forwardForce, ForceMode.Impulse);
            if (maxProjectileLifeTime > 0)
            {
                Destroy(projectile, maxProjectileLifeTime);
            }
        }
    }
}