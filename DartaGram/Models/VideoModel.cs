using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartaGram.Models
{
    public class VideoModel
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public String lat { get; set; }
        public String lon { get; set; }
        public byte[] videoStream { get; set; }
        public String videoLink { get; set; }
    }
}
