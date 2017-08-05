namespace SummatorLib
{
    public static class Utils
    {

        /// <summary>
        /// Prints program usage information
        /// </summary>
        /// <param name="program"></param>
        public static void PrintUsage(string program)
        {
            System.Console.WriteLine($"Usage: {program} [file1 file2 ...]");
        }

        /// <summary>
        /// Converts bytes array to string
        /// </summary>
        /// <param name="bytea"></param>
        /// <returns></returns>
        public static string ByteaToString(byte[] bytea)
        {
            // https://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file
            return System.BitConverter.ToString(bytea).Replace("-", "").ToLower();
        }

    }
}
