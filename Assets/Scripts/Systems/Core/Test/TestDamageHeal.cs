using UnityEngine;

namespace SejDev.Systems.Core.Test
{
    public class TestDamageHeal : MonoBehaviour
    {
        public bool shouldHeal = false;
        int amount = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (shouldHeal)
            {
                var healable = other.gameObject.GetComponent<IHealable>();
                if (healable != null)
                {
                    healable.Heal(amount);
                }
            }
            else
            {
                var damagable = other.gameObject.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(this, amount);
                }
            }
        }
    }
}
