using System;
using System.Collections.Generic;
using System.IO;
using Illuminus.Core.DomainService;
using Illuminus.Core.Entity;

namespace Illuminus.Infastructure.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly string FILEPATHVIDEOS = AppContext.BaseDirectory + "\\TxtFiles\\VideosText.txt";

        public void CreateVideo(string name, string genre)
        {
            Video video = new Video(GetNextVideoId(), name, genre);
            string videoLine = video.Id.ToString() + "|" + video.Name + "|" + video.Genre;
            File.AppendAllText(FILEPATHVIDEOS, videoLine + Environment.NewLine);
        }

        public void RemoveVideo(int id)
        {
            using (var sr = new StreamReader(FILEPATHVIDEOS))
            using (var sw = new StreamWriter("tempFile.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');
                    if (lines[0] != id.ToString())
                    {
                        if (id < int.Parse(lines[0]))
                        {
                            int orderedId = int.Parse(lines[0]) - 1;
                            string lineId = orderedId.ToString();
                            line = lineId + "|" + lines[1] + "|" + lines[2];
                            sw.WriteLine(line);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
            File.Delete(FILEPATHVIDEOS);
            File.Move("tempFile.txt", FILEPATHVIDEOS);
        }

        public void UpdateVideo(int id, string name, string genre)
        {
            Video video = new Video(id, name, genre);
            string videoLine = video.Id.ToString() + "|" + video.Name + "|" + video.Genre;
            using (var sr = new StreamReader(FILEPATHVIDEOS))
            using (var sw = new StreamWriter("tempFile.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');
                    if (lines[0] != id.ToString())
                    {
                        sw.WriteLine(line);
                    }
                    else
                    {
                        sw.WriteLine(videoLine);
                    }
                }
            }
            File.Delete(FILEPATHVIDEOS);
            File.Move("tempFile.txt", FILEPATHVIDEOS);
        }

        public List<Video> ReadAllVideos()
        {
            List<Video> videosListToReturn = new List<Video>();
            using (StreamReader srVideos = new StreamReader(FILEPATHVIDEOS))
            {
                string videoLine = "";
                while ((videoLine = srVideos.ReadLine()) != null)
                {
                    string[] videoLines = videoLine.Split('|');
                    Video video = new Video(int.Parse(videoLines[0]), videoLines[1], videoLines[2]);
                    videosListToReturn.Add(video);
                }
                srVideos.Close();
            }
            return videosListToReturn;
        }

        private int GetNextVideoId()
        {
            int id = 0;
            using (StreamReader srVideos = new StreamReader(FILEPATHVIDEOS))
            {
                string videoLine = "";
                while ((videoLine = srVideos.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(videoLine))
                    {
                        string[] videoLines = videoLine.Split('|');
                        int videoId = int.Parse(videoLines[0]);
                        if (videoId >= id)
                        {
                            id = videoId;
                        }
                    }
                }
            }
            id += 1;
            return id;
        }
    }
}
