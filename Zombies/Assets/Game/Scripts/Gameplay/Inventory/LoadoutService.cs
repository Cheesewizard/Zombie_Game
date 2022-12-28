namespace Game.Scripts.Gameplay.Guns
{
    public class LoadoutService : ILoadoutService
    {
        public Loadout Loadout { get; }
    }

    public interface ILoadoutService
    {
         Loadout Loadout { get; }
    }
}