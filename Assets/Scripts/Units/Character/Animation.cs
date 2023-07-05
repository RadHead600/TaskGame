using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;
    private bool isRun;


    public bool IsRun
    {
        get => isRun;
        set => isRun = value;
    }

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
        if (isRun)
            State = CharState.Run;
        else
            State = CharState.Idle;
    }

}
