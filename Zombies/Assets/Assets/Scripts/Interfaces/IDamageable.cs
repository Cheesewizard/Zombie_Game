namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        float DamageTakenMultiplier { get; }
        float CriticalChancePercentage { get; }
    }

    public interface IDamageDealer
    {
        float Damage { get; }
        float CriticalDamagePercentage { get; }
        float CriticalChancePercentage { get; }
    }
}
