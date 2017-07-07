using System;
using System.IO;
using System.Windows.Forms;
using Leadtools.Codecs;
using Leadtools.Controls;
using Leadtools;
using Leadtools.Drawing;

namespace ShowPhotoLocation
{
   public partial class Form1 : Form
   {
      private readonly string _rootPath = Application.StartupPath;

      public Form1()
      {
         RasterSupport.SetLicense(
            File.ReadAllBytes(@"C:\LEADTOOLS 19\Common\License\leadtools.lic"),
            File.ReadAllText(@"C:\LEADTOOLS 19\Common\License\leadtools.lic.key")
            );

         InitializeComponent();

         webBrowser1.ScriptErrorsSuppressed = true;
         webBrowser1.ScrollBarsEnabled = true;
        
         RasterPaintProperties paintProperties = RasterPaintProperties.Default;
         paintProperties.PaintDisplayMode = RasterPaintDisplayModeFlags.Bicubic;
         paintProperties.PaintEngine = RasterPaintEngine.GdiPlus;
         paintProperties.UsePaintPalette = true;
         rasterPictureBox1.PaintProperties = paintProperties;
         rasterPictureBox1.SizeMode = RasterPictureBoxSizeMode.Fit;
      }


      private void Form1_Load(object sender, EventArgs e)
      {
         ListDirectory(treeView1, Path.Combine(_rootPath, "Images"));
      }

      private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
      {
         UpdateStatusMessage("");
         if (e.Node == null)
            return;

         string fullPath = Path.Combine(_rootPath, e.Node.FullPath);
         
         if (!File.Exists(fullPath))
            return;

         if (rasterPictureBox1.Image != null && !rasterPictureBox1.Image.IsDisposed)
            rasterPictureBox1.Image.Dispose();

         try
         {
            using (RasterCodecs codecs = new RasterCodecs())
               rasterPictureBox1.Image = codecs.Load(fullPath);
         }
         catch (Exception exception)
         {
            ClearMap(e.Node.Text);
            UpdateStatusMessage(exception.Message);
            return;
         }
         
         double? longitude = GetLongitude(fullPath);
         double? latitude = GetLatitude(fullPath);

         if (!longitude.HasValue || !latitude.HasValue)
         {
            ClearMap(e.Node.Text);
            return;
         }
         labelStatusMapLink.Visible = true;

         UpdateStatusMessage($@"Coordinates for {e.Node.Text}: {latitude:F5}, {longitude:F5}");
         UpdateStatusMapLink(latitude.Value, longitude.Value);

         string targetHtmlFile = Path.Combine(Application.StartupPath, "map.html");
         CreateHtmlFile("MapTemplate.html", targetHtmlFile, latitude.ToString(), longitude.ToString(), e.Node.Name, "", "", fullPath);

         webBrowser1.Url = new Uri(targetHtmlFile);
      }

      private static void ListDirectory(TreeView treeView, string path)
      {
         treeView.Nodes.Clear();
         var rootDirectoryInfo = new DirectoryInfo(path);
         treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
      }

      private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
      {
         var directoryNode = new TreeNode(directoryInfo.Name);
         foreach (var directory in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(directory));
         foreach (var file in directoryInfo.GetFiles())
            directoryNode.Nodes.Add(new TreeNode(file.Name));
         return directoryNode;
      }

      private void ClearMap(string fileName)
      {
         labelStatusMapLink.Visible = false;
         webBrowser1.Url = new Uri("about:blank");
         UpdateStatusMessage($@"No GPS information found in {fileName}");
      }

      private void UpdateStatusMessage(string message)
      {
         labelStatus.Text = message;
      }

      private void UpdateStatusMapLink(double latitude, double longitude)
      {
         
         labelStatusMapLink.Tag = $@"http://maps.google.com/?q={latitude},{longitude}";
         labelStatusStreeViewLink.Tag =
            $@"http://maps.google.com/?q=&layer=c&cbll={latitude},{longitude}&cbp=11,0,0,0,0";
      }

      private static double? GetLongitude(string imagePath)
      {
         try
         {
            using (var codecs = new RasterCodecs())
            {
               RasterMetadataURational[] data = 
                  codecs.ReadComment(imagePath, 1, RasterCommentMetadataType.GpsLongitude)?.ToURational();
               string refData = 
                  codecs.ReadComment(imagePath, 1, RasterCommentMetadataType.GpsLongitudeRef)?.ToAscii();
               return ExifGpsToDouble(refData, data);
            }
         }
         catch (ArgumentException)
         {
            return null;
         }
      }

      private static double? GetLatitude(string imagePath)
      {
         try
         {
            using (var codecs = new RasterCodecs())
            {
               RasterMetadataURational[] data =
                  codecs.ReadComment(imagePath, 1, RasterCommentMetadataType.GpsLatitude)?.ToURational();
               string refData =
                  codecs.ReadComment(imagePath, 1, RasterCommentMetadataType.GpsLatitudeRef)?.ToAscii();
               return ExifGpsToDouble(refData, data);
            }
         }
         catch (ArgumentException)
         {
            return null;
         }
      }

      private static double? ExifGpsToDouble(string itemRef, RasterMetadataURational[] items)
      {
         if (string.IsNullOrWhiteSpace(itemRef) || items == null)
            throw new ArgumentException();

         double degrees = (double) items[0].Numerator / items[0].Denominator;
         if (degrees > 360) return null;
         double minutes = (double) items[1].Numerator / items[1].Denominator;
         double seconds = (double) items[2].Numerator / items[2].Denominator;

         double coordinate = degrees + (minutes / 60d) + (seconds / 3600d);
         
         if (itemRef == "S" || itemRef == "W")
            coordinate = coordinate * -1;

         return coordinate;
      }

      private static void CreateHtmlFile(string templateFile, string targetFile, string la, string lo,
                                        string title, string city, string marker, string imagePath)
      {
         if (!File.Exists(templateFile)) return;
         using (StreamReader reader = new StreamReader(templateFile))
         {
            string htmlString = reader.ReadToEnd()
                                      .Replace("[la]", la)
                                      .Replace("[lo]", lo)
                                      .Replace("[title]", title)
                                      .Replace("[city]", city)
                                      .Replace("[image]", imagePath)
                                      .Replace("[marker]", marker);
            using (var writer = new StreamWriter(targetFile))
               writer.Write(htmlString);
         }
      }

      private void LabelStatusMapLink_Click(object sender, EventArgs e)
      {
         // if google maps gives you grief about the webbrowser control being too old,
         // read this and make it ie11: 
         // https://weblog.west-wind.com/posts/2011/May/21/Web-Browser-Control-Specifying-the-IE-Version
         string url = labelStatusMapLink.Tag as string;
         if (url == null) return;
         webBrowser1.Url = new Uri(url);
      }

      private void LabelStatusStreeViewLink_Click(object sender, EventArgs e)
      {
         string url = labelStatusStreeViewLink.Tag as string;
         if (url == null) return;
         //webBrowser1.Url = new Uri(url);
         System.Diagnostics.Process.Start(url);
      }


   }
}
