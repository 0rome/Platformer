using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float defDistance = 10f;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public LayerMask hitLayer;
    public GameObject hitEffectPrefab;

    private Vector2 targetPosition;
    private bool hasFired = false;
    private float scanAngle = 30f;
    private float scanSpeed = 10f;
    private float currentAngleOffset = 0f;
    private bool scanningRight = true;

    public float pulseSpeed = 5f;
    public float minWidth = 0.05f;
    public float maxWidth = 0.2f;

    private GameObject currentEffect;
    private Transform player;

    private float offsetDistance = 2f;

    private void Start()
    {
        player = FindFirstObjectByType<Player>()?.transform;
        if (player != null)
        {
            SetRandomTargetPosition();
            RotateToTarget();
            hasFired = true;
        }
    }

    private void Update()
    {
        if (hasFired)
        {
            ScanArea();
            ShootLaser();
        }
    }

    private void SetRandomTargetPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * offsetDistance;
        targetPosition = (Vector2)player.position + randomOffset;
    }

    private void RotateToTarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, baseAngle);
    }

    private void ScanArea()
    {
        if (scanningRight)
        {
            currentAngleOffset += scanSpeed * Time.deltaTime;
            if (currentAngleOffset >= scanAngle)
                scanningRight = false;
        }
        else
        {
            currentAngleOffset -= scanSpeed * Time.deltaTime;
            if (currentAngleOffset <= -scanAngle)
                scanningRight = true;
        }

        transform.rotation *= Quaternion.Euler(0, 0, scanningRight ? scanSpeed * Time.deltaTime : -scanSpeed * Time.deltaTime);
    }

    private void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, defDistance, hitLayer);

        if (hit.collider != null)
        {
            DrawRay(firePoint.position, hit.point);
            ShowHitEffect(hit.point, hit.normal);

            // ✅ Мгновенное убийство, если игрок касается лазера
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>()?.Death();
            }
        }
        else
        {
            Vector2 endPos = firePoint.position + (Vector3)(firePoint.right * defDistance);
            DrawRay(firePoint.position, endPos);
            HideHitEffect();
        }

        PulseLaser();
    }

    private void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    private void ShowHitEffect(Vector2 position, Vector2 normal)
    {
        if (hitEffectPrefab != null)
        {
            if (currentEffect == null)
            {
                currentEffect = Instantiate(hitEffectPrefab, position, Quaternion.FromToRotation(Vector2.up, normal));
            }
            else
            {
                currentEffect.transform.position = position;
                currentEffect.transform.rotation = Quaternion.FromToRotation(Vector2.up, normal);
                currentEffect.SetActive(true);
            }
        }
    }

    private void HideHitEffect()
    {
        if (currentEffect != null)
        {
            currentEffect.SetActive(false);
        }
    }

    private void PulseLaser()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;
        float width = Mathf.Lerp(minWidth, maxWidth, pulse);
        lineRenderer.widthMultiplier = width;
    }
}
