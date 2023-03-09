using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SulfateDrop : MonoBehaviour
{
    [SerializeField] float spd;
    [SerializeField] float scaleInDuration;

    private LabItemSulfate parentItem;
    private TestTube target;
    private float targetHeight;

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    public void SetLabItemParent(LabItemSulfate p_labItem)
    {
        parentItem = p_labItem;
    }

    public void SetTargetHeight(TestTube targetTube)
    {
        target = targetTube;
        targetHeight = target.transform.position.y;
    }

    private void EndSequence()
    {
        parentItem.ResetDropper();

        target.StartColorShiftReaction(target.GetSulfateReaction());

        Destroy(gameObject);
    }

    IEnumerator StartSequence()
    {
        float delay = 0.0f;
        StartCoroutine(ScaleIn(delay, scaleInDuration));

        delay += scaleInDuration;
        StartCoroutine(Fall(delay));

        while (transform.position.y > target.transform.position.y)
        {
            yield return null;
        }

        EndSequence();
    }

    IEnumerator ScaleIn(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(1f, 1f, 1f);

        float timeElapsed = 0.0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = endScale;
    }

    IEnumerator Fall(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (transform.position.y > target.transform.position.y)
        {
            Vector3 newPos = transform.position;
            newPos.y -= spd * Time.deltaTime;

            transform.position = newPos;

            yield return null;
        }
    }
}
