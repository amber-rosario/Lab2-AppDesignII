using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGadgetRepository repository;
        public CartController(IGadgetRepository repo)
        {
            repository = repo;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int gadgetID, string returnUrl)
        {
            Gadget gadget = repository.Gadgets.FirstOrDefault
                                              (g => g.GadgetID == gadgetID);

            if (gadget != null)
            {
                GetCart().AddItem(gadget, 1);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int gadgetID, string returnUrl)
        { 
            Gadget gadget = repository.Gadgets.FirstOrDefault
                                                 (g => g.GadgetID == gadgetID);
        if (gadget != null)
        {
            GetCart().RemoveLine(gadget);
        }
        return RedirectToAction("Index", new { returnUrl });

        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl

            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}