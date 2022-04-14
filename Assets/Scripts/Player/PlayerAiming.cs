using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] Camera cam;
    private Rigidbody2D rb;
    private Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle; 
    }
}
