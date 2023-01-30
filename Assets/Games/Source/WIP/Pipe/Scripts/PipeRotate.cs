using UnityEngine;

public class PipeRotate : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
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
}
