using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;
    public Asteroid asteroid;
    public float spawnRate = 0.5f;
    private float spawnCoolDown;
    public Camera camera;
    public int asteroidCount = 0;
    public int maxAsteroidCount = 200;
    // Start is called before the first frame update
    void Start()
    {
        spawnCoolDown = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCoolDown <= 0.0f && asteroidCount < maxAsteroidCount) {
            spawnAsteroid();
            spawnCoolDown = spawnRate;
        }
        spawnCoolDown -= Time.deltaTime;
    }

    void spawnAsteroid() {
        Asteroid roid = Instantiate(asteroid);
        roid.gm = this;
        roid.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Camera.main.nearClipPlane + 15.0f));
        asteroidCount++;
    }

    public void addScore(int points) {
        score += points;
        scoreText.text = "Score: "+score;
    }
}
