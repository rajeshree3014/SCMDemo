using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDemoWebApplication.Models
{
    public class NotesViewModel
    {
        public List<NotesModel> NotesList { get; set; }
    }

    public class NotesModel
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Note Required")]
        [DataType(DataType.Text)]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Invalid Length of Note")]
        public string Note { get; set; }

        public int NoteID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}