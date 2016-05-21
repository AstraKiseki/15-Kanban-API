using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KanbanAPI;
using AutoMapper;
using KanbanAPI.Models;

namespace KanbanAPI.Controllers
{
    public class CardsController : ApiController
    {
        private KanbanEntities db = new KanbanEntities();
        // GET: api/Cards
        public IEnumerable<CardModel> GetCards()
        {
            return Mapper.Map<IEnumerable<CardModel>>(db.Cards);
        }

        // GET: api/Cards/5
        [ResponseType(typeof(CardModel))]
        public IHttpActionResult GetCard(int id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CardModel>(card));
        }

        // PUT: api/Cards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCard(int id, CardModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.CardID)
            {
                return BadRequest();
            }

            db.Entry(card).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cards
        [ResponseType(typeof(CardModel))]
        public IHttpActionResult PostCard(CardModel card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCard = new Card(card);
            db.Cards.Add(dbCard);
            db.SaveChanges();

            card.CreatedDate = dbCard.CreatedDate;
            card.CardID = dbCard.CardID;

            return CreatedAtRoute("DefaultApi", new { id = card.CardID }, card);
        }

        // DELETE: api/Cards/5
        [ResponseType(typeof(CardModel))]
        public IHttpActionResult DeleteCard(int id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok(Mapper.Map<CardModel>(card));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(int id)
        {
            return db.Cards.Count(e => e.CardID == id) > 0;
        }
    }
}