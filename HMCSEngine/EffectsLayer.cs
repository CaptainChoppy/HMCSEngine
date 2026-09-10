
namespace HMCSEngine
{
    internal static class EffectsLayers
    {
        private const int LayerCount = 6;

        private static EffectLayer[] EffectLayers = new EffectLayer[LayerCount];

        public static void Initalize()
        {
            for (int i = 0; i < LayerCount; i++)
            {
                EffectLayers[i] = new EffectLayer();
            }
        }

        public static void DrawLayer(EffectLayerName layer)
        {
            GetLayer(layer).Draw();
        }

        public static void SetLayerEffect(EffectLayerName layer, Effect effect)
        {
            GetLayer(layer).Effect = effect;
        }

        public static void Update()
        {
            for(int i = 0; i < LayerCount; i++)
            {
                GetLayer((EffectLayerName)(i)).Update();
            }
        }

        private static EffectLayer? GetLayer(EffectLayerName layer)
        {
            int layerindex = (int)(layer);

            if (layerindex >= LayerCount)
            {
                Debug.ErrorLog($"Tried to get a layer with an out of bounds index. index : {layerindex}");
                return null;
            }

            return EffectLayers[layerindex];
        }
    }

    internal class EffectLayer
    {
        public Effect Effect = null;

        public void Draw()
        {
            if(Effect == null)
            {
                return;
            }

            Effect.Draw();
        }

        public void Update()
        {
            if (Effect == null)
            {
                return;
            }

            Effect.Update();
        }
    }

    internal enum EffectLayerName
    {
        Layer0 = 0,
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5
    }
}
