using UnityEngine;

public class MainLaser : MonoBehaviour
{
    public GameObject HitEffect;
    public GameObject laserPrefab;
    public float HitOffset = 0;
    private int maxBounce = 20;
    private LineRenderer laser;
    private int count;
    private ParticleSystem[] psEffects;
    private ParticleSystem[] psHit;
    private bool isSplit = false;

    void Start()
    {

        laser = GetComponent<LineRenderer>();
        psEffects = GetComponentsInChildren<ParticleSystem>();
        psHit = HitEffect.GetComponentsInChildren<ParticleSystem>();

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

                    if (hit.transform.tag == "Win")
                    {
                        Debug.Log("You Win!");
                    }

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
