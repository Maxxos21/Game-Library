using UnityEngine.EventSystems;
using UnityEngine;

public class DragDropAnswer : MonoBehaviour, IDropHandler
{
    [Tooltip("Drag and drop the correct answer piece here")]
    [SerializeField] DragDropPiece answerPiece;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDropPiece draggableItem = dropped.GetComponent<DragDropPiece>();

            if (draggableItem == answerPiece)
            {
                AudioPlayer.Instance.PlayAudio(0);
                draggableItem.parentAfterDrag = transform;
            }
            else
            {
                AudioPlayer.Instance.PlayAudio(1);
                draggableItem.transform.position = draggableItem.originalPositions[0];
            }
        }

    }

}