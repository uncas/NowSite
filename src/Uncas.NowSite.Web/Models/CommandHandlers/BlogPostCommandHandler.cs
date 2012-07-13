using System;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using Uncas.NowSite.Web.Models.Aggregates;

namespace Uncas.NowSite.Web.Models.CommandHandlers
{
    public abstract class BlogPostCommandHandler<T> :
       CommandHandler<T> where T : ICommand
    {
        protected readonly IDomainRepository _domainRepository;

        protected BlogPostCommandHandler(
            IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        /// <summary>
        /// Acts on existing blog post.
        /// </summary>
        /// <param name="id">The id of the blog post.</param>
        /// <param name="action">The action.</param>
        protected void ActOnExisting(
            Guid id,
            Action<BlogPost> action)
        {
            var blogPost = _domainRepository.GetById<BlogPost>(id);
            action(blogPost);
            _domainRepository.Save(blogPost);
        }
    }
}