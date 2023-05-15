using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed; //dev can set in inspector view
    float hAxis; //Horizontal
    float vAxis; //Vertical
    bool shiftDown; // 3-1. Shift key down
    bool spaceDown; //5-1 Space bar key down
    bool interactionDown;

    bool isJump;

    Vector3 moveVec;

    Rigidbody rigid; // 5-3 for physical effect
    Animator anim; // 2-1. Create Animator var

    GameObject nearObject;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // 5-4 
        anim = GetComponentInChildren<Animator>(); // 2-2. init Animator var
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Interaction();
        
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shiftDown = Input.GetButton("Run");
        spaceDown = Input.GetButton("Jump");
        interactionDown = Input.GetButtonDown("Interaction");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //don't use y-axis
        //.normalized: no matter direction

        transform.position += moveVec * speed * (shiftDown ? 2f : 1f) * Time.deltaTime;

        anim.SetBool("Walk", moveVec != Vector3.zero); //if moveVec is not zero(stop), Walk is True => execute Walk Anim 
        anim.SetBool("Run", shiftDown); // 3-2. Run

    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // 4-1. look at 
    }

    void Jump()
    {
        if (spaceDown && !isJump){
            rigid.AddForce(Vector3.up * 10,  ForceMode.Impulse);
            anim.SetBool("Jump", true);
            anim.SetTrigger("Jump");
            isJump = true;
        }
    }

    void Interaction()
    {
        if(interactionDown && nearObject != null){
            if(nearObject.tag == "NPC"){
                NPC npc = nearObject.GetComponent<NPC>();
                npc.Enter(this);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor"){
            isJump = false;
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "NPC")
            nearObject = other.gameObject;
        Debug.Log(nearObject.name);
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "NPC"){
            NPC npc = nearObject.GetComponent<NPC>();
            npc.Exit();
            nearObject = null;
        }
    }
}
