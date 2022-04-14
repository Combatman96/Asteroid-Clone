using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeSpan = 10f;
    public GameObject ExpolosionFX;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeSpan);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Explostion FX
        Instantiate(ExpolosionFX, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
