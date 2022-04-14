using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public GameObject ExplosionFX; 
    private GameManager gameManager;
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.layer == 8)        
        {
          //  Debug.Log("We got hit");
            Instantiate(ExplosionFX, this.transform.position, this.transform.rotation);

            //Set gamestate to reset 
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.GameStateChange(GameState.DIE);

            //this.gameObject.SetActive(false);
        } 
    }
}
