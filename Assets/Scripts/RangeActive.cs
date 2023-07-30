using UnityEngine;

public class RangeActive : MonoBehaviour
{
    [SerializeField] private Transform activePos;
    [SerializeField] private float activeRange;
    [SerializeField] private LayerMask entityLayer;

    private void Start()
    {
        ChangeEnableScripts(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(activePos.position, activeRange);
    }

    void Update()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(activePos.position, activeRange, entityLayer);
        if (monsters.Length < 0.8)
        {
            ChangeEnableScripts(false);
            return;
        }
        ChangeEnableScripts(true);
    }

    private void ChangeEnableScripts(bool isEnable)
    {
        Units[] scripts = gameObject.GetComponentsInChildren<Units>();

        foreach (Units script in scripts)
        {
            script.enabled = isEnable;
        }
    }
}
