using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Scripts.Gameplay.Weapons.Magazines
{
	public class MagazinePool
	{
		private ObjectPool<Magazine> magazinePool;
		private Magazine prefab;
		private Transform parent;

		public MagazinePool(Magazine prefab, Transform parent, int poolSize)
		{
			this.prefab = prefab;
			this.parent = parent;
			
			magazinePool = new ObjectPool<Magazine>(CreateMagazine, OnGetMagazine, OnReleaseMagazine, OnDestroyMagazine,
				true, poolSize);
		}

		private Magazine CreateMagazine()
		{
			var magazineClip = Object.Instantiate(prefab, parent);
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

		public void Dispose()
		{
			magazinePool.Dispose();
		}
	}
}