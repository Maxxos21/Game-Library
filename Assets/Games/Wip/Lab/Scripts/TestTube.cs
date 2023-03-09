using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTube : MonoBehaviour
{
    [SerializeField] private FlameTestData flameTestReaction;
    [SerializeField] private Color[] sulfateReaction;

    [SerializeField] private float colorShiftDuration;

    [HideInInspector] public List<LabItem> collidedLabItems = new List<LabItem>();

    private Image img;
    private Color c_original;

    private Vector3 originalPos;

    private Vector3 screenPoint;
    private Vector3 screenOffset;
    public bool isPickedUp { get; private set; } = false;

    private bool movementLocked = false;

    [Serializable] public struct FlameTestData
    {
        public bool hasReaction;
        public bool ignites;
        public string scent;
    }

    void Awake()
    {
        originalPos = transform.position;

        img = GetComponent<Image>();
        c_original = img.color;
    }

    void Update()
    {
        if (isPickedUp)
        {
            DragItem();
        }
    }

    public void OnMouseDown()
    {
        if (isPickedUp)
        {
            isPickedUp = false;
            transform.position = originalPos;

            ResetTestTube();
        }
        else if (!movementLocked)
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

    private void ResetTestTube()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public Vector2 GetHorizontalBounds()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float halfWidth = rt.rect.width / 2;
        return new Vector2(transform.localPosition.x - halfWidth, transform.localPosition.x + halfWidth);
    }

    public void StartColorShiftReaction(Color[] reaction)
    {
        StartCoroutine(ColorShift(reaction, 0));
    }

    public FlameTestData GetFlameTestReaction()
    {
        return flameTestReaction;
    }

    public Color[] GetSulfateReaction()
    {
        return sulfateReaction;
    }

    public void LockMovement()
    {
        movementLocked = true;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    IEnumerator ColorShift(Color[] colors, int nextColor)
    {
        if (nextColor < colors.Length)
        {
            Color startColor = img.color;
            Color endColor = colors[nextColor];

            float timeElapsed = 0.0f;
            while (timeElapsed < colorShiftDuration)
            {
                float t = timeElapsed / colorShiftDuration;
                t = t * t * (3f - 2f * t);

                img.color = Color.Lerp(startColor, endColor, t);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            img.color = endColor;
            nextColor++;

            StartCoroutine(ColorShift(colors, nextColor));
        }
    }
}
