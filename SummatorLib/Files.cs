using System.IO;
using System.Security.Cryptography;

namespace SummatorLib
{
    public static class Files
    {

        /// <summary>
        /// Calculates crc32 sum of file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Crc32Sum(string filename)
        {
            if (false == File.Exists(filename))
                throw new FileNotFoundException(filename);

            using (var stream = File.OpenRead(filename))
            {
                return Crc32Sum(stream).ToString("X8");
            }
        }

        /// <summary>
        /// Calculates crc32 sum of stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static uint Crc32Sum(Stream stream)
        {
            // From Ben Watson, C# 4.0 How-To
            const int bufsize = 1024;
            const uint polynomial = 0xEDB88320;

            uint result = 0xFFFFFFFF;
            byte[] buffer = new byte[bufsize];
            uint[] crc32Table = new uint[256];

            unchecked
            {
                for (int i = 0; i < 256; i++)
                {
                    uint crc32 = (uint)i;
                    for (int j = 8; j > 0; j--)
                    {
                        if ((crc32 & 1) == 1)
                            crc32 = (crc32 >> 1) ^ polynomial;
                        else
                            crc32 >>= 1;
                    }
                    crc32Table[i] = crc32;
                }

                int count = stream.Read(buffer, 0, bufsize);

                while (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        result = (result >> 8) ^ crc32Table[buffer[i] ^ (result & 0x000000FF)];
                    }
                    count = stream.Read(buffer, 0, bufsize);
                }
            }
            return ~result;
        }

        /// <summary>
        /// Calculates md5 sum of file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Md5Sum(string filename)
        {
            if (false == File.Exists(filename))
                throw new FileNotFoundException(filename);

            using (var md5 = MD5.Create())
            {
                return CustomSum(md5, filename);
            }
        }

        /// <summary>
        /// Calculates sha1 sum of file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Sha1Sum(string filename)
        {
            if (false == File.Exists(filename))
                throw new FileNotFoundException(filename);

            using (var sha1 = SHA1.Create())
            {
                return CustomSum(sha1, filename);
            }
        }

        /// <summary>
        /// Calculates sha256 sum of file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Sha256Sum(string filename)
        {
            if (false == File.Exists(filename))
                throw new FileNotFoundException(filename);

            using (var sha256 = SHA256.Create())
            {
                return CustomSum(sha256, filename);
            }
        }

        /// <summary>
        /// Calculates sha512 sum of file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Sha512Sum(string filename)
        {
            if (false == File.Exists(filename))
                throw new FileNotFoundException(filename);

            using (var sha512 = SHA512.Create())
            {
                return CustomSum(sha512, filename);
            }
        }

        /// <summary>
        /// Calculates custom sum
        /// </summary>
        /// <param name="algorithm">Hash algorithm</param>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        private static string CustomSum(HashAlgorithm algorithm, string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return Utils.ByteaToString(algorithm.ComputeHash(stream));
            }
        }

    }
}
