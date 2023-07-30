using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public CharState State
    {
        get { return (CharState)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
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
