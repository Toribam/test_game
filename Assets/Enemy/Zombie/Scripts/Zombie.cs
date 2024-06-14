using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동하게 한다.
//상태를 만들어서 제어하고 싶다.
// Idle : Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
//Run : 타겟방향으로 이동(요원)
//Attack : 일정 시간마다 공격

public class Zombie : MonoBehaviour
{

    [Header("Zombie Status")]
    [Tooltip("Zombie Walk Speed")]
    public float ZombieSpeed = 1.5f;
    [Tooltip("Zombie Search Range")]
    public float SearchRange = 5.0f;
    [Tooltip("Zombie Attack Range")]
    public float AttackRange = 2.0f;
    // 타겟
    public Transform target;
    // NavMeshAgent 활성화
    NavMeshAgent agent;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 좀비에 NavMeshAgent 활성화
        agent = GetComponent<NavMeshAgent>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        OnIdle();
        OnWalk();
        OnAttack();
    }

    private void OnAttack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= AttackRange)
        {
            anim.SetBool("IsAttack", true);
            anim.SetTrigger("Attack");
        }
        agent.speed = 0;
    }
    
    private void OnWalk()
    {
        // agent.velocity = ZombieSpeed;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > AttackRange)
        {
            anim.SetBool("IsAttack", false);
            anim.SetTrigger("Walk");
        }
        agent.speed = ZombieSpeed;
        agent.destination = target.transform.position; // 타겟을 향해 이동
    }

    private void OnIdle()
    {
        // 플레이어를 찾고 타겟으로 설정
        target = GameObject.Find("Player").transform;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        
        if (distance > SearchRange)
        {
            anim.SetBool("IsAttack", false);
            anim.SetTrigger("Idle");
        }
        // agent.velocity = Vector3.zero; // Idle 상태에서는 이동 멈추기
        agent.speed = 0;
    }
}