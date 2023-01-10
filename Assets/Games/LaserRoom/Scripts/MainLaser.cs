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

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        psEffects = GetComponentsInChildren<ParticleSystem>();
        psHit = HitEffect.GetComponentsInChildren<ParticleSystem>();

        laser.startWidth = LASER_WIDTH;
        laser.endWidth = LASER_WIDTH;

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

                count++;
                if (Physics.Raycast(ray, out hit, 300))
                {
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                    laser.SetPosition(count, hit.point);
                    HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                    HitEffect.transform.rotation = Quaternion.identity;

                    // Check if laser is hitting target
                    if (hit.transform.name == receiver.name) 
                    { 
                        isHittingReceiver = true;
                    }
                    else 
                    { 
                        isHittingReceiver = false;
                    }


                    // Check if laser is hitting mirror
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
