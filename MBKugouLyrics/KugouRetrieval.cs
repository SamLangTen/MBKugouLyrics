using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace MusicBeePlugin
{
    class KugouRetrieval
    {

        public static string KgMid { get; set; }

        public static string Search(string title, string artist)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");
                var data = client.DownloadData($"https://songsearch.kugou.com/song_search_v2?keyword={title} {artist}");
                var text = System.Text.Encoding.UTF8.GetString(data);
                var json = JObject.Parse(text);
                if (json["data"]["lists"].HasValues)
                {
                    return json["data"]["lists"][0]["FileHash"].Value<string>();
                }
            }
            return null;
        }

        public static string GetLyrics(string filehash)
        {
            using (var client = new WebClient())
            {

                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");
                client.Headers.Add(HttpRequestHeader.Cookie, $"kg_mid={KgMid};kg_mid_temp={KgMid}");

                var data = client.DownloadData($"https://wwwapi.kugou.com/yy/index.php?r=play/getdata&hash={filehash}");
                var text = System.Text.Encoding.UTF8.GetString(data);
                var json = JObject.Parse(text);
                if (json["err_code"].Value<int>() != 0)
                {
                    throw new KugouSeverException(json["err_code"].Value<int>());
                }
                var lyrics = json["data"]["lyrics"].Value<string>();
                if (lyrics == null) return null;
                //Remove tag
                var r = new Regex("\\ufeff\\[id[^\\]]*\\]\\r\\n");
                lyrics = r.Replace(lyrics, "");
                r = new Regex("\\[hash[^\\]]*\\]\\r\\n");
                lyrics = r.Replace(lyrics, "");
                r = new Regex("\\[sign[^\\]]*\\]\\r\\n");
                lyrics = r.Replace(lyrics, "");
                r = new Regex("\\[qq[^\\]]*\\]\\r\\n");
                lyrics = r.Replace(lyrics, "");
                r = new Regex("\\[total[^\\]]*\\]\\r\\n");
                lyrics = r.Replace(lyrics, "");
                lyrics = Regex.Unescape(lyrics);
                return lyrics;
            }
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GeneratedGuid()
        {
            Func<string> S4 = () => { return Convert.ToString((Convert.ToInt32(Math.Round((1 + new Random().NextDouble()) * 0x10000)) | 0), 16).Substring(1); };
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }

    }
}

