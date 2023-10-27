using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");

        // GET: Admin/ChuDe
        public ActionResult Index(int? Page)
        {
            int iPageNum = (Page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderByDescending(n => n.MaCD).ToPagedList(iPageNum, iPageSize));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE chude, FormCollection f)
        {
            chude.TenChuDe = f["sTenChuDe"];
            db.CHUDEs.InsertOnSubmit(chude);
            db.SubmitChanges();
            return RedirectToAction("Index");
            //return View();
        }
        public ActionResult Details(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        public ActionResult Delete(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(vs => vs.MaCD == id).ToList();
            if (sach != null)
            {
                db.SACHes.DeleteAllOnSubmit(sach);
                db.SubmitChanges();
            }
            db.CHUDEs.DeleteOnSubmit(chude);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Hiển thị danh sách chủ đề và nhà xuất bản đồng thời chọn chủ đề và nhà xuất bản của cuốn hiện tại
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", chude.MaCD);
            return View(chude);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == int.Parse(f["iMaCD"]));
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", chude.MaCD);
            chude.TenChuDe = f["sTenChuDe"];
            db.SubmitChanges();
            return RedirectToAction("Index");
            //return View(chude);
        }


    }
}