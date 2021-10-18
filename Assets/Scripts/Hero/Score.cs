using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private const float DISTANCE_REQUIRED_FOR_1_POINT = 1f;
    private Vector3 previousPlayerPosition;
    private float distanceSinceLastScore;

    public Transform player;
    private float score = 0f;
    public Text scoreText;

    void Awake()
    {
        //At the start save the start position.
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        //Calculate the distance travvelled since last update.
        distanceSinceLastScore += player.position.x - previousPlayerPosition.x;
        //Set the previous position the current.
        previousPlayerPosition = player.position;

        int scoreToAdd = 0;
        while (distanceSinceLastScore > DISTANCE_REQUIRED_FOR_1_POINT)
        {
            //Calculate how many points to add in this update. (Likely always 1 or none)
            ++scoreToAdd;
            distanceSinceLastScore -= DISTANCE_REQUIRED_FOR_1_POINT;
        }

        if (scoreToAdd > 0)
        {
            score += scoreToAdd;//Add to the total score.           
            scoreText.text = score.ToString();//refresh the text display.
        }
    }
}
