using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed, detectRange, destinationRange, nextWaypointDistance, distanceToPlayer;
    [SerializeField] Path path;
    [SerializeField] Animator anim;

    [SerializeField] float pathDirectionFactor;
    [SerializeField] float moveDelay;

    public Vector2 spawnPos;
    int currentWaypoint;

    Seeker seeker;
    Rigidbody2D rb;

    bool playerNearby;

    void OnEnable()
    {
        player = FindObjectOfType<BotController>().transform;

        currentWaypoint = 0;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, moveDelay);
    }

    void FixedUpdate()
    {
        distanceToPlayer =  Vector2.Distance(rb.position, player.position);

        if (distanceToPlayer <= detectRange)
        {
            playerNearby = true;
        }
        else
        {
            playerNearby = false;
        }

        Move();

        if (Vector2.Distance(transform.position, spawnPos) < 0.01f)
        {
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("walking", true);
        }
    }

    void Move()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized * pathDirectionFactor;

        Debug.Log($"Direction value:{direction}");

        Vector2 force = direction * speed * Time.deltaTime * 50f;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    private void UpdatePath()
    {
        if (playerNearby)
        {
            LeanTween.cancel(gameObject);

            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, player.position, OnPathComplete);

                LeanTween.delayedCall(0.5f, () =>
                {
                    gameObject.GetComponent<Enemy>().Shoot();
                });
            }
        }
        else
        {
            //LeanTween.move(gameObject, spawnPos, Vector2.Distance(gameObject.transform.position, spawnPos) * 3f);

            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, spawnPos, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
