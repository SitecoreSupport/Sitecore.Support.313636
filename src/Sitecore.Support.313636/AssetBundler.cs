namespace Sitecore.Support.XA.Foundation.Theming.Bundler
{
  using Sitecore.Data.Items;
  using Sitecore.Resources.Media;
  using Sitecore.XA.Foundation.Theming.Configuration;

  public class SupportAssetBundler : Sitecore.XA.Foundation.Theming.Bundler.AssetBundler
  {
    public override string GetOptimizedItemPath(Item theme, OptimizationType type, AssetServiceMode mode)
    {
      Item optimizedItem = GetOptimizedItem(theme, type, mode);
      if (optimizedItem != null)
      {
        return BuildAssetPath(optimizedItem, true);
      }
      return string.Empty;
    }

    private static string BuildAssetPath(Item item, bool addTimestamp = false)
    {
      string mediaUrl = MediaManager.GetMediaUrl(item, new MediaUrlOptions
      {
        Thumbnail = true
      });
      mediaUrl = mediaUrl.Replace("&thn=1", string.Empty);
      mediaUrl = mediaUrl.Replace("?thn=1&", "?");
      mediaUrl = mediaUrl.Replace("?thn=1", string.Empty);
      mediaUrl = (mediaUrl.Contains("://") ? mediaUrl : StringUtil.EnsurePrefix('/', mediaUrl));
      if (addTimestamp)
      {
        if (mediaUrl.Contains("?"))
        {
          mediaUrl = mediaUrl + "&t=" + item[Sitecore.XA.Foundation.SitecoreExtensions.Templates.Statistics.Fields.__Created];
        }
        else
        {
          mediaUrl = mediaUrl + "?t=" + item[Sitecore.XA.Foundation.SitecoreExtensions.Templates.Statistics.Fields.__Created];
        }
      }
      return mediaUrl;
    }
  }
}