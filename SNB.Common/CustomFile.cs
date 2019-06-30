using SNB.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SNB.Common
{
    public static class CustomFile
    {
        public static string SaveImageFile(HttpPostedFileBase file, string name, string subFolder = "")
        {
            string url = "";
            subFolder = !string.IsNullOrWhiteSpace(subFolder) ? subFolder + "/" : "";

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileExtensions = Path.GetExtension(file.FileName);

                    if (allowedExtensions.Contains(fileExtensions.ToLower()))
                    {
                        string dateStr = string.Format("{0:yyMMddhhmmss}", DateTime.Now);
                        string directory = ("~/Uploads/Images/" + subFolder).CheckDirectory();
                        fileName = name.Replace(" ", "_") + "_" + dateStr;
                        url = directory + fileName + fileExtensions;
                        file.SaveAs(HttpContext.Current.Server.MapPath(url));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return url;
        }

        public static List<AttachmentFile> SaveImageFile(List<HttpPostedFileBase> files, string name, string subFolder = "")
        {
            List<AttachmentFile> AttachmentFiles = new List<AttachmentFile>();
            subFolder = !string.IsNullOrWhiteSpace(subFolder) ? subFolder + "/" : "";

            try
            {
                if (files != null && files.Count > 0)
                {
                    var index = 0;
                    string directory = ("~/Uploads/Images/" + subFolder).CheckDirectory();

                    foreach (var file in files)
                    {
                        string url = "";

                        if (file != null && file.ContentLength > 0)
                        {
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var fileExtensions = Path.GetExtension(file.FileName);

                            if (allowedExtensions.Contains(fileExtensions.ToLower()))
                            {
                                string dateStr = string.Format("{0:yyMMddhhmmss}", DateTime.Now);
                                fileName = name.Replace(" ", "_") + "_" + dateStr + "_" + (index++).ToString();
                                url = directory + fileName + fileExtensions;
                                file.SaveAs(HttpContext.Current.Server.MapPath(url));

                                AttachmentFiles.Add(new AttachmentFile()
                                {
                                    FileName = fileName,
                                    OrginalFileName = file.FileName,
                                    FileExtension = fileExtensions,
                                    FileSize = file.ContentLength,
                                    FileLocation = url
                                });
                            }
                        }
                        url = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return AttachmentFiles;
        }
    }
}
