using System;
using System.IO;
using System.Web;

namespace GiellyGreen.Helpers
{
    public class SupplierHelper
    {
        public static string SetModelLogo(string supplierName,string supplierLogo)
        {
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (supplierLogo != null)
            {
                string imgName = supplierName + ".jpeg";
                string imgPath = Path.Combine(path, imgName);
                byte[] imgBytes = Convert.FromBase64String(supplierLogo);
                File.WriteAllBytes(imgPath, imgBytes);
                supplierLogo = imgName;
            }
            return supplierLogo;
        }
    }
}