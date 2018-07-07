using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject asteroidPrefab;
    public GameObject asteroidManager;
    public int velocity;

    // Use this for initialization
    void Start()
    {
        //asteroidPrefabRg = asteroidPrefab.
        spawnAsteroids();
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnAsteroids() {

        velocity = 1;

        // loop throught spawning objects in asteroid manager parent and get their location
        foreach (Transform child in asteroidManager.transform)
        {
            int rand = Random.Range(0, 2);
            float randRotZ = Random.Range(-50f, 50f);

            if (rand == 0) {
                GameObject astObj = GameObject.Instantiate(asteroidPrefab, child.position, new Quaternion(child.rotation.x, child.rotation.y, randRotZ, 1));
                Rigidbody rigidBody = astObj.GetComponent<Rigidbody>();
                rigidBody.AddForce(-astObj.transform.right * velocity);
            }
            else if (rand == 1) {
                GameObject astObj = GameObject.Instantiate(asteroidPrefab, child.position, new Quaternion(child.rotation.x, child.rotation.y, randRotZ, 1));
                Rigidbody rigidBody = astObj.GetComponent<Rigidbody>();
                rigidBody.AddForce(-astObj.transform.right * velocity);
            }
        }

    }
}
