using System.Collections.Generic;
using Illuminus.Core.DomainService;
using Illuminus.Core.Entity;

namespace Illuminus.Core.ApplicationService.Services
{
    public class VideoService: IVideoService
    {
        private readonly IVideoRepository videoRepository;


        public VideoService(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        public void CreateVideo(string name, string genre)
        {
            videoRepository.CreateVideo(name, genre);
        }

        public void RemoveVideo(int id)
        {
            videoRepository.RemoveVideo(id);
        }

        public void UpdateVideo(int id, string name, string genre)
        {
            videoRepository.UpdateVideo(id, name, genre);
        }

        public List<Video> ReadAllVideos()
        {
            return videoRepository.ReadAllVideos();
        }

        public List<Video> SearchVideos(List<Video> allVideos, string filter)
        {
            List<Video> listToReturn = new List<Video>();
            foreach (Video v in allVideos)
            {
                if (v.Name.Contains(filter))
                {
                    listToReturn.Add(v);
                }
            }
            return listToReturn;
        }
    }
}
