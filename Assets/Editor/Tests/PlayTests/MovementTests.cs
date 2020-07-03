using System.Collections;
using NSubstitute;
using NUnit.Framework;
using SejDev;
using SejDev.Player;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayTests
{
    public class MovementTests
    {
        GameObject gameObject;
        PlayerController player;

        [SetUp]
        public void SetUp()
        {
            gameObject = new GameObject();
            gameObject.transform.position = Vector3.zero;
            player = gameObject.AddComponent<PlayerController>();
        }

        [UnityTest]
        public IEnumerator MoveUp()
        {
            IEntityController controller = Substitute.For<IEntityController>();
            var move = new Vector3(0,0,1);
            player.MovementData.Returns(move);

            yield return new WaitForSeconds(2.2f);

            Assert.Greater(player.gameObject.transform.position.z, 0);
            Assert.AreEqual(0, player.gameObject.transform.position.y);
            Assert.AreEqual(0, player.gameObject.transform.position.x);
        }

        // [UnityTest]
        // public IEnumerator MoveDown()
        // {
        //     player.OnMovement(new Vector2(0, -1));

        //     yield return new WaitForSeconds(0.2f);

        //     Assert.Less(player.gameObject.transform.position.z, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.y, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.x, 0);
        // }

        // [UnityTest]
        // public IEnumerator MoveLeft()
        // {
        //     player.OnMovement(new Vector2(-1, 0));

        //     yield return new WaitForSeconds(0.2f);

        //     Assert.Greater(player.gameObject.transform.position.x, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.y, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.z, 0);
        // }

        // [UnityTest]
        // public IEnumerator MoveRight()
        // {
        //     player.OnMovement(new Vector2(1, 0));

        //     yield return new WaitForSeconds(0.2f);

        //     Assert.Less(player.gameObject.transform.position.x, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.y, 0);
        //     Assert.AreEqual(player.gameObject.transform.position.z, 0);
        // }
    }
}
