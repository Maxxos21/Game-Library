using UnityEngine.EventSystems;
using UnityEngine;

public class DragDropAnswer : MonoBehaviour, IDropHandler
{
    [Tooltip("Drag and drop the correct answer piece here")]
    [SerializeField] DragDropPiece[] answerPiece;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 1)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDropPiece draggableItem = dropped.GetComponent<DragDropPiece>();

            for (int i = 0; i < answerPiece.Length; i++)
            {
                if (draggableItem == answerPiece[i])
                {
                    draggableItem.parentAfterDrag = transform;
                    AudioPlayer.Instance.PlayAudio(0);
                    return;
                }
            }

            AudioPlayer.Instance.PlayAudio(1);
            draggableItem.transform.position = draggableItem.originalPositions[0];
        }

    }

}