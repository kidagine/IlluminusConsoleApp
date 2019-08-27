using Illuminus.Core.Entity;
using System.Collections.Generic;

namespace Illuminus.Core.ApplicationService
{
    public interface IVideoService
    {
        void CreateVideo(string name, string genre);
        void RemoveVideo(int id);
        void UpdateVideo(int id, string name, string genre);
        List<Video> ReadAllVideos();
        List<Video> SearchVideos(List<Video> allVideos, string filter);
    }
}
