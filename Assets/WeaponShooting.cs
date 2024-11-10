using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public float damageAmount = 10f;
    public float shootingRange = 100f;
    public Transform weaponTip;
    public LineRenderer lineRenderer;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null) {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            Shoot();
        }

        UpdateLineOfSight();
    }

    void Shoot() {
        Vector3 adjustedDirection = Quaternion.Euler(0, -90, 0) * weaponTip.forward;

        Debug.Log("Shoot() called.");

        Ray ray = new Ray(weaponTip.position, adjustedDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange)) {
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy")) {
                EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();

                if (enemy != null) {
                    enemy.TakeDamage(damageAmount);
                    Debug.Log("Shot " + hit.collider.name);
                }
            }
            lineRenderer.SetPosition(1, hit.point);
        } else {
            lineRenderer.SetPosition(1, weaponTip.position + weaponTip.forward * shootingRange);
        }

    }

    void UpdateLineOfSight() {
        if (lineRenderer != null) {
            lineRenderer.SetPosition(0, weaponTip.position);
            Vector3 adjustedDirection = Quaternion.Euler(0, -90, 0) * weaponTip.forward;
            lineRenderer.SetPosition(1, weaponTip.position + adjustedDirection * shootingRange);
        }
    }
}
