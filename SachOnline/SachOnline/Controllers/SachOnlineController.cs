using System;
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

        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");
        /// <summary>
        /// LaySachMoi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        private List <SACH> LaySachMoi(int count)
        {
            return db.SACHes.OrderByDescending(a =>a.NgayCapNhat).Take(count).ToList();
        }
        // GET: SachOnline
        public ActionResult Index()
        {
            //Lay 6 quyen sach moi
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);
        }

        public ActionResult SachTheoChuDe(int id)

        {

            var sach = from s in db.SACHes where s.MaCD == id select s;
            return View(sach);

           
 
        }
        
        public ActionResult ChiTietSach(int id)

        {

            var sach = from s in db.SACHes

                       where s.MaSach == id select s;
            return View(sach.Single());
        }

        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in db.CHUDEs select cd;
           return PartialView(listChuDe);
            
        }

        public ActionResult NhaXuatBanPartial()
        {
            var listNhaXB = from xb in db.NHAXUATBANs select xb;
            return PartialView(listNhaXB);
        }
        public ActionResult SachTheoNhaXuatBan(int id)

        {

            var sach = from s in db.SACHes where s.MaNXB == id select s;
            return View(sach);

        }

        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = from bn in db.SACHes select bn;
             return PartialView(listSachBanNhieu);
            
        }
       
        public  ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SiderPartial()
        {
            return PartialView();
        }

    }
}