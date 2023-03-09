using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabItemFlame : LabItem
{
    [SerializeField] GameObject ignitePrefab;

    public override void CollisionActions(TestTube testTube)
    {
        TestTube.FlameTestData reaction = testTube.GetFlameTestReaction();
        if (reaction.hasReaction)
        {
            if (reaction.ignites)
            {
                GameObject obj = Instantiate(ignitePrefab, testTube.transform);
            }

            if (reaction.scent != null && reaction.scent != "")
            {
                // print the smell
                Debug.Log(reaction.scent);
            }
        }
    }
}
