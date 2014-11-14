using UnityEngine;
using System.Collections;

public class UfoAi : MonoBehaviour {

    public enum UFOState
    {
        None = 0,
        MoveTowards,
        CircleAndShoot,
    }

    public float speed;
    public float circleSpeed;
    public int playerDistance;
    private UFOState state;
    private GameObject player;

    public float rotationSpeed = 30f;
    public float wobbleSpeed = 2f;
    public float wobbleMagnitude = 0.001f;

    //Use this for initialization
    void Start()
    {
        speed = 5;
        circleSpeed = 30;
        playerDistance = 10;
        state = UFOState.MoveTowards;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case UFOState.MoveTowards: UpdateMoveState(); break;
            case UFOState.CircleAndShoot: UpdateCircleAndShootState(); break;
        }

        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, Mathf.Sin(Time.realtimeSinceStartup * wobbleSpeed)) * wobbleMagnitude);
    }

    private void UpdateCircleAndShootState()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > playerDistance + 2)
        {
            print("He got away!");
            state = UFOState.MoveTowards;
            rotationSpeed /= 2;
        }

        CircleAroundPlayer();
    }

    private void UpdateMoveState()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= playerDistance)
        {
            print("It't the enemy!");
            state = UFOState.CircleAndShoot;
            rotationSpeed *= 2;
        }

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void CircleAroundPlayer()
    {
        this.transform.RotateAround(player.transform.position, Vector3.up, circleSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, player.transform.position) <= playerDistance - 2)
        {
            //this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            rigidbody.AddForce((this.transform.position - player.transform.position) * Time.deltaTime * 300);
        }
    }
}
