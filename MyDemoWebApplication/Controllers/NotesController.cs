using DemoProject.BusinessService;
using DemoProject.Interface;
using MyDemoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDemoWebApplication.Controllers
{
    public class NotesController : Controller
    {
        [HttpPost]
        public ActionResult Notes(string Note, int NoteID, bool isUpdate)
        {
            if (Session["userid"] == null || string.IsNullOrWhiteSpace(Session["userid"].ToString()))
            {
                ViewBag.error = "Invalid data";
                return RedirectToAction("NoteList");
            }
            NotesModel objNotesModel = new NotesModel();
            if (isUpdate)
            {
                objNotesModel.Note = Note;
                objNotesModel.NoteID = NoteID;
            }

            return View("Notes", objNotesModel);
        }

        [HttpGet]
        public ActionResult NoteList()
        {
            if (Session["userid"] == null || string.IsNullOrWhiteSpace(Session["userid"].ToString()))
            {
                ViewBag.error = "Invalid Login";
                return RedirectToAction("NoteList");
            }
            NotesViewModel notesViewModel = getNotesViewModel();

            return View("NoteList", notesViewModel);
        }

        private NotesViewModel getNotesViewModel()
        {
            NotesViewModel notesViewModel = new NotesViewModel();
            notesViewModel.NotesList = null;
            List<NotesData> objNotes = NotesData.GetNoteList(Convert.ToInt32(Session["userid"]));
            if (objNotes != null)
            {
                for (int i = 0; i < objNotes.Count; i++)
                {
                    NotesModel notesModel = new NotesModel();
                    notesModel.Note = objNotes[i].UserNote;
                    notesModel.NoteID = objNotes[i].NoteID;
                    notesModel.UpdatedDate = objNotes[i].UpdatedDate;
                    notesModel.CreatedDate = objNotes[i].CreatedDate;
                    notesModel.UserName = Session["username"].ToString();

                    if (notesViewModel.NotesList == null) new List<NotesModel>();
                    notesViewModel.NotesList.Add(notesModel);
                }
            }

            return notesViewModel;
        }

        [HttpPost]
        public ActionResult AddNote(NotesModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Note))
            {
                ViewBag.error = "Empty Note";
                return View("NoteList", getNotesViewModel());
            }

            NotesData notesData = new NotesData();
            notesData.UserID = Convert.ToInt32(Session["userid"]);
            notesData.UserNote = model.Note;
            notesData.NoteID = model.NoteID;

            try
            {
                ((IPersistable)notesData).Save();
            }
            catch (Exception ex)
            {
                ViewBag.error = "Error in Save : " + ex.ToString();
                return View("NoteList", getNotesViewModel());
            }

            return View("NoteList", getNotesViewModel());
        }

        [HttpPost]
        public ActionResult DeleteNote(int NoteID)
        {
            if (NoteID <= 0)
            {
                ViewBag.error = "Invalid Note";
                return View("NoteList", getNotesViewModel());
            }

            NotesData notesData = new NotesData();
            notesData.NoteID = NoteID;
            try
            {
                notesData.Delete();
            }
            catch (Exception ex)
            {
                ViewBag.error = "Error in Delete : " + ex.ToString();
                return RedirectToAction("NoteList", getNotesViewModel());
            }

            return View("NoteList", getNotesViewModel());
        }

    }
}