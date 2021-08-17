using UnityEngine;

public class BumperAnimation : MonoBehaviour
{
    Animator anim;
    public GameObject ringSpawn;
    Vector3 ringLocation;
    public GameObject ring;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        ringLocation = new Vector3(ringSpawn.transform.position.x, ringSpawn.transform.position.y, ringSpawn.transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("HittingBall", true);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("HittingBall", false);
        }
    }

    void SpawnRing()
    {
        Instantiate(ring, ringLocation, Quaternion.identity);
    }
}
