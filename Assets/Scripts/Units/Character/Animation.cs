using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animation : MonoBehaviour
{
    private Animator _animator;
    private bool _isRun;


    private const string _animatorName = "State";

    public bool IsRun
    {
        get => _isRun;
        set => _isRun = value;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public CharState State
    {
        get { return (CharState)_animator.GetInteger(_animatorName); }
        set { _animator.SetInteger(_animatorName, (int)value); }
    }

    private void Update()
    {
        if (_isRun)
            State = CharState.Run;
        else
            State = CharState.Idle;
    }

}
