using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    // asteroid reference
    public GameObject asteroid;
    private GameObject[] childAsteroids = new GameObject[1];// size should be 24 for all 24 cubes in the asteroid
    public List<GameObject> bulletPool;
    // counter for shoot sound array
    private int k = 0;

    // score text reference
    public Text scoreText;

    // score reference
    public int score = 0;

    // health slider
    public Slider healthSlider;
    //private int health = 100; // should be set from class

    public GameObject earth;
    public GameObject bullet;
    public ParticleSystem burner;
    public AudioSource engineHum;
    public AudioSource gameSong;
    public AudioSource shootSoundOne;
    public AudioSource shootSoundTwo;
    public AudioSource shootSoundThree;
    private AudioSource[] shootSounds = new AudioSource[3];

    private Rigidbody rgShip;
    private Rigidbody rgEarth;
    private Rigidbody rgBullet;
    private GameObject[] bulletEmitterArray = new GameObject[2];
    private GameObject emitterOne;
    private GameObject emitterTwo;

    private Vector3 leftEnd = new Vector3(-1.111f, 0, 0);
    private Vector3 rightEnd = new Vector3(1.08f, 0, 0);
    private Vector3 topEnd = new Vector3(0, 0.5f, 0);
    private Vector3 bottomEnd = new Vector3(0, -0.4f, 0);

    void Start()
    {
        childAsteroids[0] = GameObject.Find("cube");
        if (childAsteroids[0] != null) {
            childAsteroids[0].transform.parent = null;
        }

        healthSlider.value = 0;
        Debug.Log(healthSlider.value);
        shootSounds[0] = shootSoundOne;
        shootSounds[1] = shootSoundTwo;
        shootSounds[2] = shootSoundThree;

        burner.Stop();

        rgShip = GetComponent<Rigidbody>();
        rgEarth = earth.GetComponent<Rigidbody>();

        emitterOne = GameObject.Find("bulletEmitterPointOne");
        emitterTwo = GameObject.Find("bulletEmitterPointTwo");

        bulletEmitterArray[0] = emitterOne;
        bulletEmitterArray[1] = emitterTwo;
    }


    void Update()
    {
        movement();

    }


    void movement()
    {

        if (Input.GetKey(KeyCode.A))
        {

            rgShip.AddTorque(0, 0, 0.01f);
            rgEarth.AddTorque(0, 0.01f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {

            rgShip.AddTorque(0, 0, -0.01f);
            rgEarth.AddTorque(0, -0.01f, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            burner.Play();
            Vector3 forceDirection = transform.right;
            rgShip.AddForce(forceDirection * 0.5f);
            rgEarth.AddTorque(0.02f, 0, 0);

        }
        if (Input.GetKey(KeyCode.S))
        {
            burner.Play();
            Vector3 forceDirection = -transform.right;
            rgShip.AddForce(forceDirection * 0.5f);
            rgEarth.AddTorque(-0.02f, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<GameObject> dynamicBulletQue = playShootSound();
            bulletDestroy(dynamicBulletQue);
            //Debug.Log(bulletPool);

            // leave here to test the score update
            /*
            score++;
            updateScore(score);
            */
        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            engineHum.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            engineHum.Play();
        }

        // check key ups 
        if (Input.GetKeyUp(KeyCode.W)) 
        {
            engineHum.Stop();
            burner.Stop();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            engineHum.Stop();
            burner.Stop();
        }

        // checl if ship went off bounds and bring back in bounds
        if (transform.position.x < leftEnd.x) {
            transform.position = new Vector3(rightEnd.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightEnd.x)
        {
            transform.position = new Vector3(leftEnd.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > topEnd.y)
        {
            Debug.Log("here 1");
            transform.position = new Vector3(transform.position.x, bottomEnd.y, transform.position.z);
        }
        if (transform.position.y < bottomEnd.y)
        {
            Debug.Log(transform.position.y);
            Debug.Log(bottomEnd.y);
            Debug.Log("here 2");
            transform.position = new Vector3(transform.position.x, topEnd.y, transform.position.z);
        }
    }

    private List<GameObject> playShootSound() {

        if (k >= 3) {
            k = 0;
        }

        if (k < 3) {
            shootSounds[k].Play();

            k++;
        }

        int randN = Random.Range(0, 2);
        int bulletVelocity = 100;   // change the constant here to set speed
        GameObject newB = Instantiate(bullet, bulletEmitterArray[randN].transform.position, bulletEmitterArray[randN].transform.rotation);

         // add bullet to bulletPool
         bulletPool.Add(newB);
        // add bullet physics
        rgBullet = newB.GetComponent<Rigidbody>();
        rgBullet.AddForce(transform.right * bulletVelocity);

        return (bulletPool);
    }
    private void loadShootAudio() {

    }
    private void setScore() {
        scoreText.text = "Score: 0";
    }

    private void updateScore(int score) {

        Debug.Log(score);
        scoreText.text = "Score: " + score.ToString();

    }

    private void bulletDestroy(List<GameObject> bulletPool) {

        // since we are not removing objects from the list when they have been destroyed, some entries will be null so we null check
        // to find the non null entries which will be the bullets fired and not yet destroyed
        for (int i = 0; i < bulletPool.Count; i++) {
            // check if bullet traveled off screen and delete
            if (bulletPool[i] != null)
            {

                if (bulletPool[i].transform.position.x < leftEnd.x)
                {
                    GameObject.DestroyImmediate(bulletPool[i], true);
                }
                else if (bulletPool[i].transform.position.x > rightEnd.x)
                {
                    GameObject.DestroyImmediate(bulletPool[i], true);
                }
                else if (bulletPool[i].transform.position.y > topEnd.y)
                {
                    GameObject.DestroyImmediate(bulletPool[i], true);
                }
                else if (bulletPool[i].transform.position.y < bottomEnd.y)
                {
                    GameObject.DestroyImmediate(bulletPool[i], true);
                }
            }
        }
    }

}