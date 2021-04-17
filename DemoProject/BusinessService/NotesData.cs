using DemoProject.Common;
using DemoProject.DataService;
using DemoProject.Interface;
using System;
using System.Collections.Generic;

namespace DemoProject.BusinessService
{
    /// <summary>This class provide Notes properties and methods
    /// </summary>
    [Serializable()]
    public class NotesData : DataItem, IPersistableV2, IDeleteable, IPersistable
    {
        internal DateTime _createdDate = DateTime.MinValue;

        internal DateTime _updatedDate = DateTime.MinValue;
        public int NoteID { get; set; }

        public int UserID { get; set; }

        public string UserNote { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return _updatedDate;
            }
        }

        /// <summary>Get or Set Status whether Note is deleted or not
        /// </summary>
        /// <value>bool</value>
        public bool Deleted { get; set; }

        /// <summary>This method return Notes object based on noteID
        /// </summary>
        /// <param name="noteID">int</param>
        /// <returns>Notes object</returns>
        public static NotesData Specific(int noteID)
        {
            return ReadNotes.ReadSpecificNote(noteID);
        }

        /// <summary>This method return Notes List object based on userID
        /// </summary>
        /// <param name="userID">int</param>
        /// <returns>Notes List object</returns>
        public static List<NotesData> GetNoteList(int userID)
        {
            return ReadNotes.ReadNoteList(userID);
        }

        void IPersistable.Save()
        {
            DataController conn = new DataController();
            try
            {
                conn.StartDatabase(ConnectionString.GetConnectionString());
                ((IPersistableV2)this).Save(conn);
            }
            finally
            {
                if (conn.IsDBStarted) conn.EndDatabase();
            }
        }

        /// <summary>This method save Notes detail with connection
        /// </summary>
        /// <param name="conn">DataController object</param>
        void IPersistableV2.Save(DataController conn)
        {
            try
            {
                WriteNotes wr = new WriteNotes(conn);
                wr.WriteNotesDetails(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Notes " + this.NoteID.ToString() + " for User " + this.UserID.ToString(), ex);
            }
        }

        #region IDeleteable Members

        public void Delete()
        {
            DataController conn = new DataController();
            try
            {
                conn.StartDatabase(ConnectionString.GetConnectionString());
                ((IDeleteable)this).Delete(conn);
            }
            finally
            {
                if (conn.IsDBStarted) conn.EndDatabase();
            }
        }

        /// <summary>
        /// This method delete Note object
        /// </summary>
        /// <param name="con">DataController object</param>
        public void Delete(DataController con)
        {
            try
            {
                WriteNotes wr = new WriteNotes(con);
                wr.DeleteNoteDetails(this.NoteID);

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Note detail " + this.NoteID.ToString(), ex);
            }
        }

        #endregion
    }
}
