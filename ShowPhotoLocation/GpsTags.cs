using System;
using Leadtools;
using Leadtools.Codecs;

namespace ShowPhotoLocation
{
   internal static class GpsTags
   {
      public static double? GetLongitude(string imagePath)
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

      public static double? GetLatitude(string imagePath)
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

      public static double? ExifGpsToDouble(string itemRef, RasterMetadataURational[] items)
      {
         if (string.IsNullOrWhiteSpace(itemRef) || items == null)
            throw new ArgumentException();

         double degrees = (double)items[0].Numerator / items[0].Denominator;
         if (degrees > 360) return null;
         double minutes = (double)items[1].Numerator / items[1].Denominator;
         double seconds = (double)items[2].Numerator / items[2].Denominator;

         double coordinate = degrees + (minutes / 60d) + (seconds / 3600d);

         if (itemRef == "S" || itemRef == "W")
            coordinate = coordinate * -1;

         return coordinate;
      }
   }
}
