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
    /// This class provide read methods of notes
    /// </summary>
    public class ReadNotes : StandardDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">DataController object</param>
        public ReadNotes(DataController data)
            : base(data)
        {
        }

        /// <summary>This class return notes object based on noteID</summary>
        /// <param name="noteID">int</param>
        /// <returns>User object</returns>
        public static NotesData ReadSpecificNote(int noteID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConnectionString.GetConnectionString());
            SqlCommand myCommand = new SqlCommand("spGetSpecificNote", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter param = new SqlParameter("@intNoteID", SqlDbType.Int, 4);
            param.Value = noteID;
            myCommand.Parameters.Add(param);

            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            NotesData data = null;

            try
            {
                if (myReader.Read())
                {
                    data = new NotesData();
                    data.NoteID = (int)myReader["intNoteID"];
                    data.UserID = (int)myReader["intUserID"];
                    data.UserNote = (string)myReader["strUserNote"];
                    data._createdDate = (DateTime)myReader["dteCreatedDate"];
                    data._updatedDate = (DateTime)myReader["dteUpdatedDate"];
                }
            }
            finally
            {
                if (myReader != null) myReader.Close();
            }
            return data;
        }

        /// <summary>This class return notes object based on noteID</summary>
        /// <param name="noteID">int</param>
        /// <returns>User object</returns>
        public static List<NotesData> ReadNoteList(int userID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConnectionString.GetConnectionString());
            SqlCommand myCommand = new SqlCommand("spGetNotesList", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter param = new SqlParameter("@intUserID", SqlDbType.Int, 4);
            param.Value = userID;
            myCommand.Parameters.Add(param);

            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            List<NotesData> list = null;
            NotesData data = null;

            try
            {
                while (myReader.Read())
                {
                    if (list == null) list = new List<NotesData>();
                    data = new NotesData();
                    data.NoteID = (int)myReader["intNoteID"];
                    data.UserID = (int)myReader["intUserID"];
                    data.UserNote = (string)myReader["strUserNote"];
                    data._createdDate = (DateTime)myReader["dteCreatedDate"];
                    data._updatedDate = (DateTime)myReader["dteUpdatedDate"];

                    list.Add(data);
                }
            }
            finally
            {
                if (myReader != null) myReader.Close();
            }
            return list;
        }
    }
}
