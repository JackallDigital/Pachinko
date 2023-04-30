using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CountPachinkoBalls : MonoBehaviour {
    SpawnPachinkoBalls ballsSpawning;

    private int smallPoints = 7;
    private int mediumPoints = 5;
    private int largePoints = 3;

    private int smallBallCounter;
    private int mediumBallCounter;
    private int largeBallCounter;
    private int score;
    private int[] endScore = new int[5];
    private int tempScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        ballsSpawning = SpawnPachinkoBalls.Instance;
        smallBallCounter = 0;
        mediumBallCounter = 0;
        largeBallCounter = 0;
        score = 0;

        tempScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballsSpawning.reset) {
            StartCoroutine(ResetValues());
        }
        if (ballsSpawning.destroyMax == ballsSpawning.spawnNumberOfBalls) {
            ballsSpawning.playingPachinko = false;
            PachinkoResult();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BallSmall")) {
            smallBallCounter++;
            score += smallPoints;
        }
        else if (other.CompareTag("BallMedium")) {
            mediumBallCounter++;
            score += mediumPoints;
        }
        else if (other.CompareTag("BallLarge")) {
            largeBallCounter++;
            score += largePoints;
        }

        ballsSpawning.destroyMax++;
        Destroy(other.gameObject);

        switch (gameObject.name) {
            case "Box":
                ballsSpawning.textBallCounter[0].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
                ballsSpawning.textScore[0].SetText("SCORE" + "\n" + score);
                break;
            case "Box (1)":
                ballsSpawning.textBallCounter[1].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
                ballsSpawning.textScore[1].SetText("SCORE" + "\n" + score);
                break;
            case "Box (2)":
                ballsSpawning.textBallCounter[2].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
                ballsSpawning.textScore[2].SetText("SCORE" + "\n" + score);
                break;
            case "Box (3)":
                ballsSpawning.textBallCounter[3].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
                ballsSpawning.textScore[3].SetText("SCORE" + "\n" + score);
                break;
            case "Box (4)":
                ballsSpawning.textBallCounter[4].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
                ballsSpawning.textScore[4].SetText("SCORE" + "\n" + score);
                break;
        }
    }
    
    void PachinkoResult() {
        bool hasWon = false;
        ballsSpawning.getScore();

        
        foreach(int i in ballsSpawning.endScore) {
            if (i > 0) {
                if (i == 69) {
                    hasWon = true;
                }
                else if (i == 100) {
                    hasWon = true;
                }
                tempScore += i;
                //Debug.Log("Temp i " + i);
            }
        }
        if (tempScore > 500) {
            hasWon = true;
        }
        
        if (hasWon) {
            //Debug.Log("Winner Winner Chick 4 Dinner");
            ballsSpawning.background.enabled = true;
            ballsSpawning.textWin.enabled = true;
        }
        else {
            //Debug.Log("Bust. Give it another shot!");
            ballsSpawning.background.enabled = true;
            ballsSpawning.textLose.enabled = true;
        }
        tempScore = 0;
    }
        
    
    IEnumerator ResetValues() {
        for (int i = 0; i < 5; i++) {
            if (score > 0) {
                endScore[i] = score;
            }
            smallBallCounter = 0;
            mediumBallCounter = 0;
            largeBallCounter = 0;
            score = 0;

            ballsSpawning.textBallCounter[i].text = "S - " + smallBallCounter + "\n" + "M - " + mediumBallCounter + "\n" + "L - " + largeBallCounter;
            ballsSpawning.textScore[i].text = "SCORE" + "\n" + score;
        }

        yield return new WaitForSeconds(0.2f);
        ballsSpawning.reset = false;
    }
}
