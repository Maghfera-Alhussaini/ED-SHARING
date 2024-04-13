using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.Data;
using MVC_CORE.Model;
using MVC_CORE.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;

namespace MVC_CORE.Controllers
{
    public class MyController : Controller
    {
        private readonly ApplicationDbContext context;

        public MyController(ApplicationDbContext context)
        {
            this.context = context;
        }
        static string x;
        static int y;
        static int data_id;
        static int r = 0;
        public string LoggedInUser => User.Identity.Name;
     

        public IActionResult watch(int id)
        {
            var result = context.Student_Videos.Where(x => x.Id == id).Single();
            ViewBag.q = result;

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();



          
        }
        public async Task upload_image(List<IFormFile> image)
        {
        
           
            

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var file in image)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profile");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);


                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                }
                var result = context.users.Where(x => x.username == LoggedInUser).Single();
                result.image = file.FileName;

                context.Update(result);
                await context.SaveChangesAsync();
            }

            RedirectToAction("Home");




        }
        

        public IActionResult codeeditor()
        {


            return View();
        }
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(string name, string email,string subject,string message)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }
            context.contacts.Add(new Contact()
            {
               user_name = name,
               email = email,
               subject = subject,
               message=message
            });
            context.SaveChanges();
            return View();


           
        }
        public IActionResult CourseDescription(int id)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }
            var result = context.courses.Where(x => x.Id == id).ToList();
            return View(result);

           
        }
        public IActionResult Home()
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }
            ViewBag.result = context.courses.OrderByDescending(x => x.Rate).ToList();
            
            return View(context.Topics.ToList());
        }





       






        public IActionResult Course(int id)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }
            var result1 = context.Topics.ToList();
            ViewBag.x = result1;
            if (id != 0)
            {
                var result = context.courses.Where(x => x.topic_id == id).ToList();
                return View(result);
            }
            else
            {
                var result = context.courses.ToList();
                return View(result);

            }
        }
       
        public IActionResult re_sort()
        {

            //display all videos in a course clicked on
            var result = context.courses.OrderByDescending(x => x.Rate).ToList();
            return View(result);
        }


       

       public IActionResult cirti(int coursid)
        {
            string username = LoggedInUser;
            var v = context.courses.Where(x => x.Id == coursid).FirstAsync();
            ViewBag.x = v.Result.Title;
            ViewBag.y = username;
            return View();
        }

            public IActionResult result(int qid, int q, string ans)
        {
            string username = LoggedInUser;
            float i=0;
            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }


          
            var result2 = context.questions.Where(x => x.Id == qid).Where(x => x.CourseId == q).Single();
           
          



            if (ans == result2.Questions_anstrue)
            {
                r = r + 1;


                if (r >= 3)
                {
                     i = (r / 3) * 100;
                  

                }
            }
            else
            {

              

            }
            if(i>60)
            {
                ViewBag.m = "success";
            }
            else
            {

                ViewBag.m = "feild";

            }

            ViewBag.q = q;

            ViewBag.t = i;
            return View();
        }

        public IActionResult exam(int qid,int q,string ans)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }

            ViewBag.q = q;
            var result2 = context.questions.Where(x => x.Id == qid).Where(x => x.CourseId == q).Single();
            ViewBag.qid = result2.Id;
            var result = context.questions.Where(x => x.Id == qid).Where(x => x.CourseId == q).ToList();
            
            


            return View(result);
        }
        public IActionResult exam1(int qid, int q, string ans)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }

            ViewBag.q = q;
            var result2 = context.questions.Where(x => x.Id == qid).Where(x => x.CourseId == q).Single();
            ViewBag.qid = result2.Id + 1;
            var result = context.questions.Where(x => x.Id == qid+1).Where(x => x.CourseId == q).ToList();

           

                if (ans == result2.Questions_anstrue)
                {
                    r = r + 1;


                   
                }
                else
                {

                    //  return RedirectToAction("result?r=Faild");

                }



           



            return View(result);
        }
        public IActionResult exam2(int qid, int q, string ans)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }

            ViewBag.q = q;
            var result2 = context.questions.Where(x => x.Id == qid).Where(x => x.CourseId == q).Single();
            ViewBag.qid = result2.Id + 1;
            var result = context.questions.Where(x => x.Id == qid+1).Where(x => x.CourseId == q).ToList();

          

                if (ans == result2.Questions_anstrue)
                {
                    r = r + 1;


                }
                else
                {

                    //  return RedirectToAction("result?r=Faild");

                }



          



            return View(result);
        }
        public async Task<IActionResult> videos_course(int co,int num)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }

            ViewBag.numofvideo = num;

            ViewBag.e = context.Student_Videos.OrderBy(x => x.Num).Where(x => x.CourseId == co).ToList();
            var result2 = context.videos.Where(x => x.Num == num).Where(x => x.CourseId == co).Single();
            int idvideo = result2.Id;
            ViewBag.q = result2;

            ViewBag.idv = idvideo;

            ViewBag.k = context.comments.Where(x => x.VideoId == idvideo).ToList();

            var t = context.courses.Where(x => x.Id == co).Single();
            ViewBag.f = Convert.ToInt32(t.Id);

            var x = context.courses.Where(x => x.Id == co).ToList();
            ViewBag.c = x;



            return View(context.videos.OrderBy(x => x.Num).Where(x => x.CourseId == co).ToList());

        }

        public async Task<IActionResult> Svideos_course(int co, int num)
        {
            string username = LoggedInUser;

            if (username != null)
            {
                var v = context.users.Where(x => x.username == username).FirstAsync();
                ViewBag.userrol = v.Result.role;
                ViewBag.image = v.Result.image;

            }
            try {


            var result2 = context.Student_Videos.Where(x => x.Num == num).Single();
            int idvideo = result2.Id;
            ViewBag.q = result2;

            ViewBag.idv = idvideo;
                

            var t = context.courses.Where(x => x.Id == co).Single();
            ViewBag.f = Convert.ToInt32(t.Id);

            var x = context.courses.Where(x => x.Id == co).ToList();
            ViewBag.c = x;
            }
         
            catch(Exception)
            {  }
           

            return View(context.Student_Videos.OrderBy(x => x.Num).Where(x => x.CourseId == co).ToList());
        }



        public async Task<IActionResult> AddCommentAsync(string comment, int id, int courseId,int num)
        {
           
           
            var input = new ModelInput();
            input.Col0 = comment;
            var predictionResult = ConsumeModel.Predict(input);

            if (Convert.ToInt16(predictionResult.Prediction) == 1)
            {

                var v =  context.videos.Where(x => x.Id == id).Single();
                v.Rate = v.Rate + 1;
                context.Update(v);
                await context.SaveChangesAsync();
                var vcourse =  context.courses.Where(x => x.Id == courseId).Single();
                vcourse.Rate = vcourse.Rate +1;
               context.Update(vcourse);
                await context.SaveChangesAsync();
                    



             }
            
            else
            {



            }



            context.comments.Add(new Comments()
            {
                VideoId=id,
                username = LoggedInUser,
                Contents = comment
            });
            context.SaveChanges();
            return Redirect("~/My/videos_course/?co=" + (courseId).ToString()+ "&num="+ (num).ToString());
            }
           
        }





  
    }
      


