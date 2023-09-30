using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
//Tao 1 doi tuong chua toan bo CSDL tu bdSachOnline

dbSachOnlineDataContext data = new dbSachOnlineDataContext();
        /// <summary>
        /// LaySachMoi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        private List <SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a =>a.NgayCapNhat).Take(count).ToList();
        }
        // GET: SachOnline
        public ActionResult Index()
        {
            //Lay 6 quyen sach moi
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
       
    }
}