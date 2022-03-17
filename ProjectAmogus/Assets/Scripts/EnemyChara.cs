using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChara : Entity
{
    [SerializeField] EntityType whatType;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip slashyslashSlash;

    bool isAsleep = true;
    float aiTimer = 5.0f;
    bool canAttack = true;
    float atkTimer = 2.0f;

    bool canDither = true;
    float ditherTimer = 0.0f;
    Vector3 moveDir = new Vector3(0.0f, 0.0f, 0.0f);

    Transform playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<EntityController>().gameObject.transform;

        switch (whatType)
        {
            case EntityType.Swarm:
                {
                    MaxHealth = 60.0f;
                    Health = MaxHealth;
                    MoveSpeed = 3.0f;
                    TurnSpeed = 3.0f;
                    Priority = AIPriority.AttackClosest;
                }
                break;
            case EntityType.Monitor:
                {
                    MaxHealth = 180.0f;
                    Health = MaxHealth;
                    MoveSpeed = 5.0f;
                    TurnSpeed = 5.0f;
                    Priority = AIPriority.AttackPlayer;
                }
                break;
            case EntityType.SuperMonitor:
                {
                    MaxHealth = 300.0f;
                    Health = MaxHealth;
                    MoveSpeed = 7.0f;
                    TurnSpeed = 7.0f;
                    Priority = AIPriority.AttackPlayer;
                }
                break;
            default:
                break;
        }
    }

    public void WakeEnemyUp()
    {
        isAsleep = false;
    }

    public void MakeEnemySleep()
    {
        isAsleep = true;
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        WakeEnemyUp();
    }

    public override void SetDowned()
    {
        status = StatusMode.Downed;

        if (explosionPrefab != null)
        {
            var expPFX = Instantiate(explosionPrefab);
            expPFX.transform.position = this.transform.position;
        }

        Debug.Log(this.gameObject.name + "is Downed!");
        Object.Destroy(gameObject);
    }

    void AttackClosest()
    {
        //Prioritizing Target

        var targets = FindObjectsOfType<PlayerChara>();
        List<PlayerChara> validTargets = new List<PlayerChara>();

        foreach (PlayerChara pChar in targets)
        {
            if (pChar.Status != StatusMode.Downed) validTargets.Add(pChar);
        }

        Transform nearest = targets[0].transform;

        foreach (PlayerChara pchar in validTargets)
        {
            var temp = Vector3.Distance(this.transform.position, pchar.transform.position);
            if (temp < Vector3.Distance(this.transform.position, nearest.position)) nearest = pchar.transform;
        }

        var dist = Vector3.Distance(this.transform.position, nearest.position);

        if (dist > 0.0f && dist < 10.0f)
        {
            //Approach
            this.transform.position = Vector3.MoveTowards(this.transform.position, nearest.position, MoveSpeed * 2.0f * Time.deltaTime);

            if (canAttack)
            {
                if (dist <= 3.0f)
                {
                    //Melee Attack
                    Vector3 direction = (nearest.position - transform.position).normalized;
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;
                    Debug.DrawRay(transform.position, direction, Color.red);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Trooper"))
                        {
                            if (projectilePrefab != null)
                            {
                                var projPFX = Instantiate(projectilePrefab);
                                projPFX.transform.position = hit.collider.gameObject.transform.position;

                                if (slashyslashSlash != null)
                                {
                                    var audioSource = FindObjectOfType<AudioSource>();
                                    if (audioSource != null) audioSource.PlayOneShot(slashyslashSlash);


                                }
                            }

                            hit.collider.gameObject.GetComponent<Entity>().Damage(10.0f);
                            hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(direction * 100.0f);
                            canAttack = false;
                        }
                    }
                }
            }
            else
            {
                //Reset Attack
                atkTimer -= Time.deltaTime;
                if (atkTimer <= 0.0f)
                {
                    canAttack = true;
                    atkTimer = 2.0f;
                }
            }
        }
        else
        {
            //Disengage
            aiTimer -= Time.deltaTime;
            if (aiTimer <= 0.0f)
            {
                isAsleep = true;
                aiTimer = 5.0f;
            }
        }
    }

    void AttackPlayer()
    {
        var dist = Vector3.Distance(this.transform.position, playerRef.position);

        if (dist > 0.0f && dist < 10.0f)
        {
            //Approach
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerRef.position, MoveSpeed * 2.0f * Time.deltaTime);
        }
        else if (dist <= 1.0f)
        {
            //Attack Range
            if (canAttack)
            {
                if (dist <= 3.0f)
                {
                    //Melee Attack
                    Vector3 direction = (playerRef.position - transform.position).normalized;
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;
                    Debug.DrawRay(transform.position, direction, Color.red);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Trooper"))
                        {
                            hit.collider.gameObject.GetComponent<Entity>().Damage(30.0f);
                            hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(direction * 100.0f);
                            canAttack = false;
                        }
                    }
                }
            }
            else
            {
                atkTimer -= Time.deltaTime;
                if (atkTimer <= 0.0f)
                {
                    canAttack = true;
                    atkTimer = 2.0f;
                }
            }
        }
        else
        {
            //Disengage
            aiTimer -= Time.deltaTime;
            if (aiTimer < 0.0f)
            {
                isAsleep = true;
                aiTimer = 5.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAsleep)
        {
            switch (Priority)
            {
                case AIPriority.AttackClosest:
                    AttackClosest();
                    break;
                case AIPriority.AttackPlayer:
                    AttackPlayer();
                    break;
            }
        }
        else
        {
            if (canDither)
            {
                this.transform.Translate(new Vector3(moveDir.x, 0.0f, moveDir.y) * moveSpeed * Time.deltaTime);
            }

            //Dither Movement
            ditherTimer -= Time.deltaTime;
            if (ditherTimer < 0.0f)
            {
                moveDir = Random.insideUnitSphere;
                ditherTimer = Random.Range(0.1f, 0.2f);
                canDither = !canDither;
            }
        }
    }
}
