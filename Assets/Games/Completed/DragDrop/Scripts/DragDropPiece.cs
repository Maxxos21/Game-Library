using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class DragDropPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    [SerializeField] public Transform parentAfterDrag;
    [SerializeField] private CanvasGroup canvasGroup;

    public Dictionary<int, Vector3> originalPositions = new Dictionary<int, Vector3>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!originalPositions.ContainsKey(0))
        {
            originalPositions.Add(0, transform.position);
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        if (transform.parent.tag == "Correct")
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
}

