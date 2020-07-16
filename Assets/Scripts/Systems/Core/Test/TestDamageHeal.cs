using UnityEngine;

namespace SejDev.Systems.Core.Test
{
    public class TestDamageHeal : MonoBehaviour
    {
        private readonly int amount = 10;
        public bool shouldHeal;

        private void OnTriggerEnter(Collider other)
        {
            if (shouldHeal)
            {
                var healable = other.gameObject.GetComponent<IHealable>();
                if (healable != null) healable.Heal(this, amount);
            }
            else
            {
                var damagable = other.gameObject.GetComponent<IDamagable>();
                if (damagable != null) damagable.TakeDamage(this, amount);
            }
        }
    }
}