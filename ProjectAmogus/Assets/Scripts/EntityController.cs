using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EntityController : MonoBehaviour
{
    [SerializeField] bool canControl = false;
    Rigidbody targetRB;

    [SerializeField] private LayerMask wallMask;
    [SerializeField] Transform leftPoint, forwardPoint, rightPoint;
    Vector3 original_leftPoint_dir, original_forwardPoint_dir, original_rightPoint_dir;


    private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        original_forwardPoint_dir = (forwardPoint.position - transform.position).normalized;
        original_leftPoint_dir = (leftPoint.position - transform.position).normalized;
        original_rightPoint_dir = (rightPoint.position - transform.position).normalized;

    }

    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRB != null && canControl)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, 0.01f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                targetRB.AddForce(new Vector3(-0.01f, 0.0f, 0.0f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, -0.01f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                targetRB.AddForce(new Vector3(0.01f, 0.0f, 0.0f), ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //movePositionTransform.transform.position = hit.point;
                navMeshAgent.SetDestination(hit.point);
                
            }
        }

        //  navMeshAgent.destination = movePositionTransform.position;
    }

    private void FixedUpdate()
    {


        CheckSide(3.5f, leftPoint, original_leftPoint_dir);
        CheckSide(3.5f, rightPoint, original_rightPoint_dir);
        CheckSide(3.5f, forwardPoint, original_forwardPoint_dir);

    }

    public void CheckSide(float distance, Transform point, Vector3 originalDirection)
    {
        Vector3 direction = (point.transform.position - this.transform.position).normalized;



        if (Physics.Raycast(transform.position, direction * distance, out RaycastHit hit, distance, wallMask))
        {
            //Debug.Log("hit");
            point.transform.position = hit.point;
            Debug.DrawLine(this.transform.position, hit.point - direction*2);


        }
        else
        {
            Debug.DrawLine(this.transform.position, transform.position + originalDirection * distance);

            point.position = transform.position + originalDirection * distance;
        }

    }
}
