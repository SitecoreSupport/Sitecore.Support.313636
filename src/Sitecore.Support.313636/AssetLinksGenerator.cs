namespace Sitecore.Support.XA.Foundation.Theming.Bundler
{
  using Sitecore.Data.Items;
  using Sitecore.Resources.Media;
  using Sitecore.XA.Foundation.Theming;
  using Sitecore.XA.Foundation.Theming.Bundler;
  using Sitecore.XA.Foundation.Theming.Configuration;
  using Sitecore.XA.Foundation.Theming.EventHandlers;
  using System.IO;

  public class SupportAssetLinksGenerator : Sitecore.XA.Foundation.Theming.Bundler.AssetLinksGenerator
  {
    public SupportAssetLinksGenerator() : base() { }

    public static new AssetLinks GenerateLinks(IThemesProvider themesProvider)
    {
      if (!AssetContentRefresher.IsPublishing() && !IsAddingRendering())
      {
        return new SupportAssetLinksGenerator().GenerateAssetLinks(themesProvider);
      }
      return new AssetLinks();
    }

    protected override string GetOptimizedItemLink(Item theme, OptimizationType type, AssetServiceMode mode, string query, string fileName)
    {
      query = string.Format(query, Templates.OptimizedFile.ID, fileName);
      Item item = theme.Axes.SelectSingleItem(query);
      if (item != null && IsNotEmpty(item))
      {
        return BuildAssetPath(item, true);
      }
      return new SupportAssetBundler().GetOptimizedItemPath(theme, type, mode);
    }

    private bool IsNotEmpty(Item optimizedScriptItem)
    {
      using (Stream stream = ((MediaItem)optimizedScriptItem).GetMediaStream())
      {
        return stream != null && stream.Length > 0;
      }
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
        mediaUrl = mediaUrl + "?t=" + item[Sitecore.XA.Foundation.SitecoreExtensions.Templates.Statistics.Fields.__Created];
      }
      return mediaUrl;
    }
  }
}