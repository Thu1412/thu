using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");

        // GET: Admin/NhaXuatBan
        public ActionResult Index(int? Page)
        {
            int iPageNum = (Page ?? 1);
            int iPageSize = 5;
            return View(db.NHAXUATBANs.ToList().OrderByDescending(n => n.MaNXB).ToPagedList(iPageNum, iPageSize));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN nhaxuatban, FormCollection f)
        {
            nhaxuatban.TenNXB = f["sTenNXB"];
            nhaxuatban.DiaChi = f["sDiaChi"];
            nhaxuatban.DienThoai = f["sDienThoai"];
            db.NHAXUATBANs.InsertOnSubmit(nhaxuatban);
            db.SubmitChanges();
            return RedirectToAction("Index");
            //return View();
        }
        public ActionResult Details(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
        public ActionResult Delete(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(vs => vs.MaNXB == id).ToList();
            if (sach != null)
            {
                db.SACHes.DeleteAllOnSubmit(sach);
                db.SubmitChanges();
            }
            db.NHAXUATBANs.DeleteOnSubmit(nhaxuatban);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Hiển thị danh sách chủ đề và nhà xuất bản đồng thời chọn chủ đề và nhà xuất bản của cuốn hiện tại
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNhaXuatBan", nhaxuatban.MaNXB);
            return View(nhaxuatban);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == int.Parse(f["iMaNXB"]));
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", nhaxuatban.MaNXB);
            nhaxuatban.TenNXB = f["sTenNXB"];
            nhaxuatban.DiaChi = f["sDiaChi"];
            nhaxuatban.DienThoai = f["sDienThoai"];
            db.SubmitChanges();
            return RedirectToAction("Index");
            // return View(nhaxuatban);
        }

    }
}
