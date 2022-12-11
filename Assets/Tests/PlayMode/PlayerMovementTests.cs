using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class PlayerMovementTests
    {
        [UnityTest]
        public IEnumerator MovementTest()
        {
            var playerMovement = new GameObject().AddComponent<Player>();
            var expectedPosition = new Vector2(playerMovement.Speed * Time.fixedUnscaledDeltaTime, 0);
            playerMovement.MoveDirection(Vector2.right);
            playerMovement.MoveRight(true);
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime);
            Assert.AreEqual(true, Vector2.Distance(expectedPosition, playerMovement.transform.position) < .1f);
        }
        
        [UnityTest]
        public IEnumerator BordersLogicTest()
        {
            var playerMovement = new GameObject().AddComponent<Player>();
            var expectedPosition = new Vector2(0, 0);
            playerMovement.SetBorders(Vector2.zero);
            playerMovement.MoveDirection(Vector2.left);
            playerMovement.MoveDirection(Vector2.right);
            playerMovement.MoveLeft(true);
            playerMovement.MoveRight(false);
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime);
            Assert.AreEqual(true, Vector2.Distance(expectedPosition, playerMovement.transform.position) < .1f);
        }
    }
}
