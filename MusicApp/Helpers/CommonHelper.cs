using System;

namespace MusicApp.Helpers
{
    public static class CommonHelper
    {
        public static string EncryptId(int id)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(id.ToString());
            return Convert.ToBase64String(plainTextBytes);
        }

        public static int DecryptId(string encryptedId)
        {
            var base64EncodedBytes = Convert.FromBase64String(encryptedId);
            var decodedString = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return int.Parse(decodedString);
        }
    }
}