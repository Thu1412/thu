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
    public class KhachHangController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");

        // GET: Admin/KhachHang
        public ActionResult Index(int? Page)
        {
            int iPageNum = (Page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderByDescending(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f)
        {
            kh.HoTen = f["sHoTen"];
            kh.TaiKhoan = f["sTaiKhoan"];
            kh.MatKhau = f["sMatKhau"];
            kh.Email = f["sEmail"];
            kh.DiaChi = f["sDiaChi"];
            kh.DienThoai = f["sDienThoai"];
            kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
            db.KHACHHANGs.InsertOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
            // return View();
        }
        public ActionResult Details(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));
            khachhang.HoTen = f["sHoTen"];
            khachhang.TaiKhoan = f["sTaiKhoan"];
            khachhang.MatKhau = f["sMatKhau"];
            khachhang.Email = f["sEmail"];
            khachhang.DiaChi = f["sDiaChi"];
            khachhang.DienThoai = f["sDienThoai"];
            khachhang.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
            db.SubmitChanges();
            return RedirectToAction("Index");
            return View(khachhang);
        }
        public ActionResult Delete(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ddh = db.DONDATHANGs.Where(vs => vs.MaKH == id).ToList();
            if (ddh != null)
            {
                db.DONDATHANGs.DeleteAllOnSubmit(ddh);
                db.SubmitChanges();
            }
            db.KHACHHANGs.DeleteOnSubmit(khachhang);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}