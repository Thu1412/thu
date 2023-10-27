using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class DonDatHangController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");

        // GET: Admin/DonDatHang
        public ActionResult Index(int? Page)
        {
            int iPageNum = (Page ?? 1);
            int iPageSize = 7;
            return View(db.DONDATHANGs.ToList().OrderByDescending(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONDATHANG ddh, FormCollection f)
        {
            ddh.DaThanhToan = bool.Parse(f["DaThanhToan"]);
            ddh.TinhTrangGiaoHang = int.Parse(f["iTinhTrangGiaoHang"]);
            ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
            ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
            ddh.MaKH = int.Parse(f["iMaKH"]);
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            return RedirectToAction("Index");
            //return View();
        }
        public ActionResult Details(int id)
        {
            //ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == int.Parse(f["iMaDonHang"]));
            ddh.DaThanhToan = bool.Parse(f["DaThanhToan"]);
            ddh.TinhTrangGiaoHang = int.Parse(f["iTinhTrangGiaoHang"]);
            ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
            ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
            ddh.MaKH = int.Parse(f["iMaKH"]);
            //db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            return RedirectToAction("Index");
            //return View();
        }
        public ActionResult Delete(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var ctdh = db.CHITIETDATHANGs.Where(vs => vs.MaDonHang == id).ToList();
            if (ctdh != null)
            {
                db.CHITIETDATHANGs.DeleteAllOnSubmit(ctdh);
                db.SubmitChanges();
            }
            db.DONDATHANGs.DeleteOnSubmit(ddh);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
    }
}