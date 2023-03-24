using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class MagazinePool
	{
		private Magazine magazine;
		private ObjectPool<Magazine> magazinePool;

		public MagazinePool(Magazine magazine, int poolSize)
		{
			this.magazine = magazine;
			magazinePool = new ObjectPool<Magazine>(CreateMagazine, OnGetMagazine, OnReleaseMagazine, OnDestroyMagazine,
				false, poolSize, 1);
		}

		private Magazine CreateMagazine()
		{
			var magazineClip = Object.Instantiate(magazine);
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