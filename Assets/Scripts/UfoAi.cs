using UnityEngine;
using System.Collections;

public class UfoAi : MonoBehaviour {

    public enum UFOState
    {
        None = 0,
        MoveTowards,
        CircleAndShoot,
    }

    public float speed = 20f;
    public float maxSpeed = 5f;
    public float circleSpeed = 20f;
    public float maxCircleSpeed = 5f;
    public int movingDistance = 12;
    public float rotationSpeed = 30f;
    public float shootingDistance = 15f;
    public float fireRate = 1.5f;

    public GameObject missile;

    private UFOState state;
    private GameObject player;
    private int rotationDirection;
    private float nextFire;

    //Use this for initialization
    void Start()
    {
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

        if (player && Vector3.Distance(this.transform.position, player.transform.position) > 1000)
        {
            Destroy(this.gameObject);
        }

        if (player && Vector3.Distance(this.transform.position, player.transform.position) < shootingDistance && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rotationVector = player.transform.position - this.transform.position;
            GameObject shot = Instantiate(missile, this.transform.position, Quaternion.LookRotation(rotationVector)) as GameObject;
            Physics.IgnoreCollision(shot.collider, this.collider, true);
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
        if (Vector3.Distance(this.transform.position, player.transform.position) > movingDistance + 5)
        {
            state = UFOState.MoveTowards;
        }

        CircleAroundPlayer();
    }

    private void UpdateMoveState()
    {
        if (player && Vector3.Distance(this.transform.position, player.transform.position) <= Random.Range(movingDistance - 5, movingDistance + 5))
        {
            state = UFOState.CircleAndShoot;

            //determine rotation direction
            if (Random.value > 0.5)
                rotationDirection *= -1;

            //stop approach
            rigidbody.drag = 1000;
            //rotationSpeed *= 2;
        }

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
		if (player) {
			rigidbody.AddForce((player.transform.position - this.transform.position) * Time.deltaTime * speed);
		}

        //maxSpeed
        if (rigidbody.velocity.magnitude > maxSpeed)
            rigidbody.velocity *= 0.8f;
    }

    private void CircleAroundPlayer()
    {
        //this.transform.RotateAround(player.transform.position, Vector3.up, circleSpeed * Time.deltaTime);
        rigidbody.drag = 0.5f;
        Vector3 perpandicular = Vector3.Cross(player.transform.position - this.transform.position, Vector3.up);
        rigidbody.AddForce(perpandicular * Time.deltaTime * circleSpeed * rotationDirection);

        //maxSpeed
        if (rigidbody.velocity.magnitude > maxCircleSpeed)
            rigidbody.velocity *= 0.8f;

        if (Vector3.Distance(this.transform.position, player.transform.position) <= movingDistance - 2)
        {
            //this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            rigidbody.AddForce((this.transform.position - player.transform.position) * Time.deltaTime * speed * 2);
        }
        else if (Vector3.Distance(this.transform.position, player.transform.position) >= movingDistance + 2)
        {
            rigidbody.AddForce((player.transform.position - this.transform.position) * Time.deltaTime * speed * 2);
        }

    }

	void OnDestroy()
	{
		GameObject audioPlayer = GameObject.FindGameObjectWithTag("Sound");
		audioPlayer.GetComponents<AudioSource> () [0].Play ();
	}
}
