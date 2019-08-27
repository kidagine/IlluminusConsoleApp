using Illuminus.Core.Entity;
using System.Collections.Generic;

namespace Illuminus.Core.DomainService
{
    public interface IVideoRepository
    {
        void CreateVideo(string name, string genre);
        void RemoveVideo(int id);
        void UpdateVideo(int id, string name, string genre);
        List<Video> ReadAllVideos();
    }
}
