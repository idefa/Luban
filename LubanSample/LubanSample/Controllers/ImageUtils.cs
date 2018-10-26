using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace LubanSample.Controllers
{
    public class ImageUtils
    {

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="hpf">HttpPostedFileBase 文件上传实例</param>
        /// <param name="folder">上传到的文件夹,不存在则创建</param>
        /// <param name="targetFileName">保存到的文件名</param>
        /// <returns></returns>
        public static UploadFileInfo UploadFiles(HttpPostedFileBase hpf, string folder, string targetFileName)
        {
            try
            {
                if (hpf.ContentLength == 0 || hpf == null)
                {
                    return new UploadFileInfo()
                    {
                        IsValid = false,
                        Length = hpf.ContentLength,
                        Message = "文件不存在,或者文件大小为0",
                        Type = hpf.ContentType
                    };
                }
                else
                {
                    if (hpf.ContentLength > ImageConfig.MaxSize) //如果大于规定最大尺寸
                    {
                        return new UploadFileInfo()
                        {
                            IsValid = false,
                            Length = hpf.ContentLength,
                            Message = "上传文件大小过大,要求是" + ImageConfig.MaxSize,
                            Type = hpf.ContentType
                        };
                    }
                    else
                    {
                        var extension = Path.GetExtension(hpf.FileName).ToLower();
                        if (!ImageConfig.AllowedExtensions.Contains(extension))//如果文件的后缀名不包含在规定的后缀数组中
                        {
                            return new UploadFileInfo()
                            {
                                IsValid = false,
                                Length = hpf.ContentLength,
                                Message = "图片格式必须是" + string.Join(",", ImageConfig.AllowedExtensions) + "的一种",
                                Type = hpf.ContentType
                            };
                        }
                        else
                        {
                            string virtualPath = Path.Combine(ImageConfig.BaseFolder, folder);
                            string fileDirectory = MapPath(Path.Combine("~/", virtualPath));
                            //在根目录下创建目标文件夹
                            if (CreateFolderIfNeeded(fileDirectory))
                            {
                                //保存原图

                                string filePath = Path.Combine(fileDirectory, targetFileName);

                                try
                                {
                                    new Luban(hpf).Compress(filePath);
                                }
                                catch (Exception ex)
                                {
                                    //Log.Save("ImageLog", string.Format("保存异常: {0}, {1}", ex.Message, ex.StackTrace));

                                    try
                                    {
                                        File.Delete(filePath);
                                        new Luban(hpf).Compress(filePath);
                                    }
                                    catch (Exception io)
                                    {
                                        //Log.Save("ImageLog", string.Format("删除异常: {0}, {1}", io.Message, io.StackTrace));
                                    }
                                }
                            }
                            return new UploadFileInfo()
                            {
                                FileName = targetFileName,
                                Folder = folder,
                                Url = ImageConfig.ImageSite + Path.Combine(virtualPath, targetFileName).Replace('\\', '/'),
                                IsValid = true,
                                Length = hpf.ContentLength,
                                Message = "上传成功",
                                Type = hpf.ContentType
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new UploadFileInfo()
                {
                    IsValid = false,
                    Length = hpf.ContentLength,
                    Message = "上传异常:" + ex.Message,
                    Type = hpf.ContentType
                };
            }
        }

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="hpf">HttpPostedFileBase 文件上传实例</param>
        /// <param name="folder">上传到的文件夹,不存在则创建</param>
        /// <param name="targetFileName">保存到的文件名</param>
        /// <returns></returns>
        public static UploadFileInfo UploadFiles(HttpPostedFile hpf, string folder, string targetFileName)
        {
            try
            {
                if (hpf.ContentLength == 0 || hpf == null)
                {
                    return new UploadFileInfo()
                    {
                        IsValid = false,
                        Length = hpf.ContentLength,
                        Message = "文件不存在,或者文件大小为0",
                        Type = hpf.ContentType
                    };
                }
                else
                {
                    if (hpf.ContentLength > ImageConfig.MaxSize) //如果大于规定最大尺寸
                    {
                        return new UploadFileInfo()
                        {
                            IsValid = false,
                            Length = hpf.ContentLength,
                            Message = "上传文件大小过大,要求是" + ImageConfig.MaxSize,
                            Type = hpf.ContentType
                        };
                    }
                    else
                    {
                        var extension = Path.GetExtension(hpf.FileName).ToLower(); ;
                        if (!ImageConfig.AllowedExtensions.Contains(extension))//如果文件的后缀名不包含在规定的后缀数组中
                        {
                            return new UploadFileInfo()
                            {
                                IsValid = false,
                                Length = hpf.ContentLength,
                                Message = "图片格式必须是" + string.Join(",", ImageConfig.AllowedExtensions) + "的一种",
                                Type = hpf.ContentType
                            };
                        }
                        else
                        {
                            string virtualPath = Path.Combine(ImageConfig.BaseFolder, folder);
                            string fileDirectory = MapPath(Path.Combine("~/", virtualPath));
                            //在根目录下创建目标文件夹
                            if (CreateFolderIfNeeded(fileDirectory))
                            {
                                //保存原图

                                string filePath = Path.Combine(fileDirectory, targetFileName);

                                try
                                {
                                    new Luban(hpf).Compress(filePath);
                                }
                                catch (Exception ex)
                                {
                                    //Log.Save("ImageLog", string.Format("保存异常: {0}, {1}", ex.Message, ex.StackTrace));

                                    try
                                    {
                                        File.Delete(filePath);
                                        new Luban(hpf).Compress(filePath);
                                    }
                                    catch (Exception io)
                                    {
                                        //Log.Save("ImageLog", string.Format("删除异常: {0}, {1}", io.Message, io.StackTrace));
                                    }
                                }
                            }
                            return new UploadFileInfo()
                            {
                                FileName = targetFileName,
                                Folder = folder,
                                Url = ImageConfig.ImageSite + Path.Combine(virtualPath, targetFileName).Replace('\\', '/'),
                                IsValid = true,
                                Length = hpf.ContentLength,
                                Message = "上传成功",
                                Type = hpf.ContentType
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new UploadFileInfo()
                {
                    IsValid = false,
                    Length = hpf.ContentLength,
                    Message = "上传异常:" + ex.Message,
                    Type = hpf.ContentType
                };
            }
        }

        /// <summary>
        /// 得到物理路径
        /// </summary>
        private static string MapPath(string path)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(path);

            return HttpRuntime.AppDomainAppPath + path.Replace("~", string.Empty).Replace('/', '\\');
        }

        //根据相对路径在项目根路径下创建文件夹
        private static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
    }

    public class ImageConfig
    {
        /// <summary>
        /// 文件大小上限默认 2M
        /// </summary>
        public const int MaxSize = 5 * 1024 * 1024;

        /// <summary>
        /// 图片的扩展名
        /// </summary>
        public static string[] AllowedExtensions = { ".png", ".gif", ".jpg", ".jpeg" };

        /// <summary>
        /// 根文件夹
        /// </summary>
        public const string BaseFolder = "Images";

        public static string ImageSite = "http://localhost:63175/";
        static ImageConfig()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ImageSite"]))
                ImageSite = ConfigurationManager.AppSettings["ImageSite"];
        }
    }

    public class UploadFileInfo
    {
        /// <summary>
        /// 带后缀名的文件名称 xxx.jpg
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件所属的文件夹
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 图片的完整路径：~/AjaxUpload/20141112_large.jpg
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 图片的类型：image/jpeg 
        /// </summary>
        public string Type { get; set; }

        public bool IsValid { get; set; }

        /// <summary>
        /// 输出消息
        /// </summary>
        public string Message { get; set; }


    }
}