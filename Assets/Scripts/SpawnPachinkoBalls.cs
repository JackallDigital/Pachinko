using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class SpawnPachinkoBalls : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnPachinkoBall;
    [SerializeField] public TextMeshProUGUI[] textBallCounter;
    [SerializeField] public TextMeshProUGUI[] textScore;
    [SerializeField] public TextMeshProUGUI textWin;
    [SerializeField] public TextMeshProUGUI textLose;
    [SerializeField] public Image background;

    private float spawnPosX = 9f;
    private float spawnPosY = 13f;

    public bool playingPachinko = false;
    public int spawnNumberOfBalls = 100;
    public int destroyMax = 0;
    public int[] endScore= new int[5];

    public bool reset = false;

    public static SpawnPachinkoBalls Instance;
    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        destroyMax = 0;
        textWin.enabled = false;
        textLose.enabled = false;
        background.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playingPachinko && Input.GetMouseButtonDown(0)) {
            playingPachinko = true;
            destroyMax = 0;

            reset = true;
            textWin.enabled = false;
            textLose.enabled = false;
            background.enabled = false;
            StartCoroutine(SpawnBalls());
        }
    }

    IEnumerator SpawnBalls() {
        Vector3 spawnPos;
        yield return new WaitForSeconds(0.5f);

        for (int i=0; i <spawnNumberOfBalls; i++) {
            spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), spawnPosY, -0.25f);
            Instantiate(SpawnPachinkoBall[Random.Range(0, SpawnPachinkoBall.Length)], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void getScore() {
        for(int i=0; i< textScore.Length; i++) {
            endScore[i] = int.Parse(textScore[i].text.Substring(textScore[i].textInfo.lineInfo[1].firstCharacterIndex));
        }
    }
}
