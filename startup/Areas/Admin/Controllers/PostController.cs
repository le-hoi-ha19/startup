using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DataContext _context;
        public PostController(DataContext context)
        {
            _context = context;
        }

        //lấy các danh sách post từ database và truyền dữ liệu qua file index.cshtml của thư mục Menu
        public IActionResult Index()
        {
            var postList = _context.Posts.OrderBy(m => m.PostID).ToList();
            return View(postList);
        }


        //Hiển thị Trang Thêm mới  post
        public IActionResult Create()
        {
            var postList = (from m in _context.Posts
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.PostID.ToString(),
                          }).ToList();
           postList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = postList;
            return View();
        }
        // xử lý dữ liệu khi người dùng gửi lên 1 request bằng phương thức post
        [HttpPost]
        
        public IActionResult Create(Post post)
        {
            //Validate dữ liệu xem dữ liệu nhập vào đúng k
            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
            }
             return RedirectToAction("Index");
        }

        // Hiển thị trang chỉnh sửa 1 bài viết
        public IActionResult Edit(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var postList = (from m in _context.Posts
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.PostID.ToString(),
                          }).ToList();
            postList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.postList = postList;
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Update(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        // Hiển thị trang xóa 1 bài viết
        public IActionResult Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Posts.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        // 
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var delePost = _context.Posts.Find(id);
            if (delePost == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(delePost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
