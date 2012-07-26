using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Google.GData.Photos;
using SimpleCqrs.Domain;
using Uncas.NowSite.Domain.BlogPosts.CommandHandlers;
using Uncas.NowSite.Domain.Pictures.Commands;

namespace Uncas.NowSite.Domain.Pictures.CommandHandlers
{
    public class UploadPictureCommandHandler :
        BlogPostCommandHandler<UploadPictureCommand>
    {
        public UploadPictureCommandHandler(
            IDomainRepository domainRepository) :
            base(domainRepository)
        {
        }

        public override void Handle(UploadPictureCommand command)
        {
            string pictureUrl = UploadPicture(
                command.PictureId,
                command.FileName,
                command.FileStream);
            var picture = new Picture(
                command.PictureId,
                pictureUrl);
            _domainRepository.Save(picture);
        }

        private string UploadPicture(
            Guid pictureId,
            string fileName,
            Stream fileStream)
        {
            PicasaService service = new PicasaService("NowSite");
            string userName = 
                ConfigurationManager.AppSettings["GoogleAccount.UserName"];
            string password =
                ConfigurationManager.AppSettings["GoogleAccount.Password"];
            service.setUserCredentials(
                userName,
                password);

            string albumTitle = "NowSite";
            if (!DoesAlbumExist(service, albumTitle))
            {
                CreateAlbum(service, albumTitle);
            }

            string albumId = GetAlbumId(service, albumTitle);

            return AddPhoto(
                service,
                albumId,
                fileName,
                fileStream);
        }

        private string GetAlbumId(
            PicasaService service,
            string albumTitle)
        {
            AlbumQuery query = new AlbumQuery(
                           PicasaQuery.CreatePicasaUri("default"));
            PicasaFeed feed = service.Query(query);
            foreach (PicasaEntry entry in feed.Entries)
            {
                if (albumTitle.Equals(entry.Title.Text))
                {
                    return entry.Id.AbsoluteUri.Split('/').Last();
                }
            }

            return null;
        }

        private bool DoesAlbumExist(
            PicasaService service,
            string albumName)
        {
            AlbumQuery query = new AlbumQuery(
                           PicasaQuery.CreatePicasaUri("default"));
            PicasaFeed feed = service.Query(query);
            foreach (PicasaEntry entry in feed.Entries)
            {
                if (albumName.Equals(entry.Title.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private void CreateAlbum(PicasaService service, string albumTitle)
        {
            var newEntry = new AlbumEntry();
            newEntry.Title.Text = albumTitle;
            newEntry.Summary.Text = "Album used for pictures for NowSite";
            var ac = new AlbumAccessor(newEntry);
            //set to "private" for a private album
            ac.Access = "public";
            var feedUri = new Uri(PicasaQuery.CreatePicasaUri("default"));
            var createdEntry = (PicasaEntry)service.Insert(feedUri, newEntry);
        }

        private string AddPhoto(
            PicasaService service,
            string albumId,
            string fileName,
            Stream fileStream)
        {
            var postUri = new Uri(
                PicasaQuery.CreatePicasaUri("default", albumId));
            var entry = (PicasaEntry)service.Insert(
                postUri,
                fileStream,
                "image/jpeg",
                fileName);
            fileStream.Close();
            return entry.Media.Content.Url;
        }
    }
}