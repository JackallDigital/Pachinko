using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndLaunch : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchPower;
    [SerializeField] float currentPower = 0f;
    private float maxPower = 100f;

    private float incrementPower = 50f;
    private float launchForce = 100f;

    public bool isCharging = false;
    private Rigidbody rb;
    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //ballPrefab = rb.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ball == null) {
            if (Input.GetMouseButtonDown(0)) {
                ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            }
        }
        if(Input.GetMouseButtonDown(0)){
            isCharging= true;
        }
        if (isCharging) {
            currentPower += incrementPower * Time.deltaTime;
            if(currentPower > maxPower) {
                currentPower = maxPower;
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            LaunchBall();
            isCharging= false;
            currentPower= 0f;
        }
    }

    private void FixedUpdate() {
        rb.AddForce(Vector3.down, ForceMode.Acceleration);
    }

    void LaunchBall() {
        Vector3 direction = GetLaunchDirection();
        float force = currentPower / maxPower * launchForce;
        //rb.AddForce(-direction * force, ForceMode.Impulse);
        ball.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        //ball.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    private Vector3 GetLaunchDirection() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = worldPos-transform.position;
        direction.Normalize();

        return direction;
    }
}
