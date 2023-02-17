using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
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

    [Header("Object Interaction")]
    ReceiverLogic objectInteraction;
    Seperator seperator;

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
    }

    private void Update()
    {
        count = 0;
        CastLaser(transform.position, transform.up);
    }

    private void CastLaser(Vector3 position, Vector3 direction)
    {
        laser.SetPosition(0, transform.position);

        for (int i = 0; i < maxBounce; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (count < maxBounce - 1)
            {
                count++;

                if (Physics.Raycast(ray, out hit, 300))
                {
                    distance = Vector3.Distance(transform.position, hit.point);
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                    laser.SetPosition(count, hit.point);
                    HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                    HitEffect.transform.rotation = Quaternion.identity;

                    //* Receiver logic
                    if (hit.transform.tag == "Receiver")
                    {
                        objectInteraction = hit.transform.gameObject.GetComponent<ReceiverLogic>();
                        objectInteraction.ActivateObject();
                    }
                    else
                    {
                        if (objectInteraction != null)
                        {
                            objectInteraction.DeactivateObject();
                            objectInteraction = null;
                        }
                    }

                    //TODO: Seperator logic
                    if (hit.transform.tag == "Seperator")
                    {
                        Seperator separator = hit.transform.gameObject.GetComponent<Seperator>();

                        Vector3 outgoingDirection = hit.point - transform.position;

                        separator.Separate(outgoingDirection);

                    }
        




                    //TODO: Gate logic

                    //* Mirror logic
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
                        
                        Vector3 outgoingDirection = hit.point - transform.position;
                        Debug.DrawRay(hit.point, outgoingDirection, Color.red);
                    }

                }
                else
                {
                    laser.SetPosition(count, ray.GetPoint(300));
                }
            }
        }
    }
}