﻿using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using VinculacionBackend.Database;
using VinculacionBackend.Entities;
using VinculacionBackend.Enums;

namespace VinculacionBackend.Controllers
{
    public class StudentsController : ApiController
    {
        private VinculacionContext db = new VinculacionContext();

        // GET: api/Students
        public IQueryable<User> GetStudents()
        {
            var userRoleRels = db.UserRoleRels.Include(x=>x.Role).Include(y=>y.Student).Where(z=>z.Role.Name == "Student");

            return db.Users.Where(x=> userRoleRels.Any(y=>y.Student.Id == x.Id));
        }

        // GET: api/Students/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetStudent(string id)
        {
            User User = db.Users.Include(x=>x.Roles).FirstOrDefault(y=>y.Roles.Any(z=>z.Name == "Student") && y.IdNumber == id);
            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(string id, User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != User.IdNumber)
            {
                return BadRequest();
            }

            var existEmail = EntityExistanceManager.EmailExists(User.Email);
            if (existEmail)
            {
                return InternalServerError(new Exception("Email already exists in database"));
            }

            var tmpUser = db.Users.FirstOrDefault(x => x.IdNumber== id);
            tmpUser.ModificationDate=DateTime.Now;
            tmpUser.Password = User.Password;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError(new DbUpdateConcurrencyException());
                }
            }

            return Ok(tmpUser);
        }

        // POST: api/Students
        [ResponseType(typeof(User))]
        public IHttpActionResult PostStudent(User User)
        {
            if (!ModelState.IsValid || User == null)
            {
                return BadRequest(ModelState);
            }

            var existEmail = EntityExistanceManager.EmailExists(User.Email);
            if (existEmail)
            {
                return InternalServerError(new Exception("Email already exists in database"));
            }

            User.Status=Status.Inactive;
            db.Users.Add(User);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = User.IdNumber }, User);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteStudent(string id)
        {
            User User = db.Users.FirstOrDefault(x=>x.IdNumber==id);
            if (User == null)
            {
                return NotFound();
            }

            db.Users.Remove(User);
            db.SaveChanges();

            return Ok(User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(string id)
        {
            return db.Users.Count(e => e.IdNumber == id) > 0;
        }





    }

}