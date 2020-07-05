using System.Collections;
using NSubstitute;
using NUnit.Framework;
using SejDev.Systems.Stats;
using UnityEditor;
using UnityEngine.TestTools;

namespace Editor.Tests.EditorTests
{
    public class StatModifierTest
    {

        [Test]
        [TestCase(100,0.9f,1.1f,-15,-45,45)]
        [TestCase(100,0.9f,1.1f,14,2,112)]
        [TestCase(100,0.9f,1.1f,15,-45,70)]
        [TestCase(100,0.9f,1.1f,14,-2,110)]
        [TestCase(100,0.9f,1.1f,-15,45,130)]
        [TestCase(100,0.9f,1.1f,-14,2,90)]
        [TestCase(100,0.9f,1.1f,15,45,155)]
        [TestCase(100,0.9f,1.1f,-14,-2,88)]
        
        [TestCase(100,0.5f,1.5f,-14,-2,84)]
        [TestCase(100,0.5f,1.5f,15,45,160)]
        [TestCase(100,0.5f,1.5f,0,-45,55)]
        [TestCase(100,0.5f,1.5f,-14,0,86)]

        public void StatModifierTestSimplePasses(float baseValue,float minValue,float maxValue,  float absoluteNormal, float absoluteOverriding,float expected)
        {
            StatRestrictor restrictor = Substitute.For<StatRestrictor>();
            restrictor.maxPercent = maxValue;
            restrictor.minPercent = minValue;
            ModifierEvalutationTestImpl modifierEvalutationTestImpl = new ModifierEvalutationTestImpl();
            float result = modifierEvalutationTestImpl.Evaluate(baseValue, absoluteNormal, absoluteOverriding, restrictor);
            Assert.AreEqual(expected,result);
        }
    }
}