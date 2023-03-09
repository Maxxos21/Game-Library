using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LabItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        TestTube testTube = other.gameObject.GetComponent<TestTube>();
        if (!testTube.collidedLabItems.Contains(this))
        {
            testTube.collidedLabItems.Add(this);
        }

        if (testTube.collidedLabItems.Count == 1)
        {
            CollisionActions(testTube);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<TestTube>().collidedLabItems.Remove(this);
    }

    public virtual void CollisionActions(TestTube testTube)
    {

    }
}
