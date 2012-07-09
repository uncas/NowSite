using System;
using System.IO;
using SimpleCqrs.Commanding;

namespace Uncas.NowSite.Web.Models.Commands
{
    public class UploadPictureCommand : ICommand
    {
        public Guid PictureId { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}