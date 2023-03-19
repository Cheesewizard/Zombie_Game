using Game.Scripts.Gameplay.Player;

namespace Game.Scripts.Gameplay.Services
{
	public interface IPlayerWeaponBehaviourAccessService
	{
		AbstractPlayerWeaponBehaviour WeaponBehaviour { get; }
	}
}