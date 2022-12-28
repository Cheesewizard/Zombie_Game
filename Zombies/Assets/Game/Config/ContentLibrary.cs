using Quack.ReferenceMagic.Runtime;
using Quack.SceneMagic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Config
{
	[CreateAssetMenu(menuName = "Config/ContentLibrary")]
	public class ContentLibrary : ScriptableObject
	{
		[TabGroup("Scenes"), Title("System Levels", "Essential Scenes for the core game loop.")]
		[SerializeField, Required, FindInAssets]
		private SceneConfig missionSceneConfig;

		public SceneConfig MissionSceneConfig => missionSceneConfig;

		[TabGroup("Scenes")]
		[SerializeField, Required, FindInAssets]
		private SceneConfig landingPageSceneConfig;
		public SceneConfig LandingPageSceneConfig => landingPageSceneConfig;
	}
}