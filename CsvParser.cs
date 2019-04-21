﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TimtableFH
{
    static class CsvParser
    {
        public async static Task<IEnumerable<Event>> GetEvents(StorageFile file)
        {
            Encoding encoding = Encoding.ASCII;
            byte[] fileData = await GetBytes(file);
            string data = Encoding.ASCII.GetString(fileData, 0, fileData.Length);

            data = ReplaceQuestionMarks(data);

            return GetCsvLines( data).Select(Event.GetFromCSV);
        }

        private async static Task<byte[]> GetBytes(StorageFile file)
        {
            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                List<byte> buffer = new List<byte>();

                int count;

                while (true)
                {
                    byte[] bytes = new byte[1024];
                    count = await stream.ReadAsync(bytes, 0, bytes.Length);

                    if (count == 0) return buffer.ToArray();

                    buffer.AddRange(bytes.Take(count));
                }
            }
        }

        private static string ReplaceQuestionMarks(string input)
        {
            return input.Replace("Heidemarie K?llinger", "Heidemarie Köllinger")
                .Replace("H?rsaal", "Hörsaal")
                .Replace("M?sl?m Atas", "Müslüm Atas")
                .Replace("Stefan Gr?nwald", "Stefan Grünwald")
                .Replace("Einf?hrung, Hausf?hrung", "Einführung, Hausführung")
                .Replace("?KOGL", "ÖKOGL");
        }

        public static IEnumerable<Dictionary<string, string>> GetCsvLines(string data)
        {
            string[] lines = data.Split('\n').Select(l => l.TrimEnd('\r')).ToArray();
            string[] headers = Split(lines[0]).ToArray();

            for (int i = 1; i < lines.Length; i++)
            {
                int headerIndex = 0;
                Dictionary<string, string> dict = new Dictionary<string, string>();

                foreach (string value in Split(lines[i]))
                {
                    dict.Add(headers[headerIndex++], value);
                }

                yield return dict;
            }
        }

        private static IEnumerable<string> Split(string line)
        {
            int index = 0;

            while (index < line.Length)
            {
                bool withQuotation;
                string value = string.Empty;

                if (line[index] == '\"')
                {
                    withQuotation = true;
                    index++;
                }
                else withQuotation = false;

                while (index < line.Length)
                {
                    if (withQuotation && line[index] == '\"')
                    {
                        index++;
                        break;
                    }
                    else if (!withQuotation && line[index] == ',') break;

                    value += line[index++];
                }

                if (index < line.Length)
                {
                    if (line[index] != ',') throw new Exception();
                    else index++;
                }

                yield return value;
            }
        }
    }
}
