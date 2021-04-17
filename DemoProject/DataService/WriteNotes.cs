using DemoProject.BusinessService;
using DemoProject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.DataService
{
    /// <summary>
    /// This class provide write methods for Notes
    /// </summary>
	public class WriteNotes : StandardDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">DataController object</param>
		public WriteNotes(DataController data) : base(data)
        {
        }


        /// <summary>This method save Note details</summary>
        /// <param name="data">Notes object</param>
        public void WriteNotesDetails(NotesData data)
        {
            SqlCommand myCommand = new SqlCommand("spSaveNotes");

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC		
            SqlParameter paramNoteID = new SqlParameter("@intNoteID", SqlDbType.Int, 4);
            paramNoteID.Value = data.NoteID;
            paramNoteID.Direction = ParameterDirection.InputOutput;
            myCommand.Parameters.Add(paramNoteID);

            SqlParameter paramUserNote = new SqlParameter("@strUserNote", SqlDbType.NVarChar, 500);
            paramUserNote.Value = data.UserNote;
            myCommand.Parameters.Add(paramUserNote);

            SqlParameter paramUserId = new SqlParameter("@intUserID", SqlDbType.Int, 4);
            paramUserId.Value = data.UserID;
            myCommand.Parameters.Add(paramUserId);

            try
            {
                this.Controller.AddCommand(myCommand);
                myCommand.ExecuteNonQuery();

                data.NoteID = (int)paramNoteID.Value;
            }
            finally
            {
                myCommand.Dispose();
            }
        }

        /// <summary>This method delete Notes detail for given noteID
        /// </summary>
        /// <param name="noteID">int</param>
        public void DeleteNoteDetails(int noteID)
        {
            SqlCommand myCommand = new SqlCommand("spDeleteNotes");

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC            
            SqlParameter paramNoteID = new SqlParameter("@intNoteID", SqlDbType.Int, 4);
            paramNoteID.Value = noteID;
            myCommand.Parameters.Add(paramNoteID);

            try
            {
                this.Controller.AddCommand(myCommand);
                myCommand.ExecuteNonQuery();
            }
            finally
            {
                myCommand.Dispose();
            }
        }
    }
}
