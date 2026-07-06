using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private AudioSource targetAudio;
    public AudioClip[] targetDestroyClips;

    private float minForceAmplitude = 12;
    private float maxForceAmplitude = 14;

    private float torqueRange = 5;

    private float xRange = 4;
    private float ySpawnPos = -2;

    public int pointValue;
    public ParticleSystem explosionParticle;
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
        targetAudio = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            if(targetAudio!=null && targetDestroyClips.Length > 0)
            {
                int clipIndex = Random.Range(0, targetDestroyClips.Length);
                AudioSource.PlayClipAtPoint(targetDestroyClips[clipIndex],transform.position);
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.DecrementLives();
            }
        }
        if (other.CompareTag("MouseTrail"))
        {
            if (gameManager.isGameActive)
            {
                if (targetAudio != null && targetDestroyClips.Length > 0)
                {
                    int clipIndex = Random.Range(0, targetDestroyClips.Length);
                    AudioSource.PlayClipAtPoint(targetDestroyClips[clipIndex], transform.position);
                }
                
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(pointValue);
            }
        }
        
        Destroy(gameObject);
    }
    Vector3 RandomForce()
    {
        return Vector3.up *Random.Range(minForceAmplitude, maxForceAmplitude);
    }
    float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }

}
