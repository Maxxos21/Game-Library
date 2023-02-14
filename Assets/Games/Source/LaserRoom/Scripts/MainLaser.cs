using UnityEngine;
using UnityEngine.Events;

public class MainLaser : MonoBehaviour
{
    const float LASER_WIDTH = 0.2f;
    public GameObject HitEffect;
    public GameObject receiver;
    public float HitOffset = 0;
    private int maxBounce = 20;
    private LineRenderer laser;
    private int count;
    private ParticleSystem[] psEffects;
    private ParticleSystem[] psHit;
    public bool isHittingReceiver;
    float previousDistance;
    float distance;
    public Material defaultMat, hitMat, screenMat, laserMat;
    private Renderer rend;


    void Awake()
    {
        rend = receiver.GetComponent<Renderer>();
        laser = GetComponent<LineRenderer>();
        psEffects = GetComponentsInChildren<ParticleSystem>();
        psHit = HitEffect.GetComponentsInChildren<ParticleSystem>();
        
        laser.startWidth = LASER_WIDTH;
        laser.endWidth = LASER_WIDTH;
    }

    void Start()
    {
        laser.SetPosition(0, transform.position);
        laser.positionCount = maxBounce;

        if (AudioPlayer.Instance != null)
        {
            AudioPlayer.Instance.PlayAudio(1);
        }
    }

    private void Update()
    {
        count = 0;
        CastLaser(transform.position, transform.up);

        if (Mathf.Abs(previousDistance - distance) > 0.5f)
        {
            if (AudioPlayer.Instance != null)
            {
                AudioPlayer.Instance.PlayAudio(1);
            }
        }
        previousDistance = distance;
    }

    private void CastLaser(Vector3 position, Vector3 direction)
    {
        laser.SetPosition(0, transform.position);

        for (int i = 0; i < maxBounce; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (count < maxBounce - 1)

                count++;

                if (Physics.Raycast(ray, out hit, 300))
                {
                    distance = Vector3.Distance(transform.position, hit.point);
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                    laser.SetPosition(count, hit.point);
                    HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                    HitEffect.transform.rotation = Quaternion.identity;

                    //! Reicever
                    if (hit.transform.gameObject == receiver) 
                    { 
                        isHittingReceiver = true;

                        Material[] mats = rend.materials;
                        mats[2] = hitMat;
                        rend.materials = mats;
                    }
                    else 
                    { 
                        isHittingReceiver = false;
                        
                        Material[] mats = rend.materials;
                        mats[2] = laserMat;
                        rend.materials = mats;
                    }

                    //! Gate
                    // if (hit.transform.tag == "Gate")
                    // {
                    //     Debug.Log("Gate");
                        
                    //     // get a reference to the object hit
                    //     GameObject hitObject = hit.transform.gameObject;
                    //     rend = hitObject.GetComponent<Renderer>();
                    //     Material[] mats = rend.materials;
                    //     mats[2] = hitMat;
                    //     rend.materials = mats;
                    // }
                    // else
                    // {
                    //     Debug.Log("Not Gate");
                    //     GameObject hitObject = hit.transform.gameObject;
                    //     rend = hitObject.GetComponent<Renderer>();
                    //     Material[] mats = rend.materials;
                    //     mats[2] = defaultMat;
                    //     rend.materials = mats;
                    // }

                    //! Mirror
                    if (hit.transform.tag != "Mirror")
                    {   
                        for (int j = (i + 1); j < maxBounce; j++)
                        {
                            laser.SetPosition(j, hit.point);
                        }
                        break;
                    }
                    else
                    {
                        laser.SetPosition(count, hit.point);
                        
                    }
                }
            else
            {
                laser.SetPosition(count, ray.GetPoint(300));
            }
        }
    }
}
