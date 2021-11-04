namespace Assets.Scripts.Interfaces
{
    public interface IKnockbackable<T>
    {
        void TakeKnockBack(T other, float force);
    }
}

