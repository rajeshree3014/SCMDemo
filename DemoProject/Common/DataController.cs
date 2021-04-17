using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoProject.Common
{
    [Serializable()]
	public class DataController
	{
		protected SqlConnection _currentConnection = null;

		public bool IsDBStarted { get; private set; } = false;

		public virtual void AddCommand(SqlCommand command)
		{
			if (command == null) throw new Exception("Null command passed");
			command.Connection = _currentConnection;
		}

		public void StartDatabase(string connection)
		{
			if (IsDBStarted) throw new Exception("Database is already started");
			try
			{
				_currentConnection = new SqlConnection(connection);
				_currentConnection.Open();
				this.IsDBStarted = true;
			}
			catch (Exception ex)
			{
				if (_currentConnection != null && _currentConnection.State != ConnectionState.Closed) _currentConnection.Close();
				if (_currentConnection != null) _currentConnection.Dispose();

				throw ex;
			}
		}

		public void EndDatabase()
		{
			if (!IsDBStarted) throw new Exception("Database is not started");
			if (_currentConnection != null && _currentConnection.State != ConnectionState.Closed) _currentConnection.Close();
			if (_currentConnection != null) _currentConnection.Dispose();
			this.IsDBStarted = false;
		}
	}
}
