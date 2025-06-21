using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("References")]
    public Transform weaponTransform;             // Weapon pivot (child of player)
    public SpriteRenderer playerSpriteRenderer;   // Reference to player's sprite
    public SpriteRenderer weaponSpriteRenderer;   // Reference to weapon sprite

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 5f; // bullets per second
    private float nextFireTime;

    [Header("Animation")]
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement and rotation
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        RotateWeaponToMouse();
        FlipSpritesBasedOnMouse();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if (animator != null)
            animator.SetFloat("speed", movement.sqrMagnitude);
    }
    

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseWorldPos - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(shootDir);
    }


    void RotateWeaponToMouse()
    {
        if (weaponTransform == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - weaponTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FlipSpritesBasedOnMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mouseWorldPos - transform.position;

        bool facingRight = lookDir.x >= 0f;

        // Flip player sprite
        if (playerSpriteRenderer != null)
            playerSpriteRenderer.flipX = !facingRight;

        // Flip weapon sprite vertically when aiming left
        if (weaponSpriteRenderer != null)
            weaponSpriteRenderer.flipY = !facingRight;
    }
}
