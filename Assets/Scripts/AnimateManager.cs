using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour
{

    public Animator animator;

    private string currentAnimaton;
    public enum TypeAnim  { IDEL, RUN, WALCK_BACK, JUMP, DOUBLE_JUMP, HIT, DEAD, MILLE_ATTACK, GRANATE_ATTACK}
    public TypeAnim CurrentTypeAnim =  TypeAnim.IDEL;
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_WALCK_BAKE = "Player_walck_bake";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_DOUBLE_JUMP = "Hero_Double_Jump";
    const string PLAYER_HIT = "Player_hit";
    const string PLAYER_DEAD = "Player_dead";
    const string PLAYER_MILLE_ATTACK = "Player_mille_attack";
    const string PLAYER_GRANATE_ATTACK = "Hero_Granate";





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationControl();

        

    }
    public void SetAnimation( TypeAnim anim)
    {
        CurrentTypeAnim = anim;
    }

    public void AnimationControl()
    {
        switch (CurrentTypeAnim)
        {
            case TypeAnim.IDEL:
                currentAnimaton = PLAYER_IDLE;
                break;

            case TypeAnim.RUN:
                currentAnimaton = PLAYER_RUN;
                break;

            case TypeAnim.WALCK_BACK:
                currentAnimaton = PLAYER_WALCK_BAKE;
                break;

            case TypeAnim.JUMP:
                currentAnimaton = PLAYER_JUMP;
                break;

            case TypeAnim.DOUBLE_JUMP:
                currentAnimaton = PLAYER_DOUBLE_JUMP;
                break;

            case TypeAnim.HIT:
                currentAnimaton = PLAYER_HIT;
                break;

            case TypeAnim.DEAD:
                currentAnimaton = PLAYER_DEAD;
                break;

            case TypeAnim.MILLE_ATTACK:
                currentAnimaton = PLAYER_MILLE_ATTACK;
                break;
            case TypeAnim.GRANATE_ATTACK:
                currentAnimaton = PLAYER_GRANATE_ATTACK;
                break;

        }


        ChangeAnimationState(currentAnimaton);

    }



    void ChangeAnimationState(string newAnimation)
    {
        //if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
