using IlluminatiConsole.BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class MainModel
    {
        private readonly string FILEPATHCUSTOMERS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\CustomersText.txt";
        private readonly string FILEPATHVIDEOS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\VideosText.txt";
        private static MainModel instance = null;
        private List<Customer> customersList = new List<Customer>();


        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainModel();
                }
                return instance;
            }
        }

        public List<Video> LoadVideosList()
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

        public void EditVideo(int id, string name, string genre)
        {
            Video video = new Video(GetNextVideoId(), name, genre);
            string videoLineEdited = video.Id.ToString() + "|" + video.Name + "|" + video.Genre;

            string[] videoLines = null;
            using (StreamReader srVideos = new StreamReader(FILEPATHVIDEOS))
            {
                string videoLine = "";
                while ((videoLine = srVideos.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(videoLine))
                    {
                        videoLines = videoLine.Split('|');
                    }
                }
                srVideos.Close();
            }
            using (StreamWriter srVideos = new StreamWriter(FILEPATHVIDEOS))
            {
                for (int currentLineId = 1; currentLineId <= videoLines.Length; ++currentLineId)
                {
                    if (currentLineId == id)
                    {
                        srVideos.WriteLine(videoLineEdited);
                    }
                    else
                    {
                        srVideos.WriteLine(videoLines[currentLineId]);
                    }
                }
            }
        }

        public void AddVideo(string name, string genre)
        {
            Video video = new Video(GetNextVideoId(), name, genre);
            string videoLine = video.Id.ToString() + "|" + video.Name + "|" + video.Genre;
            File.AppendAllText(FILEPATHVIDEOS, videoLine + Environment.NewLine);
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

        public void AddCustomer(string name, string password)
        {
            Customer customer = new Customer(GetNextCustomerId(), name, password);
            string customerLine = customer.Id.ToString() + "|" + customer.Name + "|" + customer.Password;
            File.AppendAllText(FILEPATHCUSTOMERS, customerLine + Environment.NewLine);
        }

        private int GetNextCustomerId()
        {
            int id = 0;
            using (StreamReader srCustomers = new StreamReader(FILEPATHCUSTOMERS))
            {
                string customerLine = "";
                while ((customerLine = srCustomers.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(customerLine))
                    {
                        string[] customerLines = customerLine.Split('|');
                        int customerId = int.Parse(customerLines[0]);
                        if (customerId >= id)
                        {
                            id = customerId;
                        }
                    }
                }
            }
            id += 1;
            return id;
        }
    }
}
