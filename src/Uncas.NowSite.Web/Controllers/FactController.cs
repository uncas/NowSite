using System;
using System.Web.Mvc;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Domain.Facts.Commands;
using Uncas.NowSite.Domain.Infrastructure;
using Uncas.NowSite.Domain.ReadModels;

namespace Uncas.NowSite.Web.Controllers
{
    public class FactController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadStore _readStore;

        public FactController(
            ICommandBus commandBus,
            IReadStore readStore)
        {
            _commandBus = commandBus;
            _readStore = readStore;
        }

        public ActionResult Index()
        {
            var facts = _readStore.GetAll<FactReadModel>();
            return View(facts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateFactCommand command)
        {
            command.Id = Guid.NewGuid();
            _commandBus.Send(command);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var fact = _readStore.GetById<FactReadModel>(id);
            return View(fact);
        }

        [HttpPost]
        public ActionResult Edit(EditFactCommand command)
        {
            _commandBus.Send(command);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(DeleteFactCommand command)
        {
            _commandBus.Send(command);
            return RedirectToAction("Index");
        }
    }
}