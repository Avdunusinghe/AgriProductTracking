using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracking.util
{
    public class ImageHelper
    {
        public static string getThumnialImage(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                var myThumbnail = image.GetThumbnailImage(300, 250, () => false, IntPtr.Zero);

                using (MemoryStream m = new MemoryStream())
                {
                    myThumbnail.Save(m, ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;

                }
            }

        }

        public static bool ThumbnailCallback()
        {
            return false;
        }
    }
}
