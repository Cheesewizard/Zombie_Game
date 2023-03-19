using Game.Configs;
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

        [TabGroup("Scenes")] [SerializeField, Required, FindInAssets]
        private SceneConfig landingPageSceneConfig;

        public SceneConfig LandingPageSceneConfig => landingPageSceneConfig;

        [TabGroup("Loadout"), Title("Guns")]
        [SerializeField, Required, FindInAssets, HideLabel]
        [ListDrawerSettings(HideAddButton = true)]
        private WeaponLibrary weaponLibrary = new WeaponLibrary();
        public WeaponLibrary WeaponLibrary => weaponLibrary;

        [TabGroup("Gameplay"), Title("Bullet Impact Data", "All bullet impact data used in the game is held here.")]
        [SerializeField, HideLabel]
        private BulletImpactDataLibrary bulletImpactDataLibrary = new BulletImpactDataLibrary();

        public BulletImpactDataLibrary BulletImpactDataLibrary => bulletImpactDataLibrary;

        [TabGroup("Blood")] [SerializeField, Required]
        private ParticleSystem bloodLimbBullet;
        public ParticleSystem BloodLimbBullet => bloodLimbBullet;

        private void OnEnable()
        {
            weaponLibrary.Init();
            bulletImpactDataLibrary.Init();
        }
    }
}