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
    public float maxSpeed;
    public float circleSpeed;
    public float maxCircleSpeed;
    public int playerDistance;
    public float rotationSpeed = 30f;

    private UFOState state;
    private GameObject player;
    private int rotationDirection;

    //Use this for initialization
    void Start()
    {
        speed = 5;
        maxSpeed = 5;
        circleSpeed = 20;
        maxCircleSpeed = 5;
        playerDistance = 10;

        if (Random.value > 0.5)
            rotationDirection = 1;
        else
            rotationDirection = -1;

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

        if (Vector3.Distance(this.transform.position, player.transform.position) > 1000)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            GetComponent<Energy>().CurrentEnergy -= other.GetComponent<MissileScript>().DestructionForce;
            Destroy(other.gameObject);
        }
    }

    private void UpdateCircleAndShootState()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > playerDistance + 5)
        {
            print("He got away!");
            state = UFOState.MoveTowards;
            //rotationSpeed /= 2;
        }

        CircleAroundPlayer();
    }

    private void UpdateMoveState()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= Random.Range(playerDistance - 5, playerDistance + 5))
        {
            print("It't the enemy!");
            state = UFOState.CircleAndShoot;

            //determine rotation direction
            if (Random.value > 0.5)
                rotationDirection *= -1;

            //stop approach
            rigidbody.drag = 1500;
            //rotationSpeed *= 2;
        }

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        rigidbody.AddForce((player.transform.position - this.transform.position) * Time.deltaTime * speed);

        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity *= 0.9f;
    }

    private void CircleAroundPlayer()
    {
        //this.transform.RotateAround(player.transform.position, Vector3.up, circleSpeed * Time.deltaTime);
        rigidbody.drag = 0.5f;
        Vector3 perpandicular = Vector3.Cross(player.transform.position - this.transform.position, Vector3.up);
        rigidbody.AddForce(perpandicular * Time.deltaTime * circleSpeed * rotationDirection);

        if (rigidbody.velocity.magnitude > maxCircleSpeed)
            rigidbody.velocity *= 0.9f;

        if (Vector3.Distance(this.transform.position, player.transform.position) <= playerDistance - 2)
        {
            //this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            rigidbody.AddForce((this.transform.position - player.transform.position) * Time.deltaTime * speed * 2);
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) >= playerDistance + 2)
        {
            rigidbody.AddForce((player.transform.position - this.transform.position) * Time.deltaTime * speed * 2);
        }

    }
}
