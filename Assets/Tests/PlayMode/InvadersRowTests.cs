using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class InvadersRowTests
    {
        [UnityTest]
        public IEnumerator LeadingRightTest()
        {
            var invaders = new []
            {
                new GameObject().AddComponent<Invader>(),
                new GameObject().AddComponent<Invader>(),
                new GameObject().AddComponent<Invader>()
            };
            var border = new Vector2(invaders[0].Speed * invaders[0].StepMultiplier * 2, 2);
            var invadersRow = new InvadersRow(invaders, border);
            var time = Time.fixedUnscaledDeltaTime *
                       (Mathf.RoundToInt(border.x / 2 / (invaders[0].Speed * invaders[0].StepMultiplier) + invadersRow.LeadingRight.transform.position.x) + 1);
            invadersRow.Start();
            yield return new WaitForSeconds(time);
            var expectedPosition = Vector2.zero;
            foreach (var invader in invaders)
            {
                expectedPosition = new Vector2(invader.transform.position.x,
                    -invader.VerticalSpeed * 7);
                Debug.Log(expectedPosition);
                Debug.Log(invader.transform.position);
                Assert.AreEqual(true, Mathf.Abs(Vector2.Distance(expectedPosition, invader.transform.position)) < .01f);
            }
        }
    
        [UnityTest]
        public IEnumerator LeadingLeftTest()
        {
            var invaders = new []
            {
                new GameObject().AddComponent<Invader>(),
                new GameObject().AddComponent<Invader>(),
                new GameObject().AddComponent<Invader>()
            };
            var border = new Vector2(invaders[0].Speed * invaders[0].StepMultiplier * 2, 2);
            var invadersRow = new InvadersRow(invaders, border);
            var time = Time.fixedUnscaledDeltaTime *
                       (Mathf.RoundToInt(border.x / 2 / (invaders[0].Speed * invaders[0].StepMultiplier) + invadersRow.LeadingLeft.transform.position.x) + 1);
            invadersRow.Start();
            yield return new WaitForSeconds(time);
            var expectedPosition = Vector2.zero;
            foreach (var invader in invaders)
            {
                expectedPosition = new Vector2(invader.transform.position.x,
                    -invader.VerticalSpeed * 7);
                Debug.Log(expectedPosition);
                Debug.Log(invader.transform.position);
                Assert.AreEqual(true, Mathf.Abs(Vector2.Distance(expectedPosition, invader.transform.position)) < .01f);
            }
        }
    }
}
