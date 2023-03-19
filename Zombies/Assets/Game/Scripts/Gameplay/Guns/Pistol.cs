using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Gameplay.Weapons;

namespace Game.Scripts.Gameplay.Guns
{
    public class Pistol : Gun, IUsableWeapon
    {
        public int WeaponId => gunConfig.WeaponId;

        private bool canShoot = true;

        private void Start()
        {
            base.Init(gunConfig);
        }

        public async UniTask PerformAction()
        {
            await Fire();
        }

        protected override async UniTask Fire()
        {
            if (!canShoot) return;

            RaiseUseWeaponEvent(gunConfig);
            bullet.ResetTransform(BulletSpawnPosition);
            bullet.Launch(this);

            // TODO: I dont like this arbituary delay, need a better design
            await UniTask.Delay(TimeSpan.FromSeconds(gunConfig.FireRate.Random()));
            canShoot = true;
            RaiseFinishWeaponEvent();
        }

        protected override void Reload()
        {
            throw new NotImplementedException();
        }

        protected override void Activate()
        {
            RaiseOnActivatedEvent(this);
        }

        protected override void Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}