﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : Character
{
    public float speed;
    public float distanceToCollider;
    public LayerMask collisionLayer;

    private float horizontalInput;

    protected override void Initializtion()
    {
        base.Initializtion();
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !character.takingDamage )
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalInput = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
        if(horizontalInput != 0 )
        {
            //anim.SetBool("Idle", false);
            //anim.SetBool("Walking", true);
            if(character.isGrounded && !character.isDead) animMamager.SetAnimation(AnimateManager.TypeAnim.RUN);
            if (horizontalInput > 0 && character.isFacingLeft)
            {
                character.isFacingLeft = false;
                Flip();
            }
            if(horizontalInput < 0 && !character.isFacingLeft)
            {
                character.isFacingLeft = true;
                Flip();
            }
        }
        else
        {
            //anim.SetBool("Idle", true);
            //anim.SetBool("Walking", false);
            if (character.isGrounded && !character.isDead) animMamager.SetAnimation(AnimateManager.TypeAnim.IDEL);
        }
        SpeedModifier();
    }

    private void LateUpdate()
    {
        CheckForPlatform();
    }

    private void CheckForPlatform()
    {
        RaycastHit2D ray = Physics2D.CapsuleCast(new Vector2(col.bounds.center.x, col.bounds.min.y - .5f), new Vector2(col.bounds.size.x, .5f), CapsuleDirection2D.Vertical, 0, Vector2.down, .25f);
        if(ray.collider != null && ray.collider.GetComponent<MoveablePlatform>())
        {
            transform.parent = ray.collider.gameObject.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void SpeedModifier()
    {
        if((rb.velocity.x > 0 && CollisionCheck(Vector2.right, distanceToCollider, collisionLayer)) || (rb.velocity.x < 0 && CollisionCheck(Vector2.left, distanceToCollider, collisionLayer)) && !character.isGrounded)
        {
            rb.velocity = new Vector2(.01f, rb.velocity.y);
        }
    }
}
