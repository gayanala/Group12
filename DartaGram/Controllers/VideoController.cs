using DartaGram.Models;
using Newtonsoft.Json;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace DartaGram.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(VideoModel videoModel)
        {
            videoModel.VideoId = Guid.NewGuid();
            var dirPath = Server.MapPath("~/Videos/" + videoModel.UserId);
            var filePath = Server.MapPath("~/Videos/" + videoModel.UserId + "/VideosList.json");
            var tempPath = Server.MapPath("~/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + ".mp4");
            byte[] byteData = System.IO.File.ReadAllBytes(Server.MapPath("~/videoplayback.mp4"));
            videoModel.videoStream = byteData;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (System.IO.File.Exists(filePath))
            {
                var jsonData = System.IO.File.ReadAllText(filePath);
                var videoList = JsonConvert.DeserializeObject<List<VideoModel>>(jsonData)
                                      ?? new List<VideoModel>();
                videoList.Add(videoModel);
                jsonData = JsonConvert.SerializeObject(videoList);
                System.IO.File.WriteAllText(filePath, jsonData);
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                fs.Close();
                List<VideoModel> videoList = new List<VideoModel>();
                videoList.Add(videoModel);
                var jsonData = JsonConvert.SerializeObject(videoList);
                System.IO.File.WriteAllText(filePath, jsonData);
            }
            if (System.IO.File.Exists(tempPath))
            {
                System.IO.File.WriteAllBytes(tempPath, videoModel.videoStream);
            }
            else
            {
                FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate);
                fs.Close();
                System.IO.File.WriteAllBytes(tempPath, videoModel.videoStream);
            }
            string outputThumbnailPath = Server.MapPath("~/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + "_thumbnail.jpg");
            string outputGifUrl = Server.MapPath("~/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + "_gif.gif");
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.ConvertMedia(tempPath, "video.mp4", Format.mp4);
            ffMpeg.GetVideoThumbnail(tempPath, outputThumbnailPath);
            ffMpeg.ConvertMedia("video.mp4", null, outputGifUrl, null, new ConvertSettings() { VideoFrameSize = NReco.VideoConverter.FrameSize.hd480, VideoFrameCount = 10 });
            videoModel.videoLink = "http://dartagram.azurewebsites.net/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + ".mp4";
            String thumbnail = "http://dartagram.azurewebsites.net/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + "_thumbnail.jpg";
            String gifUrl = "http://dartagram.azurewebsites.net/Videos/" + videoModel.UserId + "/" + videoModel.VideoId + "_gif.gif";
            return new JsonResult { Data = "{success:true,videoLink:" + videoModel.videoLink + ",thumbnail="+thumbnail+",gifUrl="+gifUrl+"}" };
        }

    }
}