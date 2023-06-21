﻿using SevenZip;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\Users\gavet\Documents\Test\temp_p.7z";
            var password = "";
            try
            {
                var arch = new SevenZipExtractor(File.OpenRead(filePath), password);
                arch.ArchiveProperties.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.Value}"));
                Console.WriteLine($"Contains: {arch.ArchiveFileData.Count} files");
                var enc = arch.ArchiveFileData.Any(file => file.Encrypted || file.Method.Contains("Crypto") || file.Method.Contains("AES"));
                Console.WriteLine($"Encrypted: {enc}, {arch.ArchiveProperties.FirstOrDefault(x => x.Name == "Encrypted").Value}");
                arch.ExtractFile("temp\\dxwebsetup.exe", new MemoryStream());
            }
            catch(SevenZipOpenFailedException ex)
            {
                Console.WriteLine($"Error: {ex.Result}");
            }
            catch (ExtractionFailedException ex)
            {
                Console.WriteLine($"Error: {ex.Result}");
            }
        }
    }
}