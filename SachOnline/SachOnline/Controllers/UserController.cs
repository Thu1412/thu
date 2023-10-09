using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Controllers
{
    public class UserController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext("Data Source=XIUXIUTHW\\MAYAO;Initial Catalog=SachOnline;Integrated Security=True");
        

        // GET: User
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var stenDN = collection["TenDN"];
            var sMatKhau = collection["Matkhau"];
            var sMatkhauNhapLai = collection["MatKhauNL"];
            var sDiachi = collection["Diachi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }

            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err5"] = "Email không được rỗng";
            }

            else if (string.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err6"] = "Số điện thoại không được rỗng";
            }

            else if (db.KHACHHANGs(n => n.Taikhoan == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }

            else if (db.KHACHHANGS.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";

            }
            else
            {
                //Gần giá trị cho đối tượng được tạo mới (kh)
                kh.HoTen sHoTen;
                kh.TaiKhoan sTenDN;
                kh.MatKhau = sMatKhau;
                kh.Email sEmail;
                kh.Diachi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANGS.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();





        }
    }
}