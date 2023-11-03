using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;
using startup.Utilities;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController: Controller
    {
        private readonly DataContext _context;
        public MenuController(DataContext context)
        {
            _context = context;
        }

        //lấy các danh sách menu từ database và truyền dữ liệu qua file index.cshtml của thư mục Menu
        public IActionResult Index()
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            var mnList = _context.Menus.OrderBy(m => m.MenuID).ToList();
            return View(mnList);
        }

        //Hiển thị Trang Thêm mới  menu
        public IActionResult Create()
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }
        // xử lý dữ liệu khi người dùng gửi lên 1 request bằng phương thức post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Menu mn)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.Menus.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }

        // Hiển thị trang chỉnh sửa 1 menu
        public IActionResult Edit(int? id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View(mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu mn)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login"); 

            if (ModelState.IsValid)
            {
                _context.Menus.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }


        // Hiển thị trang xóa 1 menu
        public IActionResult Delete(int? id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            if (id == null || id==0)
            {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if(mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Thêm 2 lệnh sau vào các Action của các Controller
            // để kiểm tra trạng thái đăng nhập
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            var deleMenu = _context.Menus.Find(id);
            if(deleMenu == null)
            {
                return NotFound();
            }
            _context.Menus.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
