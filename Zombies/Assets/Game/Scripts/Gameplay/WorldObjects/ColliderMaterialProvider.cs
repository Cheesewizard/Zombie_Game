using UnityEngine;

namespace Game.Scripts.Gameplay.WorldObjects
{
    public class ColliderMaterialProvider : MonoBehaviour
    {
        [SerializeField] private bool shouldAttachImpactVfx;
        public bool ShouldAttachImpactVfx => shouldAttachImpactVfx;

        [SerializeField] private ColliderSurfaceMaterial surfaceMaterial;
        public ColliderSurfaceMaterial SurfaceMaterial => surfaceMaterial;

        public void SetMaterial(ColliderSurfaceMaterial newSurfaceMaterial)
        {
            surfaceMaterial = newSurfaceMaterial;
        }

        public void EnableShouldAttachVfx(bool enable)
        {
            shouldAttachImpactVfx = enable;
        }
    }

    public enum ColliderSurfaceMaterial
    {
        Default,
        Book,
        Brick,
        Carpet,
        Clay,
        Concrete,
        ConcreteLoose,
        Dirt,
        ElectricDevice,
        Fabric,
        Glass,
        GlassBreak,
        MetalHeavy,
        MetalHollow,
        MetalLight,
        Plant,
        PlasticHeavy,
        PlasticLight,
        Rubber,
        Wood,
        Plaster
    }
}