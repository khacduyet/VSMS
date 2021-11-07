using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VSMS.Common
{
    public static class CommonConstants
    {
        // SESSION COMMON ADMIN
        public static string USER_SESSION;
        public static string MEMBER_SESSION;
        // Mã hóa mật khẩu
        public static string ParseMD5(string pass)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(pass)).Select(s => s.ToString("x2")));
        }
        // Tìm xem file có tồn tại và xóa file
        public static bool CheckExistDeleteFile(string url)
        {
            if (System.IO.File.Exists(@url)){
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(@url);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return true;
        }
    }
}