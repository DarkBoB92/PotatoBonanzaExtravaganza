using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    // Pre-Requisite Variables --------------------------------------------------------------------

    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    private Transform target;
    [SerializeField] private float smoothTime;



    // Main Loops -----------------------------------------------------------------------------

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;   // Finds the vector difference between the applied GameObject and target GameObject, then stored into "offset"
    }

    private void Start()
    {
        FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Music, transform.position, 0.2f);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;   // Calculate the desired position of the camera, stored into a new Vector3
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);   // Moves the camera to the targetPosition Vector3 values smoothly using a dampening factor
        }
    }
}
