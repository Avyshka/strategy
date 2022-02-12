using System;

namespace Aivagames.Strategy.Utils.AssetsInjector
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute : Attribute
    {
        public readonly string Assetname;

        public InjectAssetAttribute(string assetname = null)
        {
            Assetname = assetname;
        }
    }
}