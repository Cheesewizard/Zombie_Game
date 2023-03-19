using Game.Scripts.Gameplay.Inventory;

namespace Game.Scripts.Gameplay.Services
{
    public interface IPlayerLoadoutAccessService
    {
        Loadout loadout { get; }
    }
}