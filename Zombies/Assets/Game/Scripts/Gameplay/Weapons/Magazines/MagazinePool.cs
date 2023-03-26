using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class MagazinePool
	{
		private Magazine magazine;
		private ObjectPool<Magazine> magazinePool;
		private Transform parent;

		public MagazinePool(Magazine magazine, Transform parent, int poolSize)
		{
			this.magazine = magazine;
			this.parent = parent;
			magazinePool = new ObjectPool<Magazine>(CreateMagazine, OnGetMagazine, OnReleaseMagazine, OnDestroyMagazine,
				false, poolSize, poolSize);
		}

		private Magazine CreateMagazine()
		{
			var magazineClip = Object.Instantiate(magazine, parent);
			magazineClip.Init();
			return magazineClip;
		}

		private void OnGetMagazine(Magazine bullet)
		{
			bullet.gameObject.SetActive(true);
		}

		private void OnDestroyMagazine(Magazine magazine)
		{
			Object.Destroy(magazine.gameObject);
		}

		private void OnReleaseMagazine(Magazine magazine)
		{
			magazine.gameObject.SetActive(false);
		}

		public Magazine RequestNext()
		{
			return magazinePool.Get();
		}

		public void ReturnToPool(Magazine magazine)
		{
			magazinePool.Release(magazine);
		}

		private void Dispose()
		{
			magazinePool.Dispose();
		}
	}
}