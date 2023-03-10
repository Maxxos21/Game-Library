using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotOpen : MonoBehaviour
{
    public Transform button;
    public Transform door;
    public Transform cubePusher;
    public Transform target;
    public float buttonDownDistance = 0.1f;
    public float doorOpenDistance = 0.5f;
    public float buttonDownTime = 0.1f;
    public float doorOpenTime = 1.0f;
    private bool doorOpened = false;
    public float speed;

    void OnMouseDown()
    {
        if (!doorOpened)
        {
            StartCoroutine(MoveButtonAndDoor());
        }
        else
        {
            StartCoroutine(MoveButton());
        }
    }

    IEnumerator MoveButtonAndDoor()
    {
        // Move button down
        AudioPlayer.Instance.PlayAudio(0);

        Vector3 buttonPos = button.position;
        button.position = new Vector3(buttonPos.x, buttonPos.y - buttonDownDistance, buttonPos.z);
        yield return new WaitForSeconds(buttonDownTime);

        // Move door up
        Vector3 doorPos = door.position;
        door.position = new Vector3(doorPos.x, doorPos.y + doorOpenDistance, doorPos.z);
        doorOpened = true;
        yield return new WaitForSeconds(doorOpenTime);

        // Move button back up
        button.position = buttonPos;
    }

    void Move() 
    {
        float step = speed * Time.deltaTime;
        // Move our position a step closer to the target.
        cubePusher.position = Vector3.MoveTowards(cubePusher.position, target.position, step);
    }

    void Update()
    {
        if (doorOpened)
        {
            Move();
        }
    }



    IEnumerator MoveButton()
    {
        // Move button down
        AudioPlayer.Instance.PlayAudio(0);
        
        Vector3 buttonPos = button.position;
        button.position = new Vector3(buttonPos.x, buttonPos.y - buttonDownDistance, buttonPos.z);
        yield return new WaitForSeconds(buttonDownTime);

        // Move button back up
        button.position = buttonPos;
    }
}