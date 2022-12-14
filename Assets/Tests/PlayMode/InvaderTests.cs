using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class InvaderTests
    {
        [UnityTest]
        public IEnumerator MovementTest()
        {
            var invader = new GameObject().AddComponent<Invader>();
            var expectedPosition = new Vector2(invader.Speed * invader.StepMultiplier, 0);
            invader.SetBorders(new Vector2(2, 2));
            invader.SetUpdateTime(Time.fixedUnscaledDeltaTime);
            invader.StartMoving();
            yield return new WaitForSeconds(Time.fixedUnscaledDeltaTime + Time.fixedUnscaledDeltaTime / 2);
            Assert.AreEqual(true, Mathf.Abs(Vector2.Distance(expectedPosition, invader.transform.position)) < .01f);
        }

        [UnityTest]
        public IEnumerator BorderLogicTest()
        {
            var invader = new GameObject().AddComponent<Invader>();
            var border = new Vector2(invader.Speed * invader.StepMultiplier * 2, 2);
            var time = Time.fixedUnscaledDeltaTime *
                       (Mathf.RoundToInt(border.x / 2 / (invader.Speed * invader.StepMultiplier)) + 1);
            invader.SetBorders(border);
            invader.SetUpdateTime(Time.fixedUnscaledDeltaTime);
            invader.OnMoveDown += invader.MoveDown;
            invader.StartMoving();
            yield return new WaitForSeconds(time);
            var expectedPosition = new Vector2(0, -invader.VerticalSpeed);
            Debug.Log(expectedPosition);
            Debug.Log(time);
            Debug.Log(invader.transform.position);
            Assert.AreEqual(true, Mathf.Abs(Vector2.Distance(expectedPosition, invader.transform.position)) < .01f);
        }
    }
}