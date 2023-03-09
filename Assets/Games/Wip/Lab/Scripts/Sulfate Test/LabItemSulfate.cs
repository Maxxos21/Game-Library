using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabItemSulfate : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    private Vector3 originalPos;

    private Vector3 screenPoint;
    private Vector3 screenOffset;
    private bool isPickedUp;

    private bool isTesting;

    void Awake()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (isPickedUp && !isTesting)
        {
            DragItem();
        }
    }

    void OnMouseDown()
    {
        if (isPickedUp)
        {
            List<TestTube> tubesOver = GetTestTubesUnderSelf();
            if (tubesOver.Count == 1)
            {
                isTesting = true;

                GameObject obj = Instantiate(dropPrefab, transform.parent);
                obj.transform.position = transform.position;

                SulfateDrop drop = obj.GetComponent<SulfateDrop>();

                drop.SetLabItemParent(this);
                drop.SetTargetHeight(tubesOver[0]);

                tubesOver[0].LockMovement();
            }
            else
            {
                ResetDropper();
            }
        }
        else
        {
            isPickedUp = true;

            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            screenOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    public void DragItem()
    {
        Vector3 currScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currPos = Camera.main.ScreenToWorldPoint(currScreenPoint) + screenOffset;

        transform.position = currPos;
    }

    private List<TestTube> GetTestTubesUnderSelf()
    {
        List<TestTube> testTubes = new List<TestTube>();
        
        TestTube[] allTubes = FindObjectsOfType<TestTube>();
        foreach (TestTube testTube in allTubes)
        {
            Vector2 xBounds = testTube.GetHorizontalBounds();

            if (transform.localPosition.x >= xBounds.x && transform.localPosition.x <= xBounds.y && transform.localPosition.y > testTube.transform.localPosition.y)
            {
                testTubes.Add(testTube);
            }
        }

        return testTubes;
    }

    public void ResetDropper()
    {
        isPickedUp = false;
        isTesting = false;
        transform.position = originalPos;
    }
}
