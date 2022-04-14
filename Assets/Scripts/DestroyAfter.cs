
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float lifeSpan = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeSpan);
    }

}
