using UnityEngine;
using System.Collections;
public class Spawner : MonoBehaviour
{
    public bool canSpawn = true;
    public Asteroid AstroidsPrefab;

    [SerializeField]  int spawnAmount = 2;
    [SerializeField] float spawnRadius = 9f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float trajectoryOffset = 12f;

    // Start is called before the first frame update
    void Start() 
    {
        StartCoroutine(nameof(SpawnAsteroidAfterEverySecond));
    }

    IEnumerator SpawnAsteroidAfterEverySecond()
    {
        while(canSpawn)
        {
            yield return new WaitForSeconds(waitTime);
            Spawn();
        }
    }

    void Spawn()
    {
        //------------------------------------------------------------------
        //  Spawn asteroids around the spawner within a radius
        //------------------------------------------------------------------

        for( int i = 0; i < spawnAmount; i++)
        {
            //Random spawn Pos
            Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
            
            //Spawn the asteroids with ramdom rotation
            float angleOffset = Random.Range(-trajectoryOffset, trajectoryOffset);
            Quaternion spawnTrajectory = Quaternion.AngleAxis(angleOffset, Vector3.forward);
            Asteroid asteroid = Instantiate(AstroidsPrefab, spawnPos, spawnTrajectory);

            //Set the size for the asteroid
            float spawnSize = Random.Range(asteroid.minSize, asteroid.maxSize);  
            asteroid.size = spawnSize;
                        
            //Start moving along the trajectory         
            asteroid.SetTrajectory(spawnTrajectory * -spawnPos);
        }
    }

}
