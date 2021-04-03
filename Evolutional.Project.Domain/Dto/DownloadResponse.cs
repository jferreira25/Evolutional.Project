using System;

namespace Evolutional.Project.Domain.Dto
{
    public class DownloadResponse
    {
        public DownloadResponse(byte[] dataToDownload, string mimeType, string fileName)
        {
            DataToDownload = dataToDownload;
            MimeType = mimeType;
            FileName = $"{fileName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
        }

        public byte[] DataToDownload { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
    }
}
