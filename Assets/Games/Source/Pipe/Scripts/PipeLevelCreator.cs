using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PipeLevelCreator : MonoBehaviour
{
    public enum ChildActivationEnum { Bent, Straight, T, Cross }
    [Range(0,3)] public int rotation;
    public ChildActivationEnum activeOption;


    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Invoke("UpdateChildren", 0.1f);
            Invoke("UpdateRotation", 0.1f);
        }
    }

    void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

        switch (activeOption)
        {
            case ChildActivationEnum.Bent:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case ChildActivationEnum.Straight:
                transform.GetChild(1).gameObject.SetActive(true);
                rotation = Mathf.Clamp(rotation, 0, 1);
                break;
            case ChildActivationEnum.T:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case ChildActivationEnum.Cross:
                transform.GetChild(3).gameObject.SetActive(true);
                rotation = 0;
                break;
        }
    }

    public void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(0, 90 * rotation, 0);
    }
}