using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Ninject;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Moq;
using GadgetHub.Domain.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            mykernel = kernelParam;
            AddBinding();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBinding()
        {
            //Mock<IGadgetRepository> myMock = new Mock<IGadgetRepository>();
            //myMock.Setup(m => m.Gadgets).Returns(new List<Gadget>
            //{
            //    new Gadget { GadgetID = 1, Name = "Google Pixel 8", Brand = "Google", Price = 899, Description = "AI-powered smartphone with incredible camera performance", Category = "Smartphones" },
            //    new Gadget { GadgetID = 2, Name = "Garmin Fenix 7X", Brand = "Garmin", Price = 799, Description = "Rugged GPS smartwatch with solar charging for outdoor adventurers", Category = "Wearables" },
            //    new Gadget { GadgetID = 3, Name = "JBL Charge 5", Brand = "JBL", Price = 179, Description = "Portable Bluetooth speaker with 20 hours of playtime and waterproof design", Category = "Audio" },
            //    new Gadget { GadgetID = 4, Name = "GoPro Hero 11 Black", Brand = "GoPro", Price = 499, Description = "Waterproof action camera with 5.3K video and HyperSmooth stabilization", Category = "Cameras" }
            //});

            //mykernel.Bind<IGadgetRepository>().ToConstant(myMock.Object);

            mykernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();
        }
    }
}