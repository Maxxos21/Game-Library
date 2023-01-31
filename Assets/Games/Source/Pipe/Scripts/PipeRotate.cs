using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipeRotate : MonoBehaviour
{
    private void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        PipeLevelCreator pipeLevelCreator = GetComponent<PipeLevelCreator>();
        
        if (pipeLevelCreator.activeOption == PipeLevelCreator.ChildActivationEnum.Straight)
        {
            pipeLevelCreator.rotation = (pipeLevelCreator.rotation + 1) % 2;
        }
        else
        {
            pipeLevelCreator.rotation = (pipeLevelCreator.rotation + 1) % 4;
        }        
        pipeLevelCreator.UpdateRotation();


        PipeManager pipeManager = FindObjectOfType<PipeManager>();
        pipeManager.GetPipeRotations();

    }
}
