using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafeController : MonoBehaviour
{
    // Public Variables
    public float rotationTarget = 36.0f;
    public string[] solution;

    // Private Variables
    public float rotationSpeed = 10.0f;
    private bool isRotating = false;
    private int _currentRotationValue = 0;


    [HideInInspector]
    [SerializeField] private TMP_Text _firstDigit, _secondDigit, _thirdDigit;
    [HideInInspector]
    [SerializeField] private ToggleGroup _toggleGroup;

    // Enum to store the current rotation value
    public enum RotationValue
    {
        Zero, One, Two, Three, Four, 
        Five, Six, Seven, Eight, Nine
    }

    public RotationValue currentRotationValue = RotationValue.Zero;

    public void RotateClockwise()
    {
        if (!isRotating)
        {
            isRotating = true;
            currentRotationValue = (RotationValue)(((int)currentRotationValue + 1) % 10);

            // Dial to Text
            _currentRotationValue = (int)currentRotationValue * 10;

            // check wich toggle is active and set the text
            int active = _toggleGroup.GetFirstActiveToggle().transform.GetSiblingIndex();
            switch (active)
            {
                case 0:
                    _firstDigit.text = _currentRotationValue.ToString();
                    break;
                case 1:
                    _secondDigit.text = _currentRotationValue.ToString();
                    break;
                case 2:
                    _thirdDigit.text = _currentRotationValue.ToString();
                    break;
            }
            // Start the rotation
            StartCoroutine(Rotate(Vector3.down));
            AudioPlayer.Instance.PlayAudio(0);
        }
    }


    public void RotateCounterClockwise()
    {
        if (!isRotating)
        {
            isRotating = true;
            currentRotationValue = (RotationValue)(((int)currentRotationValue + 9) % 10);

            // Dial to Text
            _currentRotationValue = (int)currentRotationValue * 10;

            // check wich toggle is active and set the text
            int active = _toggleGroup.GetFirstActiveToggle().transform.GetSiblingIndex();
            switch (active)
            {
                case 0:
                    _firstDigit.text = _currentRotationValue.ToString();
                    break;
                case 1:
                    _secondDigit.text = _currentRotationValue.ToString();
                    break;
                case 2:
                    _thirdDigit.text = _currentRotationValue.ToString();
                    break;
            }


            // Start the rotation
            StartCoroutine(Rotate(Vector3.up));
            AudioPlayer.Instance.PlayAudio(0);
        }
    }

    public RotationValue GetCurrentRotationValue()
    {
        return currentRotationValue;
    }

    IEnumerator Rotate(Vector3 direction)
    {
        float angleRemaining = rotationTarget;
        while (angleRemaining > 0)
        {
            float angle = Mathf.Min(angleRemaining, rotationSpeed * Time.deltaTime);
            transform.Rotate(direction * angle);
            angleRemaining -= angle;
            yield return null;
        }

        isRotating = false;
        CheckSolution();
    }

    public void CheckSolution()
    {
        if (_firstDigit.text == solution[0].ToString() && _secondDigit.text == solution[1].ToString() && _thirdDigit.text == solution[2].ToString())
        {
            Debug.Log("You Win!");
        }
    }
}