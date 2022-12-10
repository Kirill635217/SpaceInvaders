using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class PlayerMovementTests
    {
        [UnityTest]
        public IEnumerator MoveRightTest()
        {
            var playerMovement = new GameObject().AddComponent<PlayerMovement>();
            var expectedPosition = new Vector2(playerMovement.Speed * Time.fixedUnscaledDeltaTime, 0);
            playerMovement.MoveDirection(Vector2.right);
            playerMovement.MoveRight(true);
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime);
            Assert.AreEqual(true, Vector2.Distance(expectedPosition, playerMovement.transform.position) < .1f);
        }
    
        [UnityTest]
        public IEnumerator MoveLeftTest()
        {
            var playerMovement = new GameObject().AddComponent<PlayerMovement>();
            var expectedPosition = new Vector2(-playerMovement.Speed * Time.fixedUnscaledDeltaTime, 0);
            playerMovement.MoveDirection(Vector2.left);
            playerMovement.MoveLeft(true);
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime);
            Assert.AreEqual(true, Vector2.Distance(expectedPosition, playerMovement.transform.position) < .1f);
        }
    
        [UnityTest]
        public IEnumerator MoveRightAndLeftTest()
        {
            var playerMovement = new GameObject().AddComponent<PlayerMovement>();
            var expectedPosition = new Vector2(0, 0);
            playerMovement.MoveDirection(Vector2.left);
            playerMovement.MoveDirection(Vector2.right);
            playerMovement.MoveLeft(true);
            playerMovement.MoveRight(false);
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime);
            Assert.AreEqual(true, Vector2.Distance(expectedPosition, playerMovement.transform.position) < .1f);
        }
    }
}
