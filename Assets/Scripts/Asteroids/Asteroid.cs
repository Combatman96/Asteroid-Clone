using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] asteroidsSprireList;
    private SpriteRenderer spriteRenderer;
    
    public float size; 
    public float maxSize = 1.5f;
    public float minSize = 0.4f;
    public Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeSpan = 20f;
    private AudioSource ExplosionSFX;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ///----------------------------------------------------------------------------------
        /// Random how the asteroid would look by different sprite, rotation and size/mass 
        ///----------------------------------------------------------------------------------
        
        //Random Sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = asteroidsSprireList[(int)Random.Range(0, asteroidsSprireList.Length)];
        
        //Random Rotaion
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        //Random Size and Mass, this two are equal
        transform.localScale = Vector3.one * size;       
        rb.mass = size;        

        //Kill it after a perious of time
        Destroy(this.gameObject, lifeSpan);

        ExplosionSFX = GameObject.Find("ExplosionSFX").GetComponent<AudioSource>();
    }

    public void SetTrajectory(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }


    //If asteroids got hit by bullet then ..
    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Slit it in 2 if size is big enought
        if(size/2 >= minSize)
        {
            //Okay to spawn 2 smaller asteroids
            SplitAsteroids();
            SplitAsteroids();
        }

        ExplosionSFX.Play();

        //Update the player score
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.UpdateScore();

        Destroy(this.gameObject);    
    }

    private void SplitAsteroids()
    {
        Vector2 splitPos = Random.insideUnitCircle.normalized * 0.5f;
        splitPos += (Vector2)this.transform.position;
        
        Asteroid smallAsteroid = Instantiate(this, splitPos, this.transform.rotation);
        smallAsteroid.size = this.size/2;
        smallAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
