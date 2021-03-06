﻿using Deamon.Models;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.IO.Compression;

namespace Deamon.Backup
{
    public static class Helper
    {
        public static void CopyCompared(string source, string destination, SnapShotModel snap)
        {
            //string finaldir = Path.Combine(destination, DateTime.UtcNow.ToString().Replace(':', ';').Replace(' ', '_'));
            string temp = "";
            //destination.Replace('/',Path.DirectorySeparatorChar);
            //if (ToZip)
            //    temp = "C:\\TEMPBACKUP";
            temp = Path.Combine(destination, DateTime.UtcNow.ToString().Replace(':', ';').Replace(' ', '_'));
            Directory.CreateDirectory(destination);
            //Console.WriteLine(temp);
            foreach (string item in Directory.GetDirectories(source))
            {
                if (snap.Dirs.Contains(item) == false)
                {
                    DirectoryInfo dirinf = new DirectoryInfo(item);
                    var temp2 = Path.Combine(destination, dirinf.Name);
                    Directory.CreateDirectory(temp2);
                    snap.Dirs.Add(item);
                    CopyCompared(item,temp2, snap);
                }
            }
            foreach (string item in Directory.GetFiles(source))
            {
                if (snap.Files.Contains(item) == false)
                {
                    File.Copy(item, Path.Combine(destination, Path.GetFileName(item)));
                    snap.Files.Add(item);
                }
            }
            //if(ToZip)
            //{
            //    ZipFile.CreateFromDirectory(temp, finaldir);
            //}
        }


        public static bool IsTime(string data)
        {
            if (data.ToString().Substring(0, 17) == DateTime.UtcNow.ToString().Substring(0, 17))
            {
                return true;
            }
            else return false;
        }


        public static async Task UploadToFTP(string[] filesToUpload, string[] dirsToUpload, string server, string user, string passwd)
        {
            foreach (string item in dirsToUpload)
            {
                using (FtpClient ftp = new FtpClient(server, new System.Net.NetworkCredential { UserName = user, Password = passwd }))
                {
                    string temp = new DirectoryInfo(item).Name;
                    await ftp.CreateDirectoryAsync(temp, true);
                }
            }

            foreach (string item in filesToUpload)
            {
                using (FtpClient ftp = new FtpClient(server, new System.Net.NetworkCredential { UserName = user, Password = passwd }))
                {
                    using (FileStream fs = File.OpenRead(item))
                    {
                        await ftp.UploadFileAsync(item, Path.GetFileName(item));
                    }
                }
            }
        }
    }
}
