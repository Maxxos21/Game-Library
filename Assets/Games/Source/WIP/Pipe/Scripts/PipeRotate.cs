using UnityEngine;

public class PipeRotate : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Rotate the pipe
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


        // Update the pipe manager
        PipeManager pipeManager = FindObjectOfType<PipeManager>();
        pipeManager.GetPipeRotations();

    }
}
