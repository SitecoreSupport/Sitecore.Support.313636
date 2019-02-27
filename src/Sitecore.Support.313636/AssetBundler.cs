namespace Sitecore.Support.XA.Foundation.Theming.Bundler
{
  using Sitecore.Data.Items;
  using Sitecore.Resources.Media;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.XA.Foundation.Theming.Configuration;

  public class SupportAssetBundler : Sitecore.XA.Foundation.Theming.Bundler.AssetBundler
  {
    public override string GetOptimizedItemPath(Item theme, OptimizationType type, AssetServiceMode mode)
    {
      Item optimizedItem = GetOptimizedItem(theme, type, mode);
      if (optimizedItem != null)
      {
        return optimizedItem.BuildAssetPath(true);
      }
      return string.Empty;
    }
  }
}