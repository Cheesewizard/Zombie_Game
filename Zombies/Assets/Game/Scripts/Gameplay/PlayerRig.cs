using Cysharp.Threading.Tasks;
using Game.Scripts.Characters.Player;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
	public class PlayerRig : MonoBehaviour
	{
		private const string PLAYER_RIG_PREFAB_PATH = "Characters/Humans/Male/Player_Rig";

		[SerializeField, Required, Find(Destination.AllChildren)]
		private PlayerLogic playerLogic;
		public PlayerLogic PlayerLogic => playerLogic;

		public static async UniTask<PlayerRig> LoadAsync()
		{
			var loadedObject = await Resources.LoadAsync<PlayerRig>(PLAYER_RIG_PREFAB_PATH);
			var rig = (PlayerRig) Instantiate(loadedObject);
			return rig;
		}
	}
}