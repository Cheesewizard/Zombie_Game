using System.Collections.Generic;
using Game.Configs;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons.Magazines.Ammunition
{
	public class MagazineStack
	{
		private MagazinePool magazinePool;

		private MagazineConfig magazineConfig;

		private int currentClipAmount;

		private int MagazinePoolSize => magazineConfig.MagazinePoolSize;

		public int TotalAmmoLeft { get; private set; } = 9;
		public int MaxAmmo { get; private set; }
		private readonly int capacity;

		private List<int> magazines = new();

		private Magazine reservedMagazine;

		public MagazineStack(MagazineConfig config, Transform parent)
		{
			magazineConfig = config;
			magazinePool = new MagazinePool(config.MagazinePrefab, parent, config.MagazinePoolSize);
			capacity = config.MagazinePrefab.Capacity;
			MaxAmmo = TotalAmmoLeft = config.InfiniteAmmo ? -1 : capacity * config.TotalMagazines;
		}

		private Magazine ReserveNextAvailableMagazine()
		{
			if (reservedMagazine == null)
			{
				reservedMagazine = magazinePool.RequestNext();
				reservedMagazine.ResetTransforms();
			}

			return reservedMagazine;
		}

		private void CreateStack()
		{
			magazines.Clear();
			for (var i = 0; i < MagazinePoolSize; i++)
			{
				magazines.Add(i);
			}
		}

		public void AddAmmo(int amount)
		{
			// Ignore if it's infinite
			//if (IsInfiniteAmmo) return;

			// Capped to MaxAmmo
			TotalAmmoLeft = Mathf.Min(MaxAmmo, TotalAmmoLeft + amount);
		}

		public Magazine TakeMagazine()
		{
			var magazine = reservedMagazine;
			if (magazine == null)
			{
				magazine = ReserveNextAvailableMagazine();
			}

			var refillAmount = TotalAmmoLeft < capacity ? TotalAmmoLeft : capacity;
			TotalAmmoLeft -= refillAmount;

			reservedMagazine = null;

			magazine.ModifyAmmoLeft(refillAmount);

			return magazine;
		}

		public void Dispose()
		{
			magazinePool.Dispose();
		}
	}
}