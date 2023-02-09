using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipeRotate : MonoBehaviour
{
    private PipeManager _pipeManager;
    private PipeLevelCreator _pipeLevelCreator;

    void Awake()
    {
        _pipeManager = FindObjectOfType<PipeManager>();
        _pipeLevelCreator = GetComponent<PipeLevelCreator>();
    }

    private void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (_pipeManager.isSolved) return;
            
            Rotate();
        }
    }

    private void Rotate()
    {
        if (_pipeLevelCreator.activeOption == PipeLevelCreator.ChildActivationEnum.Straight)
        {
            _pipeLevelCreator.rotation = (_pipeLevelCreator.rotation + 1) % 2;
        }
        else if (_pipeLevelCreator.activeOption == PipeLevelCreator.ChildActivationEnum.Cross)
        {
            _pipeLevelCreator.rotation = 0;
        }
        else
        {
            _pipeLevelCreator.rotation = (_pipeLevelCreator.rotation + 1) % 4;
        }
                
        _pipeLevelCreator.UpdateRotation();

        if (_pipeManager.pipePrefab.Contains(gameObject))
        {   
            _pipeManager.GetPipeRotations();
        }
    }
}
