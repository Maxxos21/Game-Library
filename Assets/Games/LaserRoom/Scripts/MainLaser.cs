using UnityEngine;

public class MainLaser : MonoBehaviour
{
    // Use serialized fields instead of private variables for Unity Inspector access
    [SerializeField] private int maxBounce = 20;
    [SerializeField] private Color laserColor;

    private LineRenderer laser;
    private int count;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.material = new Material(Shader.Find("Sprites/Default"));
        laser.startColor = laserColor;
        laser.endColor = laserColor;
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;

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
