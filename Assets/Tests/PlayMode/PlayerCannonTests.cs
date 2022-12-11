using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class PlayerCannonTests
    {
        [UnityTest]
        public IEnumerator Shoot2BulletsTest()
        {
            var playerCannon = new GameObject().AddComponent<PlayerCannon>();
            playerCannon.SetBullet(Resources.Load<Bullet>("Bullet"));
            playerCannon.SetShootDirection(Vector2.up);
            yield return new WaitForSeconds(playerCannon.FireRate + playerCannon.FireRate / 2);
            Assert.AreEqual(2, Object.FindObjectsOfType<Bullet>().Length);
            Object.Destroy(playerCannon);
            foreach (var bullet in Object.FindObjectsOfType<Bullet>())
                Object.Destroy(bullet);
        }

        [UnityTest]
        public IEnumerator ShootDirectionTest()
        {
            var playerCannon = new GameObject().AddComponent<PlayerCannon>();
            playerCannon.SetBullet(Resources.Load<Bullet>("Bullet"));
            playerCannon.SetShootDirection(Vector2.up);
            var expectedPosition = new Vector2(0,
                playerCannon.Force * playerCannon.FireRate / 2);
            yield return new WaitForSeconds(playerCannon.FireRate / 2);
            Assert.AreEqual(true,
                Mathf.Abs(Vector2.Distance(expectedPosition,
                    Object.FindObjectOfType<Bullet>().transform.position)) < .05f);
        }
    }
}