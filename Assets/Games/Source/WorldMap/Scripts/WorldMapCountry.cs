using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class WorldMapCountry : MonoBehaviour
{
    // Variables
    private float distance;

    // Texts
    private string clickCountryText;

    // Colors
    private Material defaultMaterial;
    public Material hoverMaterial;

    private WorldMapManager _worldMapManager;


    void Start()
    {
        _worldMapManager = FindObjectOfType<WorldMapManager>();
        defaultMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public void QuestionChecker(int currentQuestion)
    {
        // Correct Answer
        if (gameObject.name == _worldMapManager.CorrectCountries[_worldMapManager.currentQuestion].country.name)
        {
            AudioPlayer.Instance.PlayAudio(0);

            // Spawn Coorect Marker
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject cube = Instantiate(_worldMapManager.markerCorrect, worldPosition, Quaternion.identity);
            cube.transform.position = new Vector3(worldPosition.x, worldPosition.y, -8);
            cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            
            // Change to next question
            _worldMapManager.currentQuestion++;
            _worldMapManager.SetCurrentQuestion(_worldMapManager.currentQuestion);
            StartCoroutine(CorrectAnswerReset(3f));
        }

        // Wrong Answer
        else
        {
            AudioPlayer.Instance.PlayAudio(1);

            if (distance < 2)
            {
                _worldMapManager.responseText.text = ($"<color={_worldMapManager.colors[0]}>Very Close!</color> Location is <color={_worldMapManager.colors[0]}>{distance}</color> units away from <color={_worldMapManager.colors[4]}>{gameObject.name}</color>.");
            }
            else if (distance < 7)
            {
                _worldMapManager.responseText.text = ($"<color={_worldMapManager.colors[1]}>Close!</color> Location is <color={_worldMapManager.colors[1]}>{distance}</color> units away from <color={_worldMapManager.colors[4]}>{gameObject.name}</color>.");
            }
            else if (distance < 20)
            {
                _worldMapManager.responseText.text = ($"<color={_worldMapManager.colors[2]}>Far!</color> Location is <color={_worldMapManager.colors[2]}>{distance}</color> units away from <color={_worldMapManager.colors[4]}>{gameObject.name}</color>.");
            }
            else
            {
                _worldMapManager.responseText.text = ($"<color={_worldMapManager.colors[3]}>Very Far!</color> Location is <color={_worldMapManager.colors[3]}>{distance}</color> units away from <color={_worldMapManager.colors[4]}>{gameObject.name}</color>.");
            }

            // Spawn Marker at mouse position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject cube = Instantiate(_worldMapManager.markerWrong, worldPosition, Quaternion.identity);
            cube.transform.position = new Vector3(worldPosition.x, worldPosition.y, -8);
            cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    void OnMouseDown()
    {
        DistanceChecker();
        QuestionChecker(_worldMapManager.currentQuestion);
    }

    void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material = hoverMaterial;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }

    public void DistanceChecker()
    {
        distance = Vector3.Distance(gameObject.transform.GetChild(0).transform.position,
                                    _worldMapManager.CorrectCountries[_worldMapManager.currentQuestion].country.transform.GetChild(0).transform.position);

        distance = Mathf.Round(distance * 100f) / 100f;
    }

    
    private IEnumerator CorrectAnswerReset(float waitTime)
    {
        _worldMapManager.questionText.text = "Correct!";
        _worldMapManager.responseText.text = string.Empty;
        yield return new WaitForSeconds(waitTime);
        _worldMapManager.questionText.text = _worldMapManager.CorrectCountries[_worldMapManager.currentQuestion].question;
    }
}
