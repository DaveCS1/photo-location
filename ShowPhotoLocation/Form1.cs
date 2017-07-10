using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Controls;
using Leadtools.Drawing;

namespace ShowPhotoLocation
{
   public partial class Form1 : Form
   {
      private readonly string _projectPath = @"Examples\GIT\ShowPhotoLocation\ShowPhotoLocation\";
      private readonly string _rootPath = @"..\..\..\";

      public Form1()
      {
         RasterSupport.SetLicense(
            File.ReadAllBytes(Path.Combine(_rootPath, @"Common\License\leadtools.lic")),
            File.ReadAllText(Path.Combine(_rootPath, @"Common\License\leadtools.lic.key")));

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
         ListDirectory(treeView1, Path.Combine(_rootPath, _projectPath, "Images"));
      }

      private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
      {
         UpdateStatusMessage("");
         if (e.Node == null)
            return;

         string fullPath = Path.Combine(_rootPath, _projectPath, e.Node.FullPath);

         if (!File.Exists(fullPath))
            return;

         if (rasterPictureBox1.Image != null && !rasterPictureBox1.Image.IsDisposed)
            rasterPictureBox1.Image.Dispose();

         try
         {
            using (var codecs = new RasterCodecs())
            {
               rasterPictureBox1.Image = codecs.Load(fullPath);
            }
         }
         catch (Exception exception)
         {
            ClearMap(e.Node.Text);
            UpdateStatusMessage(exception.Message);
            return;
         }

         double? longitude = GpsTags.GetLongitude(fullPath);
         double? latitude = GpsTags.GetLatitude(fullPath);

         if (!longitude.HasValue || !latitude.HasValue)
         {
            ClearMap(e.Node.Text);
            return;
         }
         labelStatusMapLink.Visible = true;

         UpdateStatusMessage($@"Coordinates for {e.Node.Text}: {latitude:F5}, {longitude:F5}");
         UpdateStatusMapLink(latitude.Value, longitude.Value);

         string targetHtmlFile = Path.Combine(Application.StartupPath, "map.html");
         CreateHtmlFile("MapTemplate.html", targetHtmlFile, 
            latitude.ToString(), longitude.ToString(), 
            e.Node.Name);

         webBrowser1.Url = new Uri(targetHtmlFile);
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

      private void LabelStatusMapLink_Click(object sender, EventArgs e)
      {
         // if google maps gives you grief about the webbrowser control being too old,
         // read this and make it ie11: 
         // https://weblog.west-wind.com/posts/2011/May/21/Web-Browser-Control-Specifying-the-IE-Version
         var url = labelStatusMapLink.Tag as string;
         if (url == null) return;
         webBrowser1.Url = new Uri(url);
      }

      private void LabelStatusStreeViewLink_Click(object sender, EventArgs e)
      {
         var url = labelStatusStreeViewLink.Tag as string;
         if (url == null) return;
         Process.Start(url);
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
         foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(directory));
         foreach (FileInfo file in directoryInfo.GetFiles())
            directoryNode.Nodes.Add(new TreeNode(file.Name));
         return directoryNode;
      }

      private static void CreateHtmlFile(string templateFile, string targetFile, string la, string lo,
                                         string title)
      {
         if (!File.Exists(templateFile)) return;
         using (var reader = new StreamReader(templateFile))
         {
            string htmlString = reader.ReadToEnd()
                                      .Replace("$la$", la)
                                      .Replace("$lo$", lo)
                                      .Replace("$title$", title);
            using (var writer = new StreamWriter(targetFile))
            {
               writer.Write(htmlString);
            }
         }
      }

   }
}