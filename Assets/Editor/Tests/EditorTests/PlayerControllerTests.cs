using Editor.Tests.EditorTests.Builder;
using NSubstitute;
using NUnit.Framework;
using SejDev;
using SejDev.Systems.Core;

namespace Editor.Tests.EditorTests
{
    public class HealthManagerTests
    {
        HealthManager healthManager;

        [SetUp]
        public void SetUp()
        {
            healthManager = A.HealthManager();
        }

        public class Initialisation : HealthManagerTests
        {
            [Test]
            public void Set_100_For_Max_Health_Results_In_100_Max_Health()
            {
                healthManager.Init(100);

                Assert.AreEqual(100, healthManager.MaxHealth);
            }

            [Test]
            public void Max_Health_Value_Is_Set_For_Current_When_Not_Specified_Seperately()
            {
                healthManager.Init(100);

                Assert.AreEqual(100, ((IHealth) healthManager).CurrentHealth);
            }

            [Test]
            public void Set_Current_Health_Lower_Than_MaxHealth_Retains_Current_Health()
            {
                healthManager.Init(100, 50);

                Assert.AreEqual(50, ((IHealth) healthManager).CurrentHealth);
            }
        }

        public class GeneralBehaviour : HealthManagerTests
        {
            [Test]
            public void Player_Starts_At_Max_Health()
            {
                Assert.AreEqual(healthManager.MaxHealth, ((IHealth) healthManager).CurrentHealth);
            }
        }

        public class TakeDamage : HealthManagerTests
        {
            [Test]
            public void Current_Health_Is_Reduced_By_Damage_Amount()
            {
                int damageAmount = 10;

                healthManager.TakeDamage(null, damageAmount);

                Assert.AreEqual(healthManager.MaxHealth - damageAmount, ((IHealth) healthManager).CurrentHealth);
            }

            [Test]
            public void Current_Health_Ignores_Overkill_And_Becomes_0()
            {
                int damageAmount = int.MaxValue;

                healthManager.TakeDamage(null, damageAmount);

                Assert.AreEqual(0, ((IHealth) healthManager).CurrentHealth);
            }
        }

        public class Heal : HealthManagerTests
        {
            [Test]
            public void Healing_10_On_20_Health_With_100_Max_Health_Results_In_30()
            {
                // playerController = A.PlayerController().WithCurrentHealth(20).WithMaxHealth(100);
                healthManager = Substitute.For<HealthManager>();
                int health = 20;
                ((IHealth) healthManager).CurrentHealth.Returns(health);
                healthManager.Heal(null, 10);
                Assert.AreEqual(30, ((IHealth) healthManager).CurrentHealth);
            }
        }
    }
}