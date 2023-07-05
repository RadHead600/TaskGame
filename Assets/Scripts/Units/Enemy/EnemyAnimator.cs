using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            State = CharState.Run;
        else
            State = CharState.Idle;
    }
}
public enum CharState
{
    Idle,
    Run
}
