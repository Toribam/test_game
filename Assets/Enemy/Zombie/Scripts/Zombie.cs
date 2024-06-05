using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{   
    // target
    public Transform target;
    private NavMeshAgent agent;
    // Zombie

    public Animator anim;

    // enum status
    enum State
    {
        Idle,
        Run,
        Attack
    }

    // State state
    State state;

    // Start is called before the first frame update
    void Start()
    {
        // idle state from start
        state = State.Idle;

        // identify agent
        agent = GetComponent<NavMeshAgent>();

        // find target(player) only once at start
        target = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    private void UpdateAttack()
    {
        agent.speed = 0;
        // No need to check distance and change state if already attacking
    }

    private void UpdateRun()
    {
        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
        agent.destination = target.position;

        //남은 거리가 2미터 이하면 공격 상태로 전환
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateIdle()
    {
        agent.speed = 0;
        // 이미 타겟이 지정되어 있으면 Run 상태로 전환
        if (target != null)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }
}

