using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    private GameController gameController;
    public GameObject stars;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void Update()
    {
        if (gameController.audioSource.clip == gameController.audioClip2)
        {
            Destroy(stars);
        }
    }
}
