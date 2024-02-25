using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce;
    public float jumpWait;
    public float speed;
    public float speedUp;
    public float timerInterval;
    public float Lane1;
    public float Lane2;
    public float Lane3;
    private float moveSpeed;
    private float Lane;
    private float timer = 0.0f;

    private bool Ln1, Ln2, Ln3;
    private bool isJumping;
    private bool jumpDelay;
    private bool isSpeedUp;

    Animator anim;
    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Lane = Lane2;
        Ln2 = true;
        StartCoroutine(Started());
    }

    void Update()
    {
        Debug.Log(moveSpeed);

        if(isSpeedUp)
            timer += Time.deltaTime;

        if (timer >= timerInterval)
        {
            moveSpeed += speedUp;
            timer = 0;
        }
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);

        if (transform.position.x == -2.5f)
        {
            Ln1 = true;
            Ln2 = false;
            Ln3 = false;
        }
        if (transform.position.x == 2.5f)
        {
            Ln1 = false;
            Ln2 = true;
            Ln3 = false;
        }
        if (transform.position.x == 7.5f) 
        {
            Ln1 = false;
            Ln2 = false;
            Ln3 = true;
        }
        if (Input.GetKeyDown(KeyCode.A) ) 
        {
            if (Ln2)
                Lane = Lane1;
            if (Ln1)
                Lane = Lane1;
            if (Ln3)
                Lane = Lane2;
        }

        if (Input.GetKeyDown(KeyCode.D) ) 
        {
            if (Ln2)
                Lane = Lane3;
            if (Ln1)
                Lane = Lane2;
            if (Ln3)
                Lane = Lane3;
        }

        if (Input.GetKeyDown(KeyCode.W) && !isJumping) 
        {
            isJumping = true;
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            StartCoroutine(JumpDelay());
        }

        if (isJumping && jumpDelay) 
        {
            isJumping = false;
            jumpDelay = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("Roll");
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Lane, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpWait);

        jumpDelay = true;
    }

    IEnumerator Started()
    {
        yield return new WaitForSeconds(4);

        isSpeedUp = true;

        moveSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            StartCoroutine(Hit());
        }
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            Player.CoinLock = true;
        }

    }
  
    IEnumerator Hit()
    {
        Vector3 position = transform.position;
        
        if (Ln1)
        {
            Lane = Lane1;
            transform.position = new Vector3(-2.5f, position.y, position.z - 3);
        }
        if (Ln2)
        {
            Lane = Lane2;
            transform.position = new Vector3(2.5f, position.y, position.z-3);
        }
        if (Ln3)
        {
            Lane = Lane3;
            transform.position = new Vector3(7.5f, position.y, position.z - 3);
        }

        float existSpeed = moveSpeed;

        yield return new WaitForSeconds(0.1f);

        moveSpeed = 0;
        Player.HeartLock = true;
        anim.SetTrigger("Stun");

        StartCoroutine(CollOff());
        
        yield return new WaitForSeconds(1);

        moveSpeed = existSpeed;
      
    }

    IEnumerator CollOff()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in objects)
        {
            foreach (Collider collider in obj.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }

        yield return new WaitForSeconds(3);

        foreach (GameObject obj in objects)
        {
            foreach (Collider collider in obj.GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
        }
    }
}
