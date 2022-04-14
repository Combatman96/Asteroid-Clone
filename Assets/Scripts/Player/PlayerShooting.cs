using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    public GameObject fireBulletPrefab;
    [SerializeField]  float fireForce;
    Rigidbody2D rb;

    [SerializeField] AudioSource shootingSFX;

    // Update is called once per frame
    void Update()
    {
        // Get input form mouse left click
        if(Input.GetButtonDown("Fire1"))
        {
            //SHOOT the Bullet
            GameObject bullet = Instantiate( fireBulletPrefab, firePoint.position, firePoint.rotation);
            rb = bullet.GetComponent<Rigidbody2D> ();
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            shootingSFX.Play();
        }
    }
}
