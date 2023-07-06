using UnityEngine;

public abstract class Enemy : Units
{
    [SerializeField] protected GameObject player;
    [SerializeField] private EnemyParameters parameters;
    [SerializeField] private DropParameters dropParameters;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private ParticleSystem bloodParticles;

    public EnemyParameters Parameters => parameters;

    public override int ReceiveDamage(int damage)
    {
        // Отвечает за отталкивание в при попадании
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.up * 2.0F, ForceMode2D.Impulse);

        // Отвечает за создания партикла крови
        ParticleSystem particle = Instantiate(bloodParticles, transform.position, transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, particle.startLifetime);

        if (HealthPoints - damage <= 0)
        {
            Die();
            return 0;
        }
        return HealthPoints -= damage;
    }

    public override void Die()
    {
        SaveParameters.numberKilled[SaveParameters.levelActive] += 1;
        Instantiate(dropParameters.Coin, transform.position + new Vector3(0, 1, 0), dropParameters.Coin.transform.rotation); // создает монету на месте смерти
        Destroy(gameObject);
    }
}
