using UnityEngine;
using UnityEngine.Events;

public class MainLaser : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private const float LASER_WIDTH = 0.2f;
    [SerializeField] private GameObject HitEffect;
    [SerializeField] private float HitOffset = 0;
    [SerializeField] private int maxBounce = 20;
    [SerializeField] private int count;
    private float previousDistance;
    private float distance;
    private LineRenderer laser;
    private ParticleSystem[] psEffects;
    private ParticleSystem[] psHit;


    [Header("Laser Events")]
    public bool isHittingReceiver, isHittingGate;
    public UnityEvent onHitReceiver, OnHitGate;
    public Material defaultMat, hitMat, screenMat, laserMat;


    void Awake()
    {
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

        // todo audio keep or remove
        // if (Mathf.Abs(previousDistance - distance) > 0.5f)
        // {
        //     if (AudioPlayer.Instance != null)
        //     {
        //         AudioPlayer.Instance.PlayAudio(1);
        //     }
        // }
        // previousDistance = distance;
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

                    ReceiverLogic(hit);
                    GateLogic(hit);

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

    void ReceiverLogic(RaycastHit hit)
    {
        if (hit.transform.tag == "Receiver") 
        { 
            isHittingReceiver = true;
        }
        else 
        { 
            isHittingReceiver = false;
        }
    }

    void GateLogic(RaycastHit hit)
    {
        if (hit.transform.tag == "Gate")
        {
            isHittingGate = true;
            // Physics.IgnoreCollision(hit.collider, GetComponent<Collider>());

            // GameObject hitObject = hit.collider.gameObject;
            // Debug.Log(hitObject.name);
        }
        else
        {
            isHittingGate = false;
        }
    }


}
