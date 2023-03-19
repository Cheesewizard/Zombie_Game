using Game.Save;

namespace Game.Scripts.Gameplay.Services
{
    public interface ILoadoutService 
    {
        public ILoadout CurrentLoadout { get; }
    }

    public class LoadoutService : ILoadoutService
    {
        public ILoadout CurrentLoadout { get; private set; }
         
         public LoadoutService()
         {
             //CurrentLoadout = SaveGameData.Instance.Loadout;
         }
         
         public LoadoutService(ILoadout currentLoadout)
         {
             CurrentLoadout = currentLoadout;
         }

         public void SetLoadout(ILoadout loadout)
         {
             CurrentLoadout = loadout;
         }
    }
}